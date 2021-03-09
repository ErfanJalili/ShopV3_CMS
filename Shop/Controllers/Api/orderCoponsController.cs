using AutoMapper;
using Shop.Dtos;
using Shop.Models;
using Shop.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shop.Controllers.Api
{
    [Authorize(Roles = "Admin")]
    public class OrderCoponsController : ApiController
    {
        private ApplicationDbContext _context;

        public OrderCoponsController()
        {
            _context = new ApplicationDbContext();
        }
        //GET orders
        public IEnumerable<OrderCoponsDto> GetTags()
        {
            var tags = _context.OrderCopons.ToList().Select(Mapper.Map<OrderCopon, OrderCoponsDto>);

            return tags;
        }

        public IHttpActionResult OrderCopons(int id)
        {
            var orderCopon = _context.OrderCopons.SingleOrDefault(t => t.Id == id);
            if (orderCopon == null)
                return NotFound();

            return Ok(Mapper.Map<OrderCopon, OrderCoponsDto>(orderCopon));
        }

        //POST api/tags
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateOrderCoponInDb(OrderCoponsDto orderCoponsDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var orderCopon = Mapper.Map<OrderCoponsDto, OrderCopon>(orderCoponsDto);
            _context.OrderCopons.Add(orderCopon);
            _context.SaveChanges();

            orderCoponsDto.Id = orderCopon.Id;

            return Created(new Uri(Request.RequestUri + "/" + orderCopon.Id), orderCoponsDto);
        }

        //PUT /api/tags/1
        [System.Web.Http.HttpPut]
        public IHttpActionResult UpdateOrderCoponInDb(int id, OrderCoponsDto orderCopnsDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var orderCoponInDb = _context.OrderCopons.SingleOrDefault(t => t.Id == id);
            if (orderCoponInDb == null)
                return NotFound();

            Mapper.Map(orderCopnsDto, orderCoponInDb);

            _context.SaveChanges();

            return Ok();
        }

        //DELETE /api/tags/1
        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteOrderCoponInDb(int id)
        {
            var OrderCoponInDb = _context.OrderCopons.SingleOrDefault(t => t.Id == id);

            if (OrderCoponInDb == null)
                return NotFound();

            _context.OrderCopons.Remove(OrderCoponInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
