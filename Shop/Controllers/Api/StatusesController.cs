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
    public class StatusesController : ApiController
    {
        private ApplicationDbContext _context;

        public StatusesController()
        {
            _context = new ApplicationDbContext();
        }

        //GET api/tags
        public IEnumerable<StatusesDto> GetTags()
        {
            var status = _context.Statuses.ToList().Select(Mapper.Map<Status, StatusesDto>);

            return status;
        }

        //GET /api/tags/1
        public IHttpActionResult GetTag(int id)
        {
            var status = _context.Statuses.SingleOrDefault(t => t.Id == id);
            if (status == null)
                return NotFound();

            return Ok(Mapper.Map<Status, StatusesDto>(status));
        }

        //POST api/tags
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateTag(StatusesDto statusesDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var status = Mapper.Map<StatusesDto, Status>(statusesDto);
            _context.Statuses.Add(status);
            _context.SaveChanges();

            statusesDto.Id = status.Id;

            return Created(new Uri(Request.RequestUri + "/" + status.Id), statusesDto);
        }

        //PUT /api/tags/1
        [System.Web.Http.HttpPut]
        public IHttpActionResult UpdateTag(int id, StatusesDto statusesDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var StatusInDb = _context.Statuses.SingleOrDefault(t => t.Id == id);
            if (StatusInDb == null)
                return NotFound();

            Mapper.Map(statusesDto, StatusInDb);

            _context.SaveChanges();

            return Ok();
        }

        //DELETE /api/tags/1
        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteTag(int id)
        {
            var StatusInDb = _context.Statuses.SingleOrDefault(t => t.Id == id);

            if (StatusInDb == null)
                return NotFound();

            _context.Statuses.Remove(StatusInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
