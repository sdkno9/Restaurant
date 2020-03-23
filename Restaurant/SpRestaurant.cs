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
            order.CalculateAmount();
            paymentService.Charge(paymentDetails, order);
            cookingService.Prepare(order);

            if (printReceipt)
            {
                printer.PrintReceipt(order);
            }
        }
    }
}
