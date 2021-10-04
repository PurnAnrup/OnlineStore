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
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(CartMicroserviceController));
        private readonly ICartWishhlist cartWishhlist;

        public CartMicroserviceController(ICartWishhlist cart)
        {
            cartWishhlist = cart;
        }

        /// <summary>
        /// Get Cart By Id
        /// </summary>
        /// <return>
        /// Returns Cart By Id
        /// </return>
        /// <remarks>
        /// Sample request
        /// GET /api/CartMicroService
        /// 
        /// </remarks>
        /// <response code="200"> Returns Cart By Id</response>
        [HttpGet("GetCartById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public ActionResult<List<Cart>> GetCartById(int id)
        {
            _log4net.Info("Logger Initiated");
            List<Cart> carts =   cartWishhlist.GetCartById(id);
            _log4net.Info("Returned Carts Successfully");
            return carts;
        }


        /// <summary>
        /// Get Vendor
        /// </summary>
        /// <return>
        /// Returns Vendor 
        /// </return>
        /// <remarks>
        /// Sample request
        /// GET /api/CartMicroService
        /// 
        /// </remarks>
        /// <response code="200">Returns Vendor</response>
        [HttpGet("GetVendor/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public Vendor GetVendor(int productId)
        {
            _log4net.Info("Returned Vendors Successfully");
            return cartWishhlist.GetVendor(productId);
        }

        /// <summary>
        ///Add Product To Cart
        /// </summary>
        /// <return>
        /// Returns Number of Items Added to the cart
        /// </return>
        /// <remarks>
        /// Sample request
        /// POST / api/ProductMicroServices
        /// 
        /// </remarks>
        /// <response code="201">Successfully added product to the cart</response>
        [HttpPost("AddProductToCart")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public ActionResult<Cart> AddProductToCart([FromBody] Cart cart)
        {
            _log4net.Info("Logger Initiated");
            Cart cart1 =  cartWishhlist.AddProductToCart(cart);
            if (cart1 == null)
            {
                _log4net.Error("Out Of Stock");
                return NotFound("Out of Stock");
            }
            else
            {
                _log4net.Info("Product added successfully to the cart");
                return cart1;
            }
        }

        /// <summary>
        ///Add Product To Wishlist
        /// </summary>
        /// <return>
        /// Returns Number of Items Added to the Wishlist
        /// </return>
        /// <remarks>
        /// Sample request
        /// POST / api/ProductMicroServices
        /// 
        /// </remarks>
        /// <response code="201">Successfully added product to the Wishlist</response>
        [HttpPost("PostWishlist/{customerid}/{productid}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public IActionResult AddProductToWishlist([FromRoute] int customerid,[FromRoute]int productid)
        {
            _log4net.Info("Logger Initiated");
            bool s=  cartWishhlist.AddProductToWishlist(customerid, productid).Result;
            _log4net.Info("Product Added to the wishlist successfully");
            return Ok("Inserted Successfully"); 
        }

        /// <summary>
        ///Remove Product From Cart
        /// </summary>
        /// <return>
        /// Returns status message
        /// </return>
        /// <remarks>
        /// Sample request
        /// POST / api/ProductMicroServices
        /// 
        /// </remarks>
        /// <response code="204">Successfully deleted</response>
        [HttpDelete("RemoveProductFromCart/{uid}/{pid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RemoveProductFromCart(int uid,int pid)
        {
            _log4net.Info("Logger Initiated");
            bool res = await cartWishhlist.RemoveProductFromCart(uid, pid);
            if (res)
            {
                _log4net.Info("Removed Product From Cart");
                return Ok("Deleted Successfully");
            }
            else
            {
                _log4net.Error("Failed");
                return NotFound();
            }
        }


        /// <summary>
        ///Remove Product From Wishlist
        /// </summary>
        /// <return>
        /// Returns status message
        /// </return>
        /// <remarks>
        /// Sample request
        /// POST / api/ProductMicroServices
        /// 
        /// </remarks>
        /// <response code="204">Successfully deleted</response>
        [HttpDelete("RemoveProductFromWishlist/{uid}/{pid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RemoveProductFromWishlist(int uid, int pid)
        {
            _log4net.Info("Logger Initiated");
            bool res = await cartWishhlist.RemoveProductFromWishlist(uid, pid);
            if (res)
            {
                _log4net.Info("Removed Product From Wishlist");
                return Ok("Deleted Successfully");
            }
            else
            {
                _log4net.Error("Failed");
                return NotFound();
            }
        }

        /// <summary>
        /// Get WishList By Id
        /// </summary>
        /// <return>
        /// Returns WishList By Id 
        /// </return>
        /// <remarks>
        /// Sample request
        /// GET /api/CartMicroService
        /// 
        /// </remarks>
        /// <response code="200">Returns WishList By Id</response>
        [HttpGet("GetWishList/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public ActionResult<List<CustomerWishList>> GetWishListById(int id)
        {
            _log4net.Info("Logger Initiated");
            List<CustomerWishList> wishList = cartWishhlist.GetWishListById(id);
            _log4net.Info("Get Wishlist by Id");
            return Ok(wishList);
        }
    }
}
