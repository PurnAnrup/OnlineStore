using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProceedToBuyModule.Model;
using ProceedToBuyModule.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProceedToBuyModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CartMicroserviceController : ControllerBase
    {
        private readonly ICartWishhlist cartWishhlist;

        public CartMicroserviceController(ICartWishhlist cart)
        {
            cartWishhlist = cart;
        }

        [HttpGet("GetCartById/{id}")]
        public ActionResult<List<Cart>> GetCartById(int id)
        {
            List<Cart> carts =   cartWishhlist.GetCartById(id);
            return carts;
        }

        [HttpGet("GetVendor/{productId}")]
        public Vendor GetVendor(int productId)
        {
            return cartWishhlist.GetVendor(productId);
        }

        [HttpPost("AddProductToCart")]
        public ActionResult<Cart> AddProductToCart([FromBody] Cart cart)
        {
            Cart cart1 =  cartWishhlist.AddProductToCart(cart);
            if (cart1 == null)
            {
               return NotFound("Out of Stock");
            }
            else
            {
                return cart1;
            }
        }

        [HttpPost("PostWishlist/{customerid}/{productid}")]
        public IActionResult AddProductToWishlist([FromRoute] int customerid,[FromRoute]int productid)
        {
           bool s=  cartWishhlist.AddProductToWishlist(customerid, productid).Result;
           return Ok("Inserted Successfully"); 
        }

        [HttpDelete("RemoveProductFromCart/{uid}/{pid}")]
        public async Task<IActionResult> RemoveProductFromCart(int uid,int pid)
        {
            bool res = await cartWishhlist.RemoveProductFromCart(uid, pid);
            if (res)
                return Ok("Deleted Successfully");
            else
                return NotFound();
        }


        [HttpDelete("RemoveProductFromWishlist/{uid}/{pid}")]
        public async Task<IActionResult> RemoveProductFromWishlist(int uid, int pid)
        {
            bool res = await cartWishhlist.RemoveProductFromWishlist(uid, pid);
            if (res)
                return Ok("Deleted Successfully");
            else
                return NotFound();
        }

        [HttpGet("GetWishList/{id}")]
        public ActionResult<List<CustomerWishList>> GetWishListById(int id)
        {
            List<CustomerWishList> wishList = cartWishhlist.GetWishListById(id);
            return Ok(wishList);
        }
    }
}
