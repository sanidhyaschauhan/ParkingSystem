using System;

namespace ParkingSys
{
    public class OrderProcessing
    {
        // Event that gets triggered whenever an order has been processed
        public static event orderProcessedEvent orderProcessed;

        // Method to process the given order and calculate the total amount
        public static void processOrder(OrderClass orderObject, double currentPrice)
        {
            // Check if the credit card associated with the order is valid
            if (!checkCardValidity(orderObject.CardNo))
            {
                Console.WriteLine("{0} is not a valid credit card number.", orderObject.CardNo);
                return;
            }
            else
            {
                // Calculate tax as a random value between 8% and 12%
                double tax = new Random().NextDouble() * (0.12 - 0.08) + 0.08;

                // Calculate location charge as a random value between $2 and $8
                double locationCharge = new Random().NextDouble() * (8 - 2) + 2;

                // Calculate the total amount for the order based on the order quantity, current price, tax, and location charge
                double totalAmount = orderObject.Quantity * currentPrice + (tax * currentPrice) + locationCharge;

                // Trigger the order processed event with relevant details
                orderProcessed(orderObject.SenderId, totalAmount, currentPrice, orderObject.Quantity, orderObject);
            }
        }

        // Utility method to check if a given credit card number is valid
        private static bool checkCardValidity(int cardNo)
        {
            // Credit card number is valid if it's between 5000 and 7000 (inclusive)
            return cardNo <= 7000 && cardNo >= 5000;
        }
    }
}
