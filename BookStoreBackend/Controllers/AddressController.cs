using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO.Request;
using ModelLayer.DTO;
using ModelLayer.Entities;
using BusinessLayer.Interface;
using System.Security.Claims;

namespace BookStoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController(IAddressBL addressBL) : ControllerBase
    {
        [HttpPost]
        public ResponceStructure<Object> AddAddress([FromBody] AddressRequest requestDto)
        {
            try
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = addressBL.AddAddress(requestDto, userId);

                if (result != null)
                {
                    return new ResponceStructure<Object>(true, "Address added successfully", result);
                }
                else
                {
                    return new ResponceStructure<object>(false, "Failed to add Address.");
                }
            }
            catch (Exception ex)
            {
                return new ResponceStructure<object>(false, $"Error: {ex.Message}");
            }
        }
    }
}
