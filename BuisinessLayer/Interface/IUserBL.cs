using ModelLayer.DTO.Request;
using ModelLayer.DTO.Responce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisinessLayer.Interface
{
    public interface IUserBL
    {
        public int Register(UserRequest request);
        public UserResponce Login(String Email, String password);
    }
}
