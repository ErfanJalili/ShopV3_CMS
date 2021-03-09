using Microsoft.AspNet.Identity.EntityFramework;
using Shop.Models.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    
        public class ApplicationDbContext : IdentityDbContext<IdentityModels>
        {
        public DbSet<Brand> Brands{ get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Product.Product> Products { get; set; }
        public DbSet<Gallary.Gallary> Gallaries { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<SKU> SKUs { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Post.Post> Posts { get; set; }
        public DbSet<Page.Page> Pages { get; set; }
        public DbSet<Slider.Slider>Sliders{ get; set; }
        public DbSet<Persistence.Models.Customer>Customers{ get; set; }
        public DbSet<Order.OrderCopon> OrderCopons { get; set; }
        public DbSet<Order.OrderStatus> OrderStatuses{ get; set; }
        public DbSet<Order.Order> Orders{ get; set; }
        public DbSet<Order.OrderDetail> OrderDetails{ get; set; }
        public DbSet<Cart.Cart> Carts{ get; set; }
        public ApplicationDbContext()
                : base("DefaultConnection", throwIfV1Schema: false)
            {
            }

            public static ApplicationDbContext Create()
            {
                return new ApplicationDbContext();
            }

        

        //public System.Data.Entity.DbSet<Shop.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
    
}