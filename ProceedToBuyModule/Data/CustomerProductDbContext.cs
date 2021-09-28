using Microsoft.EntityFrameworkCore;
using ProceedToBuyModule.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedToBuyModule
{
    public class CustomerProductDbContext:DbContext
    {

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CustomerWishList> CustomerWishLists { get; set; }

        public CustomerProductDbContext(DbContextOptions<CustomerProductDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>().HasKey(s=> new {s.Id,s.ProductId });
            modelBuilder.Entity<CustomerWishList>().HasKey(u => new { u.Id, u.ProductId });
            
        }
    }
}
