using Dapper;
using ModelLayer.Entities;
using ModelLayer.GlobalException;
using RepositaryLayer.Context;
using RepositaryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositaryLayer.Service
{
    public class UserServiceRL(DapperContext context):IUserRL
    {
        public int Register(User entity)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@name", entity.name);
                parameters.Add("@email", entity.email); 
                parameters.Add("@password", entity.password);
                parameters.Add("@mobileNumber", entity.mobileNumber);
                var connection = context.CreateConnection();
                return connection.Execute("spRegister", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
             //   _logger.LogError(ex, $"Error occurred while creating user: {ex.Message}");
                throw;
            }
        }

        public User GetUserByEmail(string email)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Email", email);
                Console.WriteLine(email);
                using (IDbConnection connection = context.CreateConnection())
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    var user = connection.QueryFirstOrDefault<User>("spGetUserByEmail", parameters, commandType: CommandType.StoredProcedure);
                    Console.WriteLine(user.email);
                    return user;
                }
            }
            catch (Exception ex)
            {
               // _logger.LogError(ex, $"Error occurred while fetching user by email: {email}");
                throw new UserNotFoundException("User Not Found By Email"); // Re-throw the exception to maintain the error propagation
            }
        }

    }
}
