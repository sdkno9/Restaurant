using SpRestaurant.Models;
using SpRestaurant.Utilities;
using System;

namespace SpRestaurant.Hardware
{
    public class HpPrinter
    {
        public void PrintReceipt(Order order)
        {
            string customerEmail = order.CustomerEmail;
            if (!string.IsNullOrEmpty(customerEmail))
            {
                try
                {
                    Receipt receipt = order.GetReceipt();
                    Print(receipt);
                }
                catch (Exception ex)
                {
                    Logger.Error("Problem sending notification email", ex);
                }

            }
        }

        private void Print(Receipt receipt)
        {
            throw new NotImplementedException();
        }
    }
}
