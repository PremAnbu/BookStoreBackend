using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using ModelLayer.DTO.Request;
using ModelLayer.Entities;
using System.Security.Claims;

namespace BookStoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController(IShoppingCartBL cartBL) : Controller
    {
        [HttpGet("GetCartBooks")]
        public ResponceStructure<List<Object>> GetCartBooks()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var cartBooks = cartBL.GetCartBooks(userId);
            return new ResponceStructure<List<Object>>(true,"Retrieved books successfully",cartBooks );
        }

        [HttpPost("AddToCart")]
        public ResponceStructure<List<Object>> AddToCart([FromBody] ShoppingCartRequest cartRequest)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var updatedCart =  cartBL.AddToCart(cartRequest, userId);
            return new ResponceStructure<List<Object>>(true,"Added to cart successfully",updatedCart );
        }

        [HttpPut("UpdateQuantity")]
        public ResponceStructure<ShoppingCartRequest> UpdateQuantity([FromBody] ShoppingCartRequest cartRequest)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var updatedCartRequest =  cartBL.UpdateQuantity(userId, cartRequest);
            return new ResponceStructure<ShoppingCartRequest>(true,"Updated quantity successfully",updatedCartRequest );
        }

        [HttpDelete("DeleteCart")]
        public ResponceStructure<bool> DeleteCart(int cartId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var isDeleted =  cartBL.DeleteCart(userId, cartId);
            return new ResponceStructure<bool>(true,"Deleted from cart successfully", true);
        }

    }
}
