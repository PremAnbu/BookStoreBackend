using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Entities
{
    public class WishList
    {
        public int wishListId {  get; set; }
        public int bookId {  get; set; }
        public int userId {  get; set; }
    }
}
