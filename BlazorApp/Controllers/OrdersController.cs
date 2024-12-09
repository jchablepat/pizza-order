using BlazorApp.Components.Models;
using BlazorApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(PizzaStoreContext db) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<OrderWithStatus>>> GetOrders()
        {
            var orders = await db.Orders
                .Include(o => o.Pizzas)
                .ThenInclude(p => p.Special)
                .Include(o => o.Pizzas)
                .ThenInclude(p => p.Toppings)
                .ThenInclude(t => t.Topping)
                .OrderByDescending(o => o.CreatedTime)
                .ToListAsync();

            return orders.Select(o => OrderWithStatus.FromOrder(o!)).ToList();
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderWithStatus>> GetOrderWithStatus(int orderId)
        {
            var order = await db.Orders
                .Where(o => o.OrderId == orderId)
                .Include(o => o.Pizzas)
                .ThenInclude(p => p.Special)
                .Include(o => o.Pizzas)
                .ThenInclude(p => p.Toppings)
                .ThenInclude(t => t.Topping)
                .SingleOrDefaultAsync();

            if (order == null) {
                return NotFound();
            }

            return OrderWithStatus.FromOrder(order);
        }

        [HttpPost]
        public async Task<ActionResult<int>> PlaceOrder(Order order)
        {
            order.CreatedTime = DateTime.Now;

            // Enforce existence of Pizza.SpecialId and Topping.ToppingId
            // in the database - prevent the submitter from making up
            // new specials and toppings
            foreach (var pizza in order.Pizzas)
            {
                pizza.SpecialId = pizza.Special.Id;
                pizza.Special = null;
            }

            db.Orders.Attach(order);
            await db.SaveChangesAsync();

            return order.OrderId;
        }
    }
}
