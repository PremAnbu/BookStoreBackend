using ModelLayer.DTO.Request;
using ModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IAddressRL
    {
        string AddAddress(AddressRequest addresrequest, int userId);
        Address UpdateAddress(int userId, Address request);
        bool DeleteAddress(int AddressId);
        List<Object> GetAddress(int userId);

    }
}
