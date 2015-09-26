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
            id = my_id;
            name = n;
            PriceCutCounter = 0;
            current_ticketprice = 900;
            remaining_tickets = number;
            Termination = 0;
        }

        public void AirlineFun()
        {
            while (true)
            {
                string multiCell = MultiCellBuffer.getOneCell(this.name);
                if (multiCell != null)
                {
                    Decoder decode_test = new Decoder();
                    OrderClass obj = new OrderClass();

                    obj = decode_test.decryptString(multiCell);
                    Console.WriteLine("Airline Function Sender id {0} receiver id {1} Amount {2} Unitprice {3}",
                                    obj.get_senderId(), obj.get_receiverId(), obj.get_amount(), obj.get_unitprice());

                    this.remaining_tickets = this.remaining_tickets - obj.get_amount();
                    Console.WriteLine("Remaining tic {0} {1}", this.remaining_tickets, obj.get_receiverId());

                    if (this.remaining_tickets < 0)
                    {
                        Console.WriteLine("Amount not processed is {0} {1}", obj.get_amount(), obj.get_receiverId());
                        obj.set_amount(0);
                    }

                    OrderProcessing order = new OrderProcessing();
                    Thread OrderProcessingThread = new Thread(() => order.OrderProcessingFun(obj));
                    OrderProcessingThread.Start();
                    //OrderProcessingThread.Join();
                    Console.WriteLine("{0} Thread created", obj.get_receiverId());
                }
                Thread.Sleep(1000);
            }
            //while (Termination != 1);
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
                            Console.WriteLine("PRICE CUT !!!!!");
                            pricecut(price, this.name);
                        }
                        current_ticketprice = price;
                        PriceCutCounter++;
                    }
                    else
                        Termination = 1;
                }
            }
            //while (Termination != 1);
        }
    }
}
