using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IWishListBL
    {
        List<Object> GetAllWishList(int userId);
        List<Object> AddWishList(int bookId, int userId);
        bool DeleteWishList(int wishListId,int userId);
    }
}
