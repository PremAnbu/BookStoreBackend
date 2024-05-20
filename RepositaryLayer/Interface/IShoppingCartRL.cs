using ModelLayer.DTO.Request;
using ModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IShoppingCartRL
    {
        List<Object> GetCartBooks(int userId);
        List<Object> AddToCart(ShoppingCartRequest cartRequest, int userId);
        ShoppingCartRequest UpdateQuantity(int userId, ShoppingCartRequest cartRequest);
        bool DeleteCart(int userId, int cartId);
    }
}
