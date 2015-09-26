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
            TotalAmount = unitprice * amount + Tax + LocationCharge;
            
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
          
            if (!IsValidCardNo(cardNo) || amount == 0)
            {
                obj_1.set_confirmationstatus(false);
                obj_1.set_totalamount(0);
                encodedString = encode.encryptString(obj_1);
                ConfirmationBuffer.setOneCell(encodedString);

            }
            else
            {
                CalculatePrice();
                obj_1.set_confirmationstatus(true);
                obj_1.set_totalamount(TotalAmount);

                encodedString = encode.encryptString(obj_1);
                ConfirmationBuffer.setOneCell(encodedString);

            }
            OrderConfirmation();
        }
    }
}
