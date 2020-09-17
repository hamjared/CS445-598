using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CSE598_A2_E_Ticketing
{
    public delegate void priceCutEvent(int price);
    class Park
    {
        public static event priceCutEvent priceCut;
        private int numPriceCuts;
        private static readonly int maxPriceCuts = 20;
        private PricingModel pm;
        MultiCellBuffer mcb;


        public Park(MultiCellBuffer mcb)
        {
            numPriceCuts = 0;
            pm = new PricingModel();
            this.mcb = mcb; 
        }

        public void runParkThread()
        {
            Random rand = new Random();
            Thread orderProcessingThread = new Thread(new ThreadStart(this.checkOrderQueue));
            orderProcessingThread.Start();
            while(numPriceCuts < maxPriceCuts)
            {
                Thread.Sleep(rand.Next(200, 500));
                pm.updatePrice();
                Console.WriteLine("Park{0} New price: " + pm.getCurrentPrice(), Thread.CurrentThread.Name);
                if (pm.hasPriceDropped())
                {
                    // trigger event to alert ticketing agencies of price drop. 
                    Console.WriteLine("Price drpped! Alert Ticket Agencies");
                    if (priceCut != null)
                    {
                        priceCut(pm.getCurrentPrice());
                    }
                    numPriceCuts++;
                }

                
                
            }

            

            orderProcessingThread.Join(); 
            Console.WriteLine("20 Price cuts have occured, Park{0} exiting ...", Thread.CurrentThread.Name);
        }


        private void checkOrderQueue()
        {
            
            while (numPriceCuts < maxPriceCuts)
            {
                Order order = mcb.readOneCell();
                if (order == null)
                {
                   
                    continue; // no order to process, continue checking for orders
                }
                else
                {
                    Thread thread = new Thread(new ThreadStart(order.processOrder));
                    thread.Start(); // process order in new thread
                    
                }
            }

            Thread.Sleep(1000); // allow time for final orders to fill the buffer
            flushOrderBuffer();

        }

        private void flushOrderBuffer()
        {
            Order order = mcb.readOneCell();
            if (order == null)
            {

                return; // no order to process, return to caller
            }
            else
            {
                Thread thread = new Thread(new ThreadStart(order.processOrder));
                thread.Start(); // process order in new thread
                flushOrderBuffer(); // recurse to see if any orders remain

            }
        }

        public int getNumPriceCuts()
        {
            return numPriceCuts;
        }

        public static int getMaxPriceCuts()
        {
            return maxPriceCuts;
        }

        public int getTicketPrice()
        {
            return pm.getCurrentPrice();
        }
    }
}
