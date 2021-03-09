using AutoMapper;
using Shop.Dtos;
using Shop.Models;
using Shop.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace Shop.Controllers.Api
{
    [Authorize(Roles = "Admin")]
    public class MethodsController : ApiController
    {
        private ApplicationDbContext _context;
        public MethodsController()
        {
            _context = new ApplicationDbContext();
        }
        public IHttpActionResult GetProductBySerch(string query = null)
        {
            var productQuery = _context.Products.Include(p => p.Category);

            if (!String.IsNullOrWhiteSpace(query))
                productQuery = productQuery.Where(p => p.Name.Contains(query));

            var productDto = productQuery
                .ToList()
                .Select(Mapper.Map<Product, ProductsDto>);
            return Ok(productDto);
        }
    }
}
