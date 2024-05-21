using Dapper;
using ModelLayer.DTO.Request;
using RepositaryLayer.Context;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class WishListServiceRL(DapperContext context):IWishListRL
    {
        public List<Object> AddWishList(int bookId, int userId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@bookId", bookId);
                parameters.Add("@userId", userId);

                using (var connection = context.CreateConnection())
                {
                    var result = connection.Execute("spAddWishList", parameters, commandType: CommandType.StoredProcedure);
                    if (result > 0)
                        return new List<object> { "WishList Added Successfully" };
                    else
                        throw new Exception("Failed to add WishList.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public List<Object> GetAllWishList(int userId)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {
                    var wishList = connection.Query<object>("spGetAllWishList", new { userId = userId }, commandType: CommandType.StoredProcedure);
                    return wishList.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }


        public bool DeleteWishList(int wishListId, int userId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@wishListId", wishListId);
                parameters.Add("@userId", userId);

                using (var connection = context.CreateConnection())
                {
                    var result = connection.Execute("spDeleteWishList", parameters, commandType: CommandType.StoredProcedure);
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

    }
}
