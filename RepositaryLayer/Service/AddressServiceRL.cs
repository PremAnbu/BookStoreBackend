using Dapper;
using ModelLayer.DTO.Request;
using ModelLayer.Entities;
using RepositaryLayer.Context;
using RepositoryLayer.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class AddressServiceRL(DapperContext context) : IAddressRL
    {
        public string AddAddress(AddressRequest address,int userId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@address", address.address);
                parameters.Add("@city", address.city);
                parameters.Add("@state", address.state);
                parameters.Add("@type", address.type);
                parameters.Add("@userId", userId);

                var connection = context.CreateConnection();
                var result = connection.Execute("spAddAddress", parameters, commandType: CommandType.StoredProcedure);

                if (result > 0)
                {
                    return "Address Added Successfully";
                }
                else
                {
                    throw new Exception("Failed to add book.");
                    //return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        
    }
        
    }
    
}
