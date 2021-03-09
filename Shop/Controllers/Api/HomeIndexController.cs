using AutoMapper;
using Shop.Dtos;
using Shop.Models;
using Shop.Models.Product;
using Shop.Models.Slider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shop.Controllers.Api
{
    [Authorize(Roles = "Admin")]
    public class HomeIndexController : ApiController
    {
        private ApplicationDbContext _context;

        public HomeIndexController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<HomeIndexDto> GetProductsHaSlider()
        {
          

            return _context.Products.Where(p => p.IsSlider).ToList().Select(Mapper.Map<Product, HomeIndexDto>); ;
        }


        //Get /api/brands/1
        public IHttpActionResult GetProducts(int id)
        {
            var product = _context.Products.SingleOrDefault(b => b.Id == id);

            if (product == null)
                return NotFound();


            return Ok(Mapper.Map<Product, HomeIndexDto>(product));
        }

    }
}
