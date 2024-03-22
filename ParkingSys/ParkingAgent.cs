using System;

namespace ParkingSys
{
    public class ParkingAgent
    {
        // Event that gets triggered whenever a new order is created by the Parking Agent
        public static event orderCreatedEvent orderCreated;

        // Random number generator instance for various random operations throughout the class
        public static Random rnd = new Random();

        // Variable to store the ID of the Parking Agent
        private string agentId;

        // Method to start the Parking Agent and set its ID
        public void startAgent(object obj)
        {
            // Assign the passed object (after type casting) as the agent ID
            agentId = (string)obj;
        }

        // Method to create a new order by the Parking Agent
        private void createOrder(string senderID, double price)
        {
            // Generate a random card number between 5000 and 7000 for the order
            int cardNo = rnd.Next(5000, 7000);

            // Calculate the quantity of blocks in the order based on the current price
            int quantity = rnd.Next(1, (40 - (int)price));

            // Create a new order with the generated details
            OrderClass orderObject = new OrderClass(senderID, cardNo, quantity, price);

            Console.WriteLine("Agent {0}'s order has been created.", senderID);

            // Place the created order into the MultiCellBuffer
            Program.multiCellBuffer.setOneCell(orderObject);

            // Trigger the event to notify that a new order has been created
            orderCreated();
        }

        // Method to handle the logic when an order is processed
        public void orderProcessed(string senderID, double totalAmount, double price, int quantity, OrderClass orderObj)
        {
            // Check if the order has not been processed before
            if (!orderObj.IsProcessed)
            {
                Console.WriteLine("Agent {0}'s order has been processed. The amount to be charged is $" + totalAmount + " ($" + price + " + taxes and location charge per block for " + quantity + " blocks).", senderID, Thread.CurrentThread.Name);
            }

            // Mark the order as processed
            orderObj.IsProcessed = true;
        }

        // Method to handle the logic when there's a sale in the Parking Structure
        public void onSale(double price)
        {
            Thread.Sleep(rnd.Next(100, 1000));
            // Use the agent's ID as the sender ID
            string senderID = agentId;

            // Create a new order because of the sale
            createOrder(senderID, price);
        }
    }
}
