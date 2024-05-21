using BusinessLayer.Interface;
using ModelLayer.DTO.Request;
using ModelLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class WishListServiceBL(IWishListRL wishListRL):IWishListBL
    {
        public List<Object> GetAllWishList(int userId)
        {
            return wishListRL.GetAllWishList(userId);
        }
        public List<Object> AddWishList(int bookId, int userId)
        {
            return wishListRL.AddWishList(bookId, userId);
        }
        public bool DeleteWishList(int wishListId,int userId)
        {
            return wishListRL.DeleteWishList(wishListId,userId);
        }
    }
}
