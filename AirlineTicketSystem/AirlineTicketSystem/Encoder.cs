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
        public string encryptString(OrderClass order)
        {
            EncryptDecryptService.ServiceClient client = new EncryptDecryptService.ServiceClient();
            string encryptedString;

            string temp = (order.get_senderId()) + "," + Convert.ToString(order.get_cardNo()) + "," + (order.get_receiverId())
                + "," + Convert.ToString(order.get_amount()) + "," + Convert.ToString(order.get_unitprice()) + "," +
                Convert.ToString(order.get_totalamount()) + "," + Convert.ToString(order.get_confirmationstatus());

            encryptedString = client.Encrypt(temp);

            return encryptedString;
        }
    }
}
