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
using System.Data.Entity;

namespace Shop.Controllers.Api
{
    [Authorize(Roles = "Admin")]
    public class OrderDetailsController : ApiController
    {
        private ApplicationDbContext _context;
       

        public OrderDetailsController()
        {
            _context = new ApplicationDbContext();
        }
        //public IEnumerable<OrderDetailsDto> GetOrders()
        //{

        //    var Statuses = _context.OrderDetails.Include(o => o.Order).Include(o => o.Product).ToList();
        //        //.Select(Mapper.Map<OrderDetail, OrderDetailsDto>);

        //    //return Statuses;
        //    return Mapper.Map<IEnumerable<OrderDetailsDto>>(Statuses);
        //}

        public class OrderDetailResponse 
        {
            public int Id { get; set; }
            public string FactorName { get; set; }
            public string ProductName { get; set; }
            public string Quantity { get; set; }
            public double SinglePrice { get; set; }
            public double TotalPrice { get; set; }
            public DateTime? Created { get; set; }
            public DateTime? updated { get; set; }


        }
       
        [HttpGet]
        public IEnumerable<OrderDetailResponse> NewGetOrders()
        {
            //dorost
            // var Statuses = _context.OrderDetails.Include(o => o.Product).Include(o=>o.Order).Where(o=>o.OrderId==o.Order.Id).Select(o=>new OrderDetailResponse { Id =o.Id ,FactorName=o.Order.FactorNumber,Quantity=o.Quantity.ToString(),Created=o.Created_at, updated=o.Updated_at,SinglePrice=o.Product.Price.ToString(),TotalPrice=o.SubTotalPrice.ToString()}).AsEnumerable<OrderDetailResponse>(); 
            var Statuses = _context.OrderDetails
                .Include(o => o.Product)
                .Include(o => o.Order)
                .Where(o => o.Order.Id == o.OrderId)
                .Select(o => new OrderDetailResponse
                { 
                    Id = o.Id,
                    FactorName = o.Order.FactorNumber,
                    Quantity = o.Quantity.ToString(),
                    Created = o.Created_at,
                    updated = o.Updated_at, 
                    SinglePrice = o.Product.Price, 
                    TotalPrice = o.SubTotalPrice ,
                    ProductName=o.Product.Name
                })
                .AsEnumerable<OrderDetailResponse>();
            //.Select(Mapper.Map<OrderDetail, OrderDetailsDto>);
            //var Query1 = from c in _context.Orders
            //             join o in _context.OrderDetails on c.Id equals o.OrderId into OrdersGroup
            //             select new { c };


            //List<string> list = new List<string>();



            //foreach (var item in Statuses)
            //{

            //    list.Add( item.Order.FactorNumber + "," + item.Product.Name + "," + item.Quantity + "," + item.Product.Price + "," + item.Order.TotalPrice + "," + item.Created_at + "," + item.Updated_at + "," + item.Id.ToString() );



            //}
            //String[] str = list.ToArray();
            //return Statuses;
            //return list.ToArray();
            return Statuses.ToList();
        }

        //[Route("api/OrderDetails/jadid")]
        //[HttpGet]
        //public IHttpActionResult jadid()
        //{

        //    var Statuses = from p in _context.Orders
        //                   join a in _context.OrderDetails
        //                   on p.Created_at equals a.Created_at
        //                   select new
        //                   {
        //                       factorNumber = a.Order.FactorNumber,
        //                       singlePrice = a.Product.OffPrice,
        //                       quantity=a.Quantity,
        //                       totalPrice = a.Order.TotalPrice,
                               
        //                   };

        //    return Ok(Statuses.ToArray());
        //}


        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteOrder(int id)
        {
            var orderInDb = _context.OrderDetails.SingleOrDefault(t => t.Id == id);

            if (orderInDb == null)
                return NotFound();

            _context.OrderDetails.Remove(orderInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
