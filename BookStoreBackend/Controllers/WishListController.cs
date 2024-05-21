using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO.Request;
using ModelLayer.DTO;
using System.Security.Claims;
using BusinessLayer.Interface;

namespace BookStoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController(IWishListBL wishList) : ControllerBase
    {

        [HttpPost]
        public ResponceStructure<Object> AddWishList(int bookId)
        {
            try
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = wishList.AddWishList(bookId, userId);
                if (result != null)
                    return new ResponceStructure<Object>(true, "wishList added successfully", result);
                else
                    return new ResponceStructure<Object>(false, "Failed to add wishList.");
            }
            catch (Exception ex)
            {
                return new ResponceStructure<Object>(false, $"Error: {ex.Message}");
            }
        }
        [HttpGet("GetAllWishList")]
        public ResponceStructure<List<Object>> GetAllWishList()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var cartBooks = wishList.GetAllWishList(userId);
            return new ResponceStructure<List<Object>>(true, "Retrieved wishList successfully", cartBooks);
        }
        [HttpDelete("DeleteWishList")]
        public ResponceStructure<bool> DeleteWishList(int wishListId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var isDeleted = wishList.DeleteWishList(wishListId, userId);
            return new ResponceStructure<bool>(true, "Deleted from wishList successfully", true);
        }
    }
}
