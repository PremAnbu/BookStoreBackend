using Dapper;
using ModelLayer.DTO.Request;
using ModelLayer.Entities;
using RepositaryLayer.Context;
using RepositoryLayer.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class ShoppingCartServiceRL(DapperContext context) : IShoppingCartRL
    {
        public List<Object> GetCartBooks(int userId)
        {
            using (var connection = context.CreateConnection())
            {
                var cartBooks = connection.Query<Object>("spGetCartBooks", new { UserId = userId }, commandType: CommandType.StoredProcedure);
                return cartBooks.ToList();
            }
        }

        public List<Object> AddToCart(ShoppingCartRequest cartRequest, int userId)
        {
            string query = "SELECT COUNT(*) FROM ShoppingCart WHERE  bookId = @Id";

            using (var connection = context.CreateConnection())
            {
                var count = connection.ExecuteScalar<int>(query, new {Id = cartRequest.bookId});
                if (count == 0)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@UserId", userId);
                    parameters.Add("@BookId", cartRequest.bookId);
                    parameters.Add("@Quantity", cartRequest.quantity);

                    connection.Execute("spInsertCartItem", parameters, commandType: CommandType.StoredProcedure);
                }
                return GetCartBooks(userId);
            }
        }


        public ShoppingCartRequest UpdateQuantity(int userId, ShoppingCartRequest cartRequest)
        {
            using (var connection = context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", userId);
                parameters.Add("@BookId", cartRequest.bookId);
                parameters.Add("@Quantity", cartRequest.quantity);

                connection.Execute("spUpdateCartItemQuantity", parameters, commandType: CommandType.StoredProcedure);
                return cartRequest;
            }
        }

        public bool DeleteCart(int userId, int cartId)
        {
            using (var connection = context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", userId);
                parameters.Add("@Id", cartId);

                connection.Execute("spDeleteCartItem", parameters, commandType: CommandType.StoredProcedure);
                return IsCartItemExists(userId, cartId);
            }
        }

        private bool IsCartItemExists(int userId, int id)
        {
            string query = "SELECT COUNT(*) FROM ShoppingCart WHERE userId = @UserId AND cartId = @Id";
            using (var connection = context.CreateConnection())
            {
                var count =  connection.ExecuteScalar<int>(query, new { UserId = userId, Id = id });
                return count > 0;
            }
        }
    }
}
