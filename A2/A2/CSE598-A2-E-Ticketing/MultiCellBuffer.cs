using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CSE598_A2_E_Ticketing
{
    class MultiCellBuffer
    {
        List<Order> dataCells;
        private static readonly int n = 2; // number of cells in the multi cell buffer
        private static readonly Semaphore reading = new Semaphore(0, n); // semaphore to handle available cells to read from
        private static readonly Semaphore writing = new Semaphore(0, n); // semaphore to handle available cells to write to

        public MultiCellBuffer()
        {
            dataCells = new List<Order>();
            writing.Release(2); // initially both cells are available to write to, and none can be read from 
            
        }

        public void setOneCell(Order order)
        {
            writing.WaitOne(); // wait for a cell to be available to write to
            lock (dataCells) // lock the buffer to ensure thread safety
            {
                dataCells.Add(order);
            }
            reading.Release(); // release one reading semaphore since there is now one more cell available to read from
            

        }

        public Order readOneCell()
        {
            if (!reading.WaitOne(100)) // wait (for a max of 100ms) for a cell to be available to read from
            {
                return null; // no order to processso return null
            }
            Order order;
            lock (dataCells) // lock the buffer to ensure thread safety
            {
                order = dataCells[0];
                dataCells.RemoveAt(0);
            }
            
           writing.Release(); // release one writing semaphore since there is now a cell available to write to
            return order;
        }
    }
}
