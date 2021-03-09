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
using System.Web.Mvc;

namespace Shop.Controllers.Api
{
    [System.Web.Http.Authorize(Roles = "Admin")]
    public class TagsController : ApiController
    {
        private ApplicationDbContext _context;

        public TagsController()
        {
            _context = new ApplicationDbContext();
        }

        //GET api/tags
        public IEnumerable<TagDto> GetTags()
        {
           var tags = _context.Tags.ToList().Select(Mapper.Map<Tag,TagDto>);

            return tags;
        }

        //GET /api/tags/1
        public IHttpActionResult GetTag(int id)
        {
            var tag = _context.Tags.SingleOrDefault(t => t.Id == id);
            if (tag == null)
                return NotFound();

            return Ok( Mapper.Map<Tag,TagDto>(tag));
        }

        //POST api/tags
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateTag(TagDto tagDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tag = Mapper.Map<TagDto, Tag>(tagDto);
            _context.Tags.Add(tag);
            _context.SaveChanges();

            tagDto.Id = tag.Id;

            return Created(new Uri(Request.RequestUri+"/"+tag.Id),tagDto);
        }

        //PUT /api/tags/1
        [System.Web.Http.HttpPut]
        public IHttpActionResult UpdateTag(int id, TagDto tagDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();          

            var TagInDb = _context.Tags.SingleOrDefault(t => t.Id == id);
            if (TagInDb == null)
                return NotFound();

            Mapper.Map(tagDto, TagInDb);

            _context.SaveChanges();

            return Ok();
        }

        //DELETE /api/tags/1
        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteTag(int id)
        {
            var TagInDb = _context.Tags.SingleOrDefault(t => t.Id == id);

            if (TagInDb == null)
                return NotFound();

            _context.Tags.Remove(TagInDb);
            _context.SaveChanges();

            return Ok();
        }

    }
}
