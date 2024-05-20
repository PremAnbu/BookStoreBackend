using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.Request
{
    public class OrderRequest
    {
        public DateTime orderDate { get; set; }
        public int bookId { get; set; }
    }
}
