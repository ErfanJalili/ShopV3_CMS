using AutoMapper;
using Shop.Dtos;
using Shop.Models;
using Shop.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shop.Controllers.Api
{
    [Authorize(Roles = "Admin")]
    public class OrdersController : ApiController
    {
        private ApplicationDbContext _context;

        public OrdersController()
        {
            _context = new ApplicationDbContext();
        }
        //GET orders
        public IEnumerable<OrdersDto> GetOrders()
        {

            var Statuses = _context.Orders.Include(o=>o.OrderStatus).ToList().Select(Mapper.Map<Order, OrdersDto>);

            return Statuses;
        }

        public IHttpActionResult GetOrders(int id)
        {
            var order = _context.Orders.SingleOrDefault(t => t.Id == id);
            if (order == null)
                return NotFound();

            return Ok(Mapper.Map<Order, OrdersDto>(order));
        }

        //POST api/tags
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateOrder(OrdersDto ordersDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var order = Mapper.Map<OrdersDto, Order>(ordersDtos);
            _context.Orders.Add(order);
            _context.SaveChanges();

            ordersDtos.Id = order.Id;

            return Created(new Uri(Request.RequestUri + "/" + order.Id), ordersDtos);
        }

        //PUT /api/tags/1
        [System.Web.Http.HttpPut]
        public IHttpActionResult UpdateOrder(int id, OrdersDto ordersDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var orderInDb = _context.Orders.SingleOrDefault(t => t.Id == id);
            if (orderInDb == null)
                return NotFound();

            Mapper.Map(ordersDtos, orderInDb);

            _context.SaveChanges();

            return Ok();
        }

        //DELETE /api/tags/1
        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteOrder(int id)
        {
            var orderInDb = _context.Orders.SingleOrDefault(t => t.Id == id);

            if (orderInDb == null)
                return NotFound();

            _context.Orders.Remove(orderInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
