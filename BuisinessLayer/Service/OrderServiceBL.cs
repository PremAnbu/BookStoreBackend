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
    public class OrderServiceBL(IOrderRL order):IOrderBL
    {
        public List<Object> GetOrder(int userId)
        {
            return order.GetOrder(userId);
        }
        public List<Object> AddOrder(OrderRequest request, int userId)
        {
            return order.AddOrder(request,userId);
        }

    }
}
