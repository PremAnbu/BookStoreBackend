using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Entities
{
    public class Order
    {
        public int orderId {  get; set; }
        public int userId {  get; set; }
        public DateTime orderDate { get; set; }
        public int bookId {  get; set; }

    }
}
