using System;
using System.Threading;

namespace ParkingSys
{
    // Delegate for the event when the price is cut
    public delegate void priceCutEvent(double price);

    // Delegate for the event when an order is processed. Contains details of the order.
    public delegate void orderProcessedEvent(string senderID, double totalAmount, double price, int amount, OrderClass orderObj);

    // Delegate for the event when a new order is created
    public delegate void orderCreatedEvent();

    public class Program
    {
        // Boolean flag indicating the running status of the parking structure thread
        public static bool parkingStructureThreadRunning = true;

        // MultiCellBuffer instance to handle multiple orders simultaneously
        public static MultiCellBuffer multiCellBuffer;

        // Array to hold threads for each parking agent
        public static Thread[] parkingAgentThreads;

        // Constants for configuration: 
        // N represents the total number of Parking Agents
        // K represents the total number of Parking Structures
        // n represents the number of cells in the MultiCellBuffer
        public const int N = 5;
        public const int K = 1;
        public const int n = 3;

        static void Main(string[] args)
        {
            Console.WriteLine("Simulation Started...");

            // Initialize the MultiCellBuffer with 'n' cells to manage multiple orders
            multiCellBuffer = new MultiCellBuffer(n);

            // Create and start the ParkingStructure threads
            for (int i = 0; i < K; i++)
            {
                // Initialize a new Parking Structure
                ParkingStructure parkingStructure = new ParkingStructure();

                // Create a new thread for handling price cuts and start it
                Thread handlePriceCutThread = new Thread(new ThreadStart(parkingStructure.handlePriceCut));
                handlePriceCutThread.Start();

                // Whenever an order is created, the placeOrder method of the parking structure is called
                ParkingAgent.orderCreated += new orderCreatedEvent(parkingStructure.placeOrder);
            }

            Console.WriteLine($"{K} Parking Structure(s) initialized");

            // Initialize and start the ParkingAgent threads
            parkingAgentThreads = new Thread[N];
            for (int i = 0; i < N; i++)
            {
                // Initialize a new Parking Agent
                ParkingAgent parkingAgent = new ParkingAgent();

                // Create a new thread for the agent and set its name
                parkingAgentThreads[i] = new Thread(new ParameterizedThreadStart(parkingAgent.startAgent));
                parkingAgentThreads[i].Name = (i + 1).ToString();

                // Whenever the price is cut, the onSale method of the parking agent is called
                ParkingStructure.priceCut += new priceCutEvent(parkingAgent.onSale);

                // Whenever an order is processed, the orderProcessed method of the parking agent is called
                OrderProcessing.orderProcessed += new orderProcessedEvent(parkingAgent.orderProcessed);

                // Start the agent's thread
                parkingAgentThreads[i].Start((i + 1).ToString());
            }

            Console.WriteLine($"{N} Parking Agents initialized");
            Console.WriteLine("Initial price: $15");
        }
    }
}
