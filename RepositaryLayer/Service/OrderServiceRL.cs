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
            Console.WriteLine(FormatDate(request.orderDate));
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@orderDate", FormatDate(request.orderDate));
                parameters.Add("@bookId", request.bookId);
                parameters.Add("@userId", userId);

                using (var connection = context.CreateConnection())
                {
                    var result = connection.Execute("spAddOrder", parameters, commandType: CommandType.StoredProcedure);
                    if (result > 0)
                        return new List<object> { "Order Added Successfully" };
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
