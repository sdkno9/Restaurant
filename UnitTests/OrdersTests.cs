using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpRestaurant.Models;
using SpRestaurant.Utilities;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class OrdersTests
    {
        [TestMethod]
        public void OrdersAmountTest()
        {
            //Test scenarios
            //0- Prepare test data and variables
            //1- Order 7 drinks and calculate the amount
            //2- Add 2 cheese burgers to the previous order and re-calculate the amount
            //3- Add 3 cheese burger menus to the previous order and re-calculate the amount
            //4- Calculate the amount of an empty order
            //5- Calculate the amount of an order with an inexisting item

            //0- Prepare test data and variables
            var drinkPrice = 2.5m;
            var cheeseBurgerPrice = 5m;
            var cheeseBurgerMenuPrice = 7m;
            var items = new List<Item>();

            //1- Order 7 drinks and calculate the amount
            var sevenDrinks = new Item
            {
                ItemId = Constants.Drink,
                Quantity = 7,
                Price = drinkPrice
            };
            items.Add(sevenDrinks);

            CreateOrderAndAssertAmount(
                items,
                expectedAmount: 12.5m,
                message: "we should get (7 - (7/3)) * 2.5");

            //2- Add 2 cheese burgers to the previous order and re-calculate the amount
            var twoCheeseBurgers = new Item
            {
                ItemId = Constants.CheeseBurger,
                Quantity = 2,
                Price = cheeseBurgerPrice
            };
            items.Add(twoCheeseBurgers);

            CreateOrderAndAssertAmount(
                items,
                expectedAmount: 22.5m);

            //3- Add 3 cheese burger menus to the previous order and re-calculate the amount
            var threeCheeseBurgerMenus = new Item
            {
                ItemId = Constants.CheeseBurgerMenu,
                Quantity = 3,
                Price = cheeseBurgerMenuPrice
            };
            items.Add(threeCheeseBurgerMenus);

            CreateOrderAndAssertAmount(
                items,
                expectedAmount: 41.4m);

            //4- Calculate the amount of an empty order
            items = new List<Item> { };
            CreateOrderAndAssertAmount(
                items,
                expectedAmount: 0m);

            //5- Calculate the amount of an order with an inexisting item
            var inexistingItem = new Item
            {
                ItemId = 100,
                Quantity = 3,
                Price = 11
            };
            items.Add(inexistingItem);

            CreateOrderAndAssertAmount(
                items,
                expectedAmount: 0m);
        }

        private void CreateOrderAndAssertAmount(
            List<Item> items, 
            decimal expectedAmount, 
            string message = null)
        {
            var order = new Order()
            {
                CustomerEmail = "john.doe@test.com",
                Items = items
            };
            order.CalculateAmount();

            Assert.AreEqual(expectedAmount, order.TotalAmount, message);
        }
    }
}
