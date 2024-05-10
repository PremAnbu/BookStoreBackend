using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Entities
{
    public class User
    {
        public int userId { get; set; }
        public String name { get; set; }
        public String email { get; set; }
        public String password { get; set; }
        public long mobileNumber { get; set; }
    }
}
