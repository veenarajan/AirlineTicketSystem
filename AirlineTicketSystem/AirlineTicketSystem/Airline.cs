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
        private static int[] remaining_tickets;   
        static Random rng = new Random();
        private int PriceCutCounter;
        private int current_ticketprice;
        public event priceCutEvent pricecut;

        

        public Airline(string n, int my_id, int number)
        {
            id = my_id;
            name = n;
            PriceCutCounter = 0;
            current_ticketprice = 900;
            remaining_tickets = new int[3];
            remaining_tickets[0] = number; //jetairways
            remaining_tickets[1] = number;  //britishairways
            remaining_tickets[2] = number;  //luftansa
        }

        public static void AirlineFun(int p, string n)
        {
            try{
                string multiCell = MultiCellBuffer.getOneCell();
            
                Decoder decode_test = new Decoder();
                OrderClass obj = new OrderClass();
           
                obj = decode_test.decryptString(multiCell);
                // Console.WriteLine("Airline Function Sender id {0} card no{1} receiver id {2} Amount {3} Unitprice {4}",
                //       obj.get_senderId(), obj.get_cardNo(), obj.get_receiverId(), obj.get_amount(), obj.get_unitprice());
           
                Console.WriteLine("Airline Function Sender id {0} receiver id {1} Amount {2} Unitprice {3}",
                            obj.get_senderId(), obj.get_receiverId(), obj.get_amount(), obj.get_unitprice());

                if(obj.get_receiverId() == "JetAirways")
                {
                    remaining_tickets[0] = remaining_tickets[0] - obj.get_amount();
                    Console.WriteLine("Remaining tic {0} {1}", remaining_tickets[0], obj.get_receiverId());

                    if (remaining_tickets[0] < 0)
                    {
                        Console.WriteLine("Amount not processed is {0} {1}", obj.get_amount(), obj.get_receiverId());
                        //obj.set_amount(0);
                        obj.set_orderflag(false);
                    }

                    OrderProcessing order = new OrderProcessing();
                    Thread OrderProcessingThread = new Thread(() => order.OrderProcessingFun(obj));
                    OrderProcessingThread.Start();
                    Console.WriteLine("Jetairways Thread created");
                }
            
                else if(obj.get_receiverId() == "BritishAir")
                {
                    remaining_tickets[1] = remaining_tickets[1] - obj.get_amount();
                    Console.WriteLine("Remaining tic {0} {1}", remaining_tickets[1], obj.get_receiverId());
                    if (remaining_tickets[1] < 0)
                    {
                        Console.WriteLine("Amount not processed is {0} {1}", obj.get_amount(), obj.get_receiverId());
                        //obj.set_amount(0);
                        obj.set_orderflag(false);
                    }

                    OrderProcessing order = new OrderProcessing();
                    Thread OrderProcessingThread = new Thread(() => order.OrderProcessingFun(obj));
                    OrderProcessingThread.Start();
                    Console.WriteLine("BritishAir Thread created");
                }
                else if (obj.get_receiverId() == "Luftansa")
                {
                    remaining_tickets[2] = remaining_tickets[2] - obj.get_amount();
                    Console.WriteLine("Remaining tic {0} {1}", remaining_tickets[2], obj.get_receiverId());
                    if (remaining_tickets[2] < 0)
                    {
                        Console.WriteLine("Amount not processed is {0} {1}", obj.get_amount(), obj.get_receiverId());
                        //obj.set_amount(0);
                        obj.set_orderflag(false);
                    }

                    OrderProcessing order = new OrderProcessing();
                    Thread OrderProcessingThread = new Thread(() => order.OrderProcessingFun(obj));
                    OrderProcessingThread.Start();
                    Console.WriteLine("Luftansa Thread created");
            
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception occured in Airline function" + e.Message.ToString());
            }
        }

        public void PricingModel()
        {
            int NoOfTickets = remaining_tickets[0];
            while(PriceCutCounter < 5)
            {
                int price = rng.Next(100, 900);
  /*              int price = 900;
                if (name == "JetAirways")
                    NoOfTickets = remaining_tickets[0];
                else if(name == "BritishAir")
                    NoOfTickets = remaining_tickets[1];
                else if(name == "Luftansa")
                    NoOfTickets = remaining_tickets[2];

                if (NoOfTickets > 6)
                {
                    price = rng.Next(100, 300);
                }
    /*            else if ((NoOfTickets > 150) && (NoOfTickets <= 180))
                {
                    price = rng.Next(200, 250);
                }
                else if ((NoOfTickets > 100) && (NoOfTickets <= 150))
                {
                    price = rng.Next(250, 300);
                }*/
     /*           else if ((NoOfTickets >= 3) && (NoOfTickets <= 6))
                {
                    price = rng.Next(301, 500);
                }
                else if (NoOfTickets < 3)
                {
                    price = rng.Next(501, 900);
                }


                */

               // Console.WriteLine("New price generated by pricingmodel is {0} {1}\n", price, name);

                if (price < current_ticketprice)
                {
                    if (pricecut != null)
                    {
                        pricecut(price, name);
                    }
                    current_ticketprice = price;
                    PriceCutCounter++;
                   // Console.WriteLine("Counter is {0} {1} {2}\n", PriceCutCounter, name, current_ticketprice);
                }
            }
        }
    }
}
