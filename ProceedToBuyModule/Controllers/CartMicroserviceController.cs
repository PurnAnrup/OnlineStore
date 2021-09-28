using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProceedToBuyModule.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProceedToBuyModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartMicroserviceController : ControllerBase
    {
        private readonly CustomerProductDbContext _db;

        public CartMicroserviceController(CustomerProductDbContext db)
        {
            _db = db;
        }

        [HttpGet("GetCartById/{id}")]
        public ActionResult<List<Cart>> GetCartById(int id)
        {
            List<Cart> carts = new List<Cart>();
            carts=  _db.Carts.Where(s=>s.Id==id).ToList();
            foreach (var item in carts)
            {
                item.Vendor = GetVendor(item.ProductId);
            }
            return Ok(carts);

        }

        [HttpGet]
        public Vendor GetVendor(int productId)
        {
            string url = String.Format("https://localhost:44380/Vendor/" + productId);
            Vendor vendor = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var responseTask = client.GetAsync("");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var result1 = result.Content.ReadAsStringAsync().Result;
                    vendor = JsonConvert.DeserializeObject<Vendor>(result1);
                }
            }
            return vendor;
        }

        [HttpPost("AddProductToCart")]
        public async Task<ActionResult<Cart>> AddProductToCart([FromBody] Cart cart)
        {
            Cart c = _db.Carts.Where(s => s.Id == cart.Id && s.ProductId == cart.ProductId).FirstOrDefault();
            if(c!=null)
            {
                return Ok("Item Already in Cart");
            }
            Vendor vendor = GetVendor(cart.ProductId);
            cart.Vendor = vendor;
            _db.Carts.Add(cart);
            await _db.SaveChangesAsync();
            return Ok(cart);
        }

        [HttpPost("PostWishlist/{customerid}/{productid}")]
        public async Task<IActionResult> AddProductToWishlist([FromRoute] int customerid,[FromRoute]int productid)
        {
            CustomerWishList Wishlist = _db.CustomerWishLists.Where(s => s.Id == customerid && s.ProductId == productid).FirstOrDefault();
            if(Wishlist != null)
            {
                return Ok("Item Already Exits in Wishlist");
            }
            CustomerWishList wishList = new CustomerWishList() { Id = customerid, ProductId = productid, DateAddedToWishList = DateTime.Now };
            _db.CustomerWishLists.Add(wishList);
            await _db.SaveChangesAsync();
            return Ok("Inserted Successfully");
        }

        [HttpGet("GetWishList/{id}")]
        public IActionResult GetWishListById(int id)
        {
            List<CustomerWishList> wishLists = _db.CustomerWishLists.Where(s => s.Id == id).ToList<CustomerWishList>();
            return Ok(wishLists);
        }
    }
}
