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

namespace Shop.Controllers.Api
{
    [Authorize(Roles = "Admin")]
    public class BrandsController : ApiController
    {
        private ApplicationDbContext _context;

        public BrandsController()
        {
            _context = new ApplicationDbContext();
        }
        // Get /api/brands
        public IEnumerable<BrandsDto> GetBrands()
        {
            return _context.Brands.ToList().Select(Mapper.Map<Brand, BrandsDto>);
        }
        //Get /api/brands/1
        public IHttpActionResult GetBrand(int id)
        {
            var brand = _context.Brands.SingleOrDefault(b => b.Id == id);

            if (brand == null)
                return NotFound();

            return Ok(Mapper.Map<Brand, BrandsDto>(brand));
        }

        //POST /api/brands
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateTag(TagDto brandDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var brand = Mapper.Map<TagDto, Brand>(brandDto);
            _context.Brands.Add(brand);
            _context.SaveChanges();

            brandDto.Id = brand.Id;

            return Created(new Uri(Request.RequestUri + "/" + brand.Id), brandDto);
        }

        //DELETE /api/brands/1
        [HttpDelete]
        public IHttpActionResult DeleteBrand(int id)
        {
            var brandInDb = _context.Brands.SingleOrDefault(b => b.Id == id);
            if (brandInDb == null)
                return NotFound();

            _context.Brands.Remove(brandInDb);
            _context.SaveChanges();

            return Ok();
        }

    }
}
