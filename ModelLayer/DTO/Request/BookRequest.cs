using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.Request
{
    public class BookRequest
    {
        public string title { get; set; }
        public string author { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
        public string imagePath { get; set; }
    }
}
