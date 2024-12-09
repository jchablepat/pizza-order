using BlazorApp.Components.Models;
using BlazorApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialsController(PizzaStoreContext db) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<PizzaSpecial>>> GetSpecials()
        {
            return (await db.Specials.ToListAsync()).OrderByDescending(s => s.BasePrice).ToList();
        }
    }
}
