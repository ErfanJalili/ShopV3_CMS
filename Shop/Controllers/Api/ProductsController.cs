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
    public class ProductsController : ApiController
    {
        private ApplicationDbContext _context;

        public ProductsController()
        {
            _context = new ApplicationDbContext();
        }
        
      


        // Get /api/products
        public IEnumerable<ProductsDto> GetProducts()
        {

            return _context.Products.ToList().Select(Mapper.Map<Product, ProductsDto>);
        }
        //Get /api/brands/1
        public IHttpActionResult GetProducts(int id)
        {
            var product = _context.Products.SingleOrDefault(b => b.Id == id);

            if (product == null)
                return NotFound();

           
            return Ok(Mapper.Map<Product, ProductsDto>(product));
        }


        [HttpPost]
        public IHttpActionResult CreateProducts(ProductsDto productsDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var product = Mapper.Map<ProductsDto, Product>(productsDto);


            _context.Products.Add(product);
            _context.SaveChanges();

            productsDto.Id = product.Id;

            return Created(new Uri(Request.RequestUri + "/" + product.Id), productsDto);
        }
        //PUT /api/Products/1
        [HttpPut]
       
        public IHttpActionResult UpdateProducts(int id, ProductsDto productsDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var productInDb = _context.Products.SingleOrDefault(p => p.Id == id);

            if (productInDb == null)
                return NotFound();

            Mapper.Map(productsDto, productInDb);

            _context.SaveChanges();

            return Ok();
        }
        //DELETE /api/Products/1
        [HttpDelete]
        
        public IHttpActionResult DeleteProducts(int id)
        {
            var productInDb = _context.Products.SingleOrDefault(p => p.Id == id);
            if (productInDb == null)
                return NotFound();

            _context.Products.Remove(productInDb);
            _context.SaveChanges();

            return Ok();
        }


       

    }




}

