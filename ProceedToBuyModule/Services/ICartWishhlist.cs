using Microsoft.AspNetCore.Mvc;
using ProceedToBuyModule.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedToBuyModule.Services
{
    public interface ICartWishhlist
    {
        List<Cart> GetCartById(int id);
        Vendor GetVendor(int productId);
        Cart AddProductToCart(Cart cart);
        Task<bool> AddProductToWishlist([FromRoute] int customerid, [FromRoute] int productid);
        Task<bool> RemoveProductFromCart(int uid, int pid);
        Task<bool> RemoveProductFromWishlist(int uid, int pid);
        List<CustomerWishList> GetWishListById(int id);
    }
}
