using SpRestaurant.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpRestaurant.Models
{
    public class Order
    {
        public List<Item> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerEmail { get; set; }

        public void CalculateAmount()
        {
            var total = 0m;

            if (Items?.Any() == true)
            {
                foreach (var item in Items)
                {
                    if (item.ItemId == Constants.Drink)
                    {
                        var setsOfThree = item.Quantity / 3;
                        total += (item.Quantity - setsOfThree) * item.Price;
                    }
                    else if (item.ItemId == Constants.CheeseBurger)
                    {
                        total += item.Price * item.Quantity;
                    }
                    else if (item.ItemId == Constants.CheeseBurgerMenu)
                    {
                        total += item.Price * item.Quantity * 0.9m;
                    }
                }
            }
            TotalAmount = total;
        }

        public Receipt GetReceipt()
        {
            var receipt = new Receipt
            {
                Title = "Receipt for your order placed on " + DateTime.Now,
                Body = "Your order details: \n "
            };

            if (Items?.Any() == true)
            {
                foreach (var orderItem in Items)
                {
                    receipt.Body += orderItem.GetItemReceiptLine();
                }
            }

            return receipt;
        }
    }
}
