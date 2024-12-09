namespace BlazorApp.Components.Models
{
    public class Pizza
    {
        public const int DefaultSize = 12;
        public const int MinimumSize = 9;
        public const int MaximumSize = 17;

        public int Id { get; set; }

        //public int OrderId { get; set; }

        public PizzaSpecial Special { get; set; }

        public int SpecialId { get; set; }

        public int Size { get; set; }

        public List<PizzaTopping> Toppings { get; set; }

        //public string Name { get; set; }

        //public string Description { get; set; }

        //public decimal Price { get; set; }

        //public bool Vegetarian { get; set; }

        //public bool Vegan { get; set; }

        public decimal GetBasePrice() =>
            Special is { FixedSize: not null }
            ? Special.BasePrice
            : (decimal)Size / DefaultSize * Special?.BasePrice ?? 1;

        public decimal GetTotalPrice()
        {
            return GetBasePrice();
        }

        public string GetFormattedTotalPrice()
        {
            return GetTotalPrice().ToString("0.00");
        }
    }
}
