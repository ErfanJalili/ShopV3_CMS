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
    public class OrderStatusesController : ApiController
    {
        private ApplicationDbContext _context;

        public OrderStatusesController()
        {
            _context = new ApplicationDbContext();
        }
        //GET orders
        public IEnumerable<OrderStatusesDto> GetStatuses()
        {
            var Statuses = _context.OrderStatuses.ToList().Select(Mapper.Map<OrderStatus, OrderStatusesDto>);

            return Statuses;
        }

        public IHttpActionResult OrderStatus(int id)
        {
            var orderStatus = _context.OrderStatuses.SingleOrDefault(t => t.Id == id);
            if (orderStatus == null)
                return NotFound();

            return Ok(Mapper.Map<OrderStatus, OrderStatusesDto>(orderStatus));
        }

        //POST api/tags
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateOrderStatus(OrderStatusesDto orderStatusesDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var orderStatus = Mapper.Map<OrderStatusesDto, OrderStatus>(orderStatusesDto);
            _context.OrderStatuses.Add(orderStatus);
            _context.SaveChanges();

            orderStatusesDto.Id = orderStatus.Id;

            return Created(new Uri(Request.RequestUri + "/" + orderStatus.Id), orderStatusesDto);
        }

        //PUT /api/tags/1
        [System.Web.Http.HttpPut]
        public IHttpActionResult UpdateOrderStatus(int id, OrderStatusesDto orderStatusDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var orderStatusesInDb = _context.OrderStatuses.SingleOrDefault(t => t.Id == id);
            if (orderStatusesInDb == null)
                return NotFound();

            Mapper.Map(orderStatusDto, orderStatusesInDb);

            _context.SaveChanges();

            return Ok();
        }

        //DELETE /api/tags/1
        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteOrderStatuses(int id)
        {
            var orderStatusesInDb = _context.OrderStatuses.SingleOrDefault(t => t.Id == id);

            if (orderStatusesInDb == null)
                return NotFound();

            _context.OrderStatuses.Remove(orderStatusesInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
