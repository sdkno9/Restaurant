using System;

namespace SpRestaurant.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public string GetItemReceiptLine()
        {
            return Quantity + " of item " + ItemId;
        }
    }
}
