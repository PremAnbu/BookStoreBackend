using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO.Request;
using ModelLayer.DTO;
using ModelLayer.Entities;
using BusinessLayer.Interface;
using System.Security.Claims;
using RepositoryLayer.Interface;

namespace BookStoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController(IAddressBL addressBL) : ControllerBase
    {
        [HttpPost]
        public ResponceStructure<Object> AddAddress([FromBody] AddressRequest requestDto)
        {
            try{
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = addressBL.AddAddress(requestDto, userId);
                if (result != null)
                    return new ResponceStructure<Object>(true, "Address added successfully");
                else
                    return new ResponceStructure<object>(false, "Failed to add Address.");
            }
            catch (Exception ex){
                return new ResponceStructure<object>(false, $"Error: {ex.Message}");
            }
        }
        [HttpPut("UpdateAddress")]
        public ResponceStructure<Address> UpdateAddress([FromBody] Address addressRequest)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var updatedCartRequest = addressBL.UpdateAddress(userId, addressRequest);
            return new ResponceStructure<Address>(true, "Updated quantity successfully", updatedCartRequest);
        }
        [HttpDelete("DeleteAddress")]
        public ResponceStructure<bool> DeleteAddress(int addressId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var isDeleted = addressBL.DeleteAddress(addressId);
            return new ResponceStructure<bool>(true, "Deleted from Address successfully", true);
        }
        [HttpGet("GetAddress")]
        public ResponceStructure<List<Object>> GetAddress()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var cartBooks = addressBL.GetAddress(userId);
            return new ResponceStructure<List<Object>>(true, "Retrieved order successfully", cartBooks);
        }
    }
}
