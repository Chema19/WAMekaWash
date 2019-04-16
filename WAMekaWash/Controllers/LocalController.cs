using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Transactions;

namespace WAMekaWash.Controllers
{
    [Authorize]
    [RoutePrefix("wamekawash/v1")]
    public class LocalController : BaseApiController
    {
        [HttpGet]
        [Route("providers/{providerid}/locals")]
        [Route("providers/{providerid}/locals/{localid}")]
        public IHttpActionResult Getlocals(Int32? providerid = null, Int32? localid = null)
        {

            try {
                using (var ts = new TransactionScope())
                {

                    return Ok();
                }
            }
            catch (Exception ex) {
                return Unauthorized();
            }
            
        }

        [HttpGet]
        [Route("locals")]
        public IHttpActionResult Postlocals()
        {
            return Ok(true);
        }
    }
}
