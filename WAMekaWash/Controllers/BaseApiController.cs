using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WAMekaWash.Models;
using WAMekaWash.Entities;

namespace WAMekaWash.Controllers
{
    public class BaseApiController : ApiController
    {
        protected BDMekawashEntities context { set; get; } = new BDMekawashEntities();
        protected ResultRequest response { set; get; } = new ResultRequest();
    }
}
