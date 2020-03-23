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

        //Feature Envy: This method uses only the data of the Order and Item objects
        private void CalculateAmount(Order order)
        {
            var total = 0d;
            //Switch statements: We can use polymorphism 
            foreach (var item in order.Items)
            {
                //Data class: The Item class is a data class, we can add some functionality to it
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

        //Feature Envy: This method uses primarily the data of the Order and Item objects
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
