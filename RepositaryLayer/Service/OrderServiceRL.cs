using Dapper;
using ModelLayer.DTO.Request;
using RepositaryLayer.Context;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class OrderServiceRL(DapperContext context) : IOrderRL
    {
        public List<object> AddOrder(OrderRequest request, int userId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@addressId", request.addressId);
                parameters.Add("@orderDate", FormatDate(request.orderDate));
                parameters.Add("@bookId", request.bookId);
                parameters.Add("@userId", userId);
                parameters.Add("@orderId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                Console.WriteLine(request.addressId+" "+request.bookId+" "+FormatDate(request.orderDate));
                using (var connection = context.CreateConnection())
                {
                    connection.Execute("spAddOrder", parameters, commandType: CommandType.StoredProcedure);

                    int orderId = parameters.Get<int>("@orderId");
                    if (orderId > 0)
                        return new List<object> { orderId };
                    else
                        throw new Exception("Failed to add order.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        static string FormatDate(DateTime input)
        {
            return input.ToString("MMMM d");
        }
        public List<object> GetOrder(int userId)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {
                    var orders = connection.Query<object>("spGetOrdersByUserId", new { userId = userId }, commandType: CommandType.StoredProcedure);
                    return orders.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }


    }
}
