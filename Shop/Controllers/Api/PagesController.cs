using AutoMapper;
using Shop.Dtos;
using Shop.Models;
using Shop.Models.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shop.Controllers.Api
{
    [Authorize(Roles = "Admin")]
    public class PagesController : ApiController
    {
        private ApplicationDbContext _context;

        public PagesController()
        {
            _context = new ApplicationDbContext();
        }

        // Get /api/posts
        public IEnumerable<PagesDto> GetPosts()
        {

            return _context.Pages.ToList().Select(Mapper.Map<Page, PagesDto>);
        }


        public IHttpActionResult GetProducts(int id)
        {
            var page = _context.Pages.SingleOrDefault(b => b.Id == id);

            if (page == null)
                return NotFound();


            return Ok(Mapper.Map<Page, PagesDto>(page));
        }


        [HttpPost]
        public IHttpActionResult CreateProducts(PagesDto pagesDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var page = Mapper.Map<PagesDto, Page>(pagesDto);


            _context.Pages.Add(page);
            _context.SaveChanges();

            pagesDto.Id= page.Id;

            return Created(new Uri(Request.RequestUri + "/" + page.Id), pagesDto);
        }        //PUT /api/Products/1
        [HttpPut]

        public IHttpActionResult UpdateProducts(int id, PagesDto pagesDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var PagesInDb = _context.Pages.SingleOrDefault(p => p.Id == id);

            if (PagesInDb == null)
                return NotFound();

            Mapper.Map(pagesDto, PagesInDb);

            _context.SaveChanges();

            return Ok();
        }
        //DELETE /api/Products/1
        [HttpDelete]
        public IHttpActionResult DeletePages(int id)
        {
            var productInDb = _context.Pages.SingleOrDefault(p => p.Id == id);
            if (productInDb == null)
                return NotFound();

            _context.Pages.Remove(productInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
