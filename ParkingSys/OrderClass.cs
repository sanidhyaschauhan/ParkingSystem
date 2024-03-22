using System;

namespace ParkingSys
{
    public class OrderClass
    {
        // Private fields to store order details
        private string senderId;  // ID of the sender (agent) who placed the order
        private int cardNo;       // Credit card number associated with the order
        private int quantity;     // Number of items in the order
        private double unitPrice; // Price per item for the order
        private bool isProcessed; // Flag to track if the order has been processed

        public OrderClass(string senderId, int cardNo, int quantity, double unitPrice)
        {
            this.senderId = senderId;
            this.cardNo = cardNo;
            this.quantity = quantity;
            this.unitPrice = unitPrice;
        }

        // Getter for the sender ID
        public string SenderId
        {
            get { return senderId; }
        }

        // Getter for the credit card number
        public int CardNo
        {
            get { return cardNo; }
        }

        // Getter for the quantity of items in the order
        public int Quantity
        {
            get { return quantity; }
        }

        // Getter for the unit price of items in the order
        public double UnitPrice
        {
            get { return unitPrice; }
        }

        // Property to get and set the processed status of the order
        public bool IsProcessed
        {
            get { return isProcessed; }
            set { isProcessed = value; }
        }

    }
}
