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
        public string AddAddress(AddressRequest address, int userId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@name", address.name);
                parameters.Add("@mobileNumber", address.mobileNumber);
                parameters.Add("@address", address.address);
                parameters.Add("@city", address.city);
                parameters.Add("@state", address.state);
                parameters.Add("@type", address.type);
                parameters.Add("@userId", userId);

                using (var connection = context.CreateConnection())
                {
                    var result = connection.Execute("spAddAddress", parameters, commandType: CommandType.StoredProcedure);
                    if (result > 0)
                        return "Address Added Successfully";
                    else
                        throw new Exception("Failed to add address.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public List<Object> GetAddress(int userId)
        {
            using (var connection = context.CreateConnection())
            {
                var addresses = connection.Query<Object>("spGetAllAddresses", new { userId = userId }, commandType: CommandType.StoredProcedure);
                return (List<object>)addresses;
            }
        }

        public Address UpdateAddress(int userId, Address addressRequest)
        {
            using (var connection = context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@addressId", addressRequest.addressId);
                parameters.Add("@name", addressRequest.name);
                parameters.Add("@mobileNumber", addressRequest.mobileNumber);
                parameters.Add("@address", addressRequest.address);
                parameters.Add("@city", addressRequest.city);
                parameters.Add("@state", addressRequest.state);
                parameters.Add("@addressType", addressRequest.type);
                parameters.Add("@userId", userId);

                connection.Execute("spUpdateAddress", parameters, commandType: CommandType.StoredProcedure);
                return addressRequest;
            }
        }


        public bool DeleteAddress(int addressId)
        {
            using (var connection = context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@addressId", addressId);

                var result = connection.Execute("spDeleteAddress", parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

    }

}
