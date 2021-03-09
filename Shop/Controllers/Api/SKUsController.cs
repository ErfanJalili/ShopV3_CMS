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
    public class SKUsController : ApiController
    {
        private ApplicationDbContext _context;

        public SKUsController()
        {
            _context = new ApplicationDbContext();
        }

        //GET api/tags
        public IEnumerable<SKUsDto> GetSku()
        {
            var tags = _context.SKUs.ToList().Select(Mapper.Map<SKU, SKUsDto>);

            return tags;
        }

        //GET /api/tags/1
        public IHttpActionResult GetSku(int id)
        {
            var sku = _context.SKUs.SingleOrDefault(t => t.Id == id);
            if (sku == null)
                return NotFound();

            return Ok(Mapper.Map<SKU, SKUsDto>(sku));
        }

        //POST api/tags
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateSku(SKUsDto skusDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var sku = Mapper.Map<SKUsDto, SKU>(skusDto);
            _context.SKUs.Add(sku);
            _context.SaveChanges();

            skusDto.Id = sku.Id;

            return Created(new Uri(Request.RequestUri + "/" + sku.Id), skusDto);
        }

        //PUT /api/tags/1
        [System.Web.Http.HttpPut]
        public IHttpActionResult UpdateSku(int id, SKUsDto skuDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var skuInDb = _context.SKUs.SingleOrDefault(t => t.Id == id);
            if (skuInDb == null)
                return NotFound();

            Mapper.Map(skuDto, skuInDb);

            _context.SaveChanges();

            return Ok();
        }

        //DELETE /api/tags/1
        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteSku(int id)
        {
            var skuInDb = _context.SKUs.SingleOrDefault(t => t.Id == id);

            if (skuInDb == null)
                return NotFound();

            _context.SKUs.Remove(skuInDb);
            _context.SaveChanges();

            return Ok();
        }

    }
}
