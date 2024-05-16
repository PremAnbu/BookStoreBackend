using ModelLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.Request
{
    public class AddressRequest
    { 
        public String address { get; set; }
        public String city { get; set; }
        public String state { get; set; }
        public AddressType type { get; set; }
    }
}
