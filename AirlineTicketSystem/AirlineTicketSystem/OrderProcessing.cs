using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTicketSystem
{
    public delegate void OrderConfirmationEvent();
    
class OrderProcessing
    {
        private string senderId;
        private int cardNo;
        private string receiverId;
        private int amount;
        private int unitprice;
        private int TotalAmount;
        private int Tax = 1000;
        private int LocationCharge = 100;
        public static event OrderConfirmationEvent OrderConfirmation;


        private bool IsValidCardNo(int card_number)
        {
            if (card_number > 1000 && card_number < 9001)
            {
                return true;
            }
            return false;
        }

        private void CalculatePrice()
        {
            TotalAmount = unitprice * amount + Tax + LocationCharge; //amount is no of tickets
            Console.WriteLine("Total amount is {0}", TotalAmount);
        }
        public void OrderProcessingFun(OrderClass obj_1)
        {
            Encoder encode = new Encoder();
            
            senderId = obj_1.get_senderId();
            cardNo = obj_1.get_cardNo();
            receiverId = obj_1.get_receiverId();
            amount = obj_1.get_amount();
            unitprice = obj_1.get_unitprice();

            string encodedString;
           // if (receiverId == "JetAirways")
                Console.WriteLine("Order Processing Sender id {0} receiver id {1} Amount {2} Unitprice {3}", senderId, receiverId, amount, unitprice);
      
            if (!IsValidCardNo(cardNo) || !obj_1.get_orderflag())
            {
                Console.WriteLine("the order cannot be processed {0} {1}", senderId, receiverId);
                obj_1.set_confirmationstatus(false);
                obj_1.set_totalamount(0);
                try
                {
                    encodedString = encode.encryptString(obj_1);
                    ConfirmationBuffer.setOneCell(encodedString);
                }
                catch(Exception e)
                {
                    Console.WriteLine("Exception occured while encoding string to send confirmation " + e.Message.ToString());
                }
                
                
            }
            else
            {
                CalculatePrice();
                //Console.WriteLine("it's done :P");
                obj_1.set_confirmationstatus(true);
                obj_1.set_totalamount(TotalAmount);

                try
                {
                    encodedString = encode.encryptString(obj_1);
                    ConfirmationBuffer.setOneCell(encodedString);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception occured while encoding string to send confirmation " + e.Message.ToString());
                }
                
            }
            //TravelAgency.EventHandler_ConfirmationStatus();
            OrderConfirmation();
        }
    }
}
