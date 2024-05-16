using ModelLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Entities
{
    public class Address
    {
        public int addressId { get; set; }
        public String address { get; set; }
        public String city { get; set; }
        public String state { get; set; }
        public AddressType type { get; set; }
        public int userId { get; set; }
    }
}
