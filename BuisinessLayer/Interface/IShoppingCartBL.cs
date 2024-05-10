using ModelLayer.DTO.Request;
using ModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IShoppingCartBL
    {
        List<Book> GetCartBooks(int userId);
        List<Book> AddToCart(ShoppingCartRequest cartRequest, int userId);
        ShoppingCartRequest UpdateQuantity(int userId, ShoppingCartRequest cartRequest);
        bool DeleteCart(int userId, int id);
    }
}
