using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProceedToBuyModule.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProceedToBuyModule.Services
{
    public class CartWishlistRepository : ICartWishhlist
    {
        private readonly CustomerProductDbContext _db;
        public CartWishlistRepository(CustomerProductDbContext db)
        {
            _db = db;
        }

        public  Cart AddProductToCart(Cart cart)
        {
            Vendor vendor = GetVendor(cart.ProductId);
            if (vendor == null)
            {
                return null;
            }
            cart.Vendor = vendor;
            _db.Carts.Add(cart);
             _db.SaveChanges();
            return cart;
        }

        public async Task<bool> AddProductToWishlist(int customerid,int productid)
        {
            CustomerWishList Wishlist = _db.CustomerWishLists.Where(s => s.Id == customerid && s.ProductId == productid).FirstOrDefault();

            CustomerWishList wishList = new CustomerWishList() { Id = customerid, ProductId = productid, DateAddedToWishList = DateTime.Now };
            _db.CustomerWishLists.Add(wishList);
            await _db.SaveChangesAsync();
            return true;
        }

        public List<Cart> GetCartById(int id)
        {
            List<Cart> carts = new List<Cart>();
            carts =  _db.Carts.Where(s => s.Id == id).ToList();
            foreach (var item in carts)
            {
                item.Vendor = GetVendor(item.ProductId);
            }
            return carts;
        }

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

        public List<CustomerWishList> GetWishListById(int id)
        {
            List<CustomerWishList> wishLists = _db.CustomerWishLists.Where(s => s.Id == id).ToList<CustomerWishList>();
            return wishLists;
        }

        public async Task<bool> RemoveProductFromCart(int uid, int pid)
        {
            Cart cart = await _db.Carts.Where(s => s.Id == uid && s.ProductId == pid).FirstOrDefaultAsync();
            if (cart == null)
            {
                return false;
            }
            _db.Carts.Remove(cart);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveProductFromWishlist(int uid, int pid)
        {
            CustomerWishList wishList = await _db.CustomerWishLists.Where(s => s.Id == uid && s.ProductId == pid).FirstOrDefaultAsync();
            if (wishList == null)
            {
                return false;
            }
            _db.CustomerWishLists.Remove(wishList);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
