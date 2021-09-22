using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProceedToBuyModule.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedToBuyModule.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class CartMicroserviceController : ControllerBase
    {
        private readonly CustomerProductDbContext _db;
        public CartMicroserviceController(CustomerProductDbContext db)
        {
            _db = db;
        }
        [Route("api/[controller]")]
        [HttpPost]
        public async Task<ActionResult<Cart>> addProductToCart([FromBody] Cart cart)
        {
            Vendor vendor = _db.VendorsLists.Where(p => p.ProductId == cart.ProductId).Select(s => s.Vendor).FirstOrDefault();
            cart.Vendor = vendor;
            _db.Carts.Add(cart);
            await _db.SaveChangesAsync();
            return Ok(cart);
        }
        [Route("api/[controller]/{customerid}/{productid}")]
        [HttpPost]
        public async Task<IActionResult> addProductToWishlist([FromRoute] int customerid,[FromRoute]int productid)
        {
            CustomerWishList wishList = new CustomerWishList() { CartId = customerid, ProductId = productid, DateAddedToWishList = DateTime.Now };

            _db.CustomerWishLists.Add(wishList);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
