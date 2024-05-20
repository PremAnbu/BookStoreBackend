using BusinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO.Request;
using ModelLayer.DTO;
using System.Security.Claims;

namespace BookStoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderBL orderBL) : ControllerBase
    {
        [HttpPost]
        public ResponceStructure<Object> AddOrder([FromBody] OrderRequest requestDto)
        {
            try{
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = orderBL.AddOrder(requestDto, userId);
                if (result != null)
                    return new ResponceStructure<Object>(true, "Order added successfully", result);
                else
                    return new ResponceStructure<Object>(false, "Failed to add Order.");
            }
            catch (Exception ex){
                return new ResponceStructure<Object>(false, $"Error: {ex.Message}");
            }
        }
        [HttpGet("GetOrder")]
        public ResponceStructure<List<Object>> GetOrder()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var cartBooks = orderBL.GetOrder(userId);
            return new ResponceStructure<List<Object>>(true, "Retrieved order successfully", cartBooks);
        }
    }
}
