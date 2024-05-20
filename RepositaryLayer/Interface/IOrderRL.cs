using ModelLayer.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IOrderRL
    {
        List<Object> GetOrder(int userId);
        List<Object> AddOrder(OrderRequest request, int userId);
    }
}
