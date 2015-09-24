using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTicketSystem
{
    /// <summary>
    /// Provides the implementation of the Order Object
    /// provides implementation to get and set the values of the object.
    /// </summary>
    class OrderClass
    {
        private string senderId;    // identity of sender either thread name or thread id
        private int cardNo;         // Int representing credit card number
        private string receiverId;  //identity of the receiver either name/name for airline
        private int amount;         //int representing number of tickets to order
        private int unitprice;      // Price of ticket received from airline
        private int TotalAmount;
        private bool ConfirmationStatus;
        private bool orderflag;

        public void set_senderId(string sid)
        {
            senderId = sid;
        }

        public void set_cardNo(int cnumber)
        {
            cardNo = cnumber;
        }

        public void set_receiverId(string rid)
        {
            receiverId = rid;
        }

        public void set_amount(int amt)
        {
            amount = amt;
        }

        public void set_unitprice(int uprice)
        {
            unitprice = uprice;
        }

        public void set_totalamount(int amt)
        {
            TotalAmount = amt;
        }

       public void set_confirmationstatus(bool ret)
        {
            ConfirmationStatus = ret;
        }

        public void set_orderflag(bool value)
       {
           orderflag = value;
       }

        public string get_senderId()
        {
            return senderId;
        }

        public int get_cardNo()
        {
            return cardNo;
        }

        public string get_receiverId()
        {
            return receiverId;
        }

        public int get_amount()
        {
            return amount;
        }

        public int get_unitprice()
        {
            return unitprice;
        }
        public int get_totalamount()
        {
            return TotalAmount;
        }
        public bool get_confirmationstatus()
        {
            return ConfirmationStatus;
        }

        public bool get_orderflag()
        {
            return orderflag;
        }

    }
}