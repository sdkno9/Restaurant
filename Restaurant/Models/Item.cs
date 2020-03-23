using System;

namespace SpRestaurant.Models
{
    public class Item
    {
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
