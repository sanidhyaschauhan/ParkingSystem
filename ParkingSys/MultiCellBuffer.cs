using System;

namespace ParkingSys
{
    public class MultiCellBuffer
    {

        // Array of cells to hold the orders
        public OrderClass[] cells;

        // Total number of cells in the buffer
        private int n;

        // Semaphore to control concurrent access to the buffer cells
        private Semaphore semaphore;

        public MultiCellBuffer(int n)
        {
            // Locking to ensure atomic initialization of buffer cells
            lock (this)
            {
                this.n = n;

                // Initialize the semaphore with the maximum number of cells
                semaphore = new Semaphore(n, n);

                cells = new OrderClass[n];

                // Initialize all cells as null (empty)
                for (int i = 0; i < n; i++)
                {
                    cells[i] = null;
                }
            }
        }

        // Method to place an order in one of the available buffer cells
        public void setOneCell(OrderClass order)
        {
            // Acquire a semaphore slot before accessing the buffer
            semaphore.WaitOne();

            // Lock to ensure atomic access to buffer cells
            lock (this)
            {
                for (int i = 0; i < n; i++)
                {
                    // If the cell is empty (null), place the order in it
                    if (cells[i] == null)
                    {
                        cells[i] = order;
                        break; // Exit the loop once an order is placed
                    }
                }
            }

            Console.WriteLine($"Order from Agent {order.SenderId} placed in buffer.");

            // Release the semaphore slot after placing the order
            semaphore.Release();
        }

        // Method to retrieve and remove an order from the buffer
        public OrderClass getOneCell()
        {
            // Lock to ensure atomic access to buffer cells
            lock (this)
            {
                for (int i = 0; i < n; i++)
                {
                    // If the cell has an order, retrieve and remove it
                    if (cells[i] != null)
                    {
                        OrderClass order = cells[i];

                        cells[i] = null; // Remove the order from the cell

                        Console.WriteLine($"Order for Agent {order.SenderId} retrieved from buffer.");

                        return order;
                    }
                }
                return null;
            }
        }

        public int CountFilledCells()
        {
            lock (this)
            {
                return cells.Count(cell => cell != null);
            }
        }
    }
}