using System.Collections.Generic;

namespace SpRestaurant.Models
{
    public class Order
    {
        public List<Item> Items { get; set; }
        public double TotalAmount { get; set; }
        public string CustomerEmail { get; set; }
    }
}
