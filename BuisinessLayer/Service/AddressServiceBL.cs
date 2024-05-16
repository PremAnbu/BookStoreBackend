using BusinessLayer.Interface;
using ModelLayer.DTO.Request;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class AddressServiceBL(IAddressRL serviceRL):IAddressBL
    {
        public string AddAddress(AddressRequest address, int userId)
        {
            return serviceRL.AddAddress(address, userId);
        }
    }
}
