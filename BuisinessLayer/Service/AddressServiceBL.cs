using BusinessLayer.Interface;
using ModelLayer.DTO.Request;
using ModelLayer.Entities;
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
        public Address UpdateAddress(int userId, Address request)
        {
            return serviceRL.UpdateAddress(userId, request);
        }
        public bool DeleteAddress(int AddressId)
        {
            return serviceRL.DeleteAddress(AddressId);
        }
        public List<Object> GetAddress(int userId)
        {
            return serviceRL.GetAddress(userId);
        }

    }
}
