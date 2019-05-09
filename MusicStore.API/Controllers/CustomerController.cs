using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Service;

namespace MusicStore.API.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService customerService;
        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [EnableQuery]
        [Route("api/customers")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await this.customerService.GetAll();
            return Ok(data);
        }
    }
}