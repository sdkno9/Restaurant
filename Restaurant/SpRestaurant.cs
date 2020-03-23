using System;
using SpRestaurant.Hardware;
using SpRestaurant.Models;
using SpRestaurant.Services.CookingService;
using SpRestaurant.Services.PaymentService;
using SpRestaurant.Utilities;

namespace SpRestaurant
{
    public class SpRestaurant
    {
        readonly HpPrinter printer = new HpPrinter();
        readonly  PaymentService paymentService = new PaymentService();
        readonly CookingService cookingService = new CookingService();

        public void ExecuteOrder(Order order, PaymentDetails paymentDetails, bool printReceipt)
        {
            CalculateAmount(order);
            paymentService.Charge(paymentDetails, order);

            cookingService.Prepare(order);

            if (printReceipt)
            {
                PrintReceipt(order);
            }
        }

        private void CalculateAmount(Order order)
        {
            var total = 0d;
            foreach (var item in order.Items)
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
                    total += item.Price * item.Quantity * 0.9;
                }
            }
            order.TotalAmount = total;
        }

        private void PrintReceipt(Order order)
        {
            string customerEmail = order.CustomerEmail;
            if (!string.IsNullOrEmpty(customerEmail))
            {
                var receipt = new Receipt
                {
                    Title = "Receipt for your order placed on " + DateTime.Now,
                    Body = "Your order details: \n "
                };
                foreach (var orderItem in order.Items)
                {
                    receipt.Body += orderItem.Quantity + " of item " + orderItem.ItemId;
                }

                try
                {
                    printer.Print(receipt);
                }
                catch (Exception ex)
                {
                    Logger.Error("Problem sending notification email", ex);
                }

            }
        }
    }
}
