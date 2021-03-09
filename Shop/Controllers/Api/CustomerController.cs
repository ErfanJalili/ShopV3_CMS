using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.Persistence.Dtos;
using Shop.Persistence.Interfaces;
using Shop.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Shop.Controllers.Api
{
    [Authorize(Roles = "Admin")]
    public class CustomerController : ApiController
    {
       
        private readonly ICustomerRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public CustomerController(ICustomerRepository repository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
         
        }

        [HttpGet()]
        public async Task<IHttpActionResult> CustomerById(int id)
        {
            var customer =await repository.GetCustomer(id);


            var vehicleResource = Mapper.Map<Customer, CustomerResource>(customer);

            return Ok(vehicleResource);
        }

       
    }
}
