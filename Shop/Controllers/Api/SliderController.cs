using AutoMapper;
using Shop.Dtos;
using Shop.Models;
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
    public class SliderController : ApiController
    {
        private ApplicationDbContext _context;

        public SliderController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<SliderDto> GetSliders() 
        {
            return _context.Sliders.ToList().Select(Mapper.Map<Slider, SliderDto>);
        }

        //Get /api/brands/1
        public IHttpActionResult GetSliders(int id)
        {
            var slide = _context.Sliders.SingleOrDefault(b => b.Id == id);

            if (slide == null)
                return NotFound();

            return Ok(Mapper.Map<Slider, SliderDto>(slide));
        }

        //POST /api/brands
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateSlide(SliderDto sliderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var slide = Mapper.Map<SliderDto, Slider>(sliderDto);
            _context.Sliders.Add(slide);
            _context.SaveChanges();

            sliderDto.Id = slide.Id;

            return Created(new Uri(Request.RequestUri + "/" + slide.Id), sliderDto);
        }

        //DELETE /api/brands/1
        [HttpDelete]
        public IHttpActionResult DeleteSlider(int id)
        {
            var slideInDb = _context.Sliders.SingleOrDefault(b => b.Id == id);
            if (slideInDb == null)
                return NotFound();

            _context.Sliders.Remove(slideInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
