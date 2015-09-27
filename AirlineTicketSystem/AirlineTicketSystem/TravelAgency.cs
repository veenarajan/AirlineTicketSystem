using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace AirlineTicketSystem
{
    /// <summary>
    /// Generates Order after getting price cut event from Airline and sends to multibuffer
    /// Sends order to Encoder.
    /// Implementes Call back function for confirmation of order. Includes total time taken and amount processed.
    /// </summary>
    class TravelAgency
    {
        private int id;
        private string name;
        private int thread_price;
        private bool flag;
        private string AirlineName;
        private string encodedOrderString;
        private static int OrderCount;
        static Random rnd = new Random();
        Stopwatch stopWatch = new Stopwatch();
        TimeSpan timeSpan = new TimeSpan();

        public TravelAgency(string n, int my_id)
        {
            this.name = n;
            this.id = my_id;
            this.flag = false;                      // to facilitate intial placement of order
            OrderCount = 0;
        }

            private OrderClass generate_obj()
            {
                OrderClass obj = new OrderClass();
                timeSpan = TimeSpan.Zero;

                obj.set_senderId(name);
                obj.set_cardNo(rnd.Next(1000, 9000));
                obj.set_receiverId(AirlineName);
                obj.set_amount(rnd.Next(3, 15));
                obj.set_unitprice(thread_price);
                obj.set_totalamount(0);
                obj.set_confirmationstatus(false);

                return obj;
            }

            public void TravelAgencyFun()
            {
                while (Program.AirlineThread1.IsAlive || Program.AirlineThread2.IsAlive || Program.AirlineThread3.IsAlive)
                { 
                    if (this.flag)
                    {
                        OrderClass obj = new OrderClass();
                        obj = generate_obj();
           
                        Encoder encode_test = new Encoder();
                        encodedOrderString = encode_test.encryptString(obj);
                        stopWatch.Start();

                        MultiCellBuffer.setOneCell(encodedOrderString);
                        //Console.WriteLine("Travel Agency Sender id {0} receiver id {1} Amount {2} Unitprice {3}", obj.get_senderId(), obj.get_receiverId(), obj.get_amount(), obj.get_unitprice());
                        OrderCount++;        

                        this.flag = false;
                    }
                }
            }

        public void EventHandler(int p, string n) 
        {

            this.thread_price = p;
            this.AirlineName = n;
            this.flag = true;
        }

        public void EventHandler_ConfirmationStatus(string message)
        {
            Decoder decode_test = new Decoder();
            OrderClass obj = new OrderClass();
            obj = decode_test.decryptString(message);
        

            if (obj.get_senderId() == this.name)
            {
                Console.WriteLine("\n\n\n***************  ORDER STATUS  ***************");
                Console.WriteLine("Travel {0} placed an order of {1} ticktes to Airline {2}", obj.get_senderId(), obj.get_amount(), obj.get_receiverId());
                Console.WriteLine("The price of each ticket is ${0}", obj.get_unitprice());
                Console.WriteLine("The total amount charged is : ${0}", obj.get_totalamount());
                Console.WriteLine("The order is charged to card number  : {0}", obj.get_cardNo());

                if (obj.get_confirmationstatus())
                {
                    Console.WriteLine("Congratulations !! the order has been processed. Have a safe Flight !");
                }
                else
                {
                    Console.WriteLine("No Tickets Available. Your order could not be processed at this time !");
                }

                stopWatch.Stop();
                timeSpan = stopWatch.Elapsed;
                Console.WriteLine("Total time taken for order confirmation: " + timeSpan.Milliseconds);

                Console.WriteLine("**************************************************");
                Console.WriteLine("\n\n\n");

            }
        }


    }
}
