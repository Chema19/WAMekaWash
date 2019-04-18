using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WAMekaWash.Entities;
using System.Transactions;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using WAMekaWash.Models;
using WAMekaWash.Logics;
using WAMekaWash.Helpers;


namespace WAMekaWash.Controllers
{
    [Authorize]
    [RoutePrefix("wamekawash/v1")]
    public class CustomersController : BaseApiController
    {
       

        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            var customerFake = "customer-fake";
            return Ok(customerFake);
        }


        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var customersFake = new string[] { "customer-1", "customer-2", "customer-3" };
            return Ok(customersFake);
        }   
    }
}