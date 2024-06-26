﻿using BusinessLayer.Interface;
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
    public class ShoppingCartServiceBL(IShoppingCartRL cartBL) : IShoppingCartBL    
    {

        public List<Object> GetCartBooks(int userId)
        {
            return cartBL.GetCartBooks(userId);
        }

        public List<Object> AddToCart(ShoppingCartRequest cartRequest, int userId)
        {
            return  cartBL.AddToCart(cartRequest, userId);
        }

        public ShoppingCartRequest UpdateQuantity(int userId, ShoppingCartRequest cartRequest)
        {
            return  cartBL.UpdateQuantity(userId, cartRequest);
        }

        public bool DeleteCart(int userId, int cartId)
        {
            return  cartBL.DeleteCart(userId, cartId);
        }
    }
}
