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
    public class CategoriesController : ApiController
    {
        private ApplicationDbContext _context;

        public CategoriesController()
        {
            _context = new ApplicationDbContext();
        }

        //GET api/tags
        public IEnumerable<CategoriesDto> GetTags()
        {
            var tags = _context.Categories.ToList().Select(Mapper.Map<Category, CategoriesDto>);

            return tags;
        }

        //GET /api/tags/1
        public IHttpActionResult GetTag(int id)
        {
            var category = _context.Categories.SingleOrDefault(t => t.Id == id);
            if (category == null)
                return NotFound();

            return Ok(Mapper.Map<Category, CategoriesDto>(category));
        }

        //POST api/tags
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateTag(CategoriesDto categoriesDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var category = Mapper.Map<CategoriesDto, Category>(categoriesDto);
            _context.Categories.Add(category);
            _context.SaveChanges();

            categoriesDto.Id = category.Id;

            return Created(new Uri(Request.RequestUri + "/" + category.Id), categoriesDto);
        }

        //PUT /api/tags/1
        [System.Web.Http.HttpPut]
        public IHttpActionResult UpdateTag(int id, CategoriesDto categoriesDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var categoryInDb = _context.Categories.SingleOrDefault(t => t.Id == id);
            if (categoryInDb == null)
                return NotFound();

            Mapper.Map(categoriesDto, categoryInDb);

            _context.SaveChanges();

            return Ok();
        }

        //DELETE /api/tags/1
        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteTag(int id)
        {
            var categoryInDb = _context.Categories.SingleOrDefault(t => t.Id == id);

            if (categoryInDb == null)
                return NotFound();

            _context.Categories.Remove(categoryInDb);
            _context.SaveChanges();

            return Ok();
        }

    }
}
