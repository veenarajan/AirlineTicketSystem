using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace AirlineTicketSystem
{
    public delegate void priceCutEvent(int price, string name);

    class Airline
    {
        private int id;
        private string name;
        private int remaining_tickets;
        static Random rng = new Random();
        private int PriceCutCounter;
        private int current_ticketprice;
        public event priceCutEvent pricecut;
        private static int Termination;



        public Airline(string n, int my_id, int number)
        {
            this.id = my_id;
            this.name = n;
            this.PriceCutCounter = 0;
            this.current_ticketprice = 900;
            this.remaining_tickets = number;
            Termination = 0;
        }

        public void AirlineFun()
        {
           
            while (true)
            {
               
                string multiCell = MultiCellBuffer.getOneCell(name);
        
                if (multiCell != null)
                {
                    Decoder decode_test = new Decoder();
                    OrderClass obj = new OrderClass();

                    obj = decode_test.decryptString(multiCell);
                   
                }
            }
        }

        public void PricingModel()
        {
            while (true)
            {
                int NoOfTickets = this.remaining_tickets;
                if (PriceCutCounter < 5)
                {
                    int price = rng.Next(100, 900);
                    if (price < current_ticketprice)
                    {
                        if (pricecut != null)
                        {
                            pricecut(price, this.name);
                        }
                        current_ticketprice = price;
                        PriceCutCounter++;
                        Console.WriteLine("Counter is {0} {1} {2}\n", PriceCutCounter, name, price);

                        if (current_ticketprice == 100)
                        {
                            current_ticketprice = 900;
                        }
                    }
                    else
                        Termination = 1;
                }
                string multiCell = MultiCellBuffer.getOneCell(this.name);
                if (multiCell != null)
                {
                    Decoder decode_test = new Decoder();
                    OrderClass obj = new OrderClass();

                    obj = decode_test.decryptString(multiCell);
                   
                    this.remaining_tickets = this.remaining_tickets - obj.get_amount();
                   

                    if (this.remaining_tickets < 0)
                    {
                       
                        obj.set_amount(0);
                    }

                    OrderProcessing order = new OrderProcessing();
                    Thread OrderProcessingThread = new Thread(() => order.OrderProcessingFun(obj));
                    OrderProcessingThread.Start();
                  
                }
            }
        }
    }
}
