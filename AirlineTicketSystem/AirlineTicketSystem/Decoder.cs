using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTicketSystem
{
    /// <summary>
    /// The Decoder class is implemented to convert the String back to the Order Class Object
    /// </summary>
    class Decoder
    {
        public OrderClass decryptString(string encryptedString)
        {
            EncryptDecryptService.ServiceClient client = new EncryptDecryptService.ServiceClient();
            OrderClass order = new OrderClass();

            string[] dString = new string[7];
            string decryptedString = client.Decrypt(encryptedString);

            dString = decryptedString.Split(',');

            order.set_senderId(dString[0]);
            order.set_cardNo(Convert.ToInt32(dString[1]));
            order.set_receiverId(dString[2]);
            order.set_amount(Convert.ToInt32(dString[3]));
            order.set_unitprice(Convert.ToInt32(dString[4]));
            order.set_totalamount(Convert.ToInt32(dString[5]));
            order.set_confirmationstatus(Convert.ToBoolean(dString[6]));

            return order;
        }
    }
}
