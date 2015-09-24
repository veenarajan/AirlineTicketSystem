using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTicketSystem
{
    /// <summary>
    /// The Encoder class is implemnted to convert the Order Class Object to a String 
    /// </summary>
    class Encoder
    {
        private string encryptedString;
        private string temp;
        public string encryptString(OrderClass order)
        {
            try
            {
                EncryptDecryptService.ServiceClient client = new EncryptDecryptService.ServiceClient();
                temp = (order.get_senderId()) + "," + Convert.ToString(order.get_cardNo()) + "," + (order.get_receiverId())
                    + "," + Convert.ToString(order.get_amount()) + "," + Convert.ToString(order.get_unitprice()) + "," +
                    Convert.ToString(order.get_totalamount()) + "," + Convert.ToString(order.get_confirmationstatus()) +
                    "," + Convert.ToString(order.get_orderflag());

                encryptedString = client.Encrypt(temp);
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception occured while Encoding" + e.Message.ToString());
            }

            return encryptedString;
        }
    }
}
