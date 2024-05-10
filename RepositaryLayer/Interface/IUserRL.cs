using ModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositaryLayer.Interface
{
    public interface IUserRL
    {
        public int Register(User entity);
        public User GetUserByEmail(string email);
    }
}
