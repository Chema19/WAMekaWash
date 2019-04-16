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
        [HttpPost]
        [Route("customers")]
        public IHttpActionResult PostCustomer(CustomerEntities model)
        {
            var Httpresponse = new HttpResponseMessage();
            try {
                using (var ts = new TransactionScope()) {
                    if (model.Equals("") || model.Equals(null)) {
                        response.Data = null;
                        response.Error = false;
                        response.Message = "Model empty";
                    }
                    else
                    {
                        Customer customer = new Customer();
                        context.Customer.Add(customer);

                        customer.Names = model.Names;
                        customer.LastNames = model.LastNames;
                        customer.DocumentIdentity = model.DocumentIdentity;
                        customer.Password = CipherLogic.Cipher(CipherAction.Encrypt, CipherType.UserPassword, model.Password);
                        customer.BirthdayDate = model.Birthday;
                        customer.Username = model.Username;
                        customer.Status = ConstantHelpers.Status.ACTIVE;
                        customer.DepartmentId = model.DepartementId;
                        customer.ProvinceId = model.ProvinceId;
                        customer.DistrictId = model.DistrictId;

                        context.SaveChanges();
                                                
                        response.Data = null;
                        response.Error = true;
                        response.Message = "Success, saved data";

                      
                    }

                    
                    ts.Complete();
                }
                return Ok(response);
            } catch (Exception ex) {
                return Unauthorized();
            }
        }

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