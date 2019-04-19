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
    [RoutePrefix("wamekawash/v2")]
    public class CustomerController : BaseApiController
    {
        [HttpGet]
        [Route("customers")]
        public IHttpActionResult ListCustomers()
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Customer.Where(x=>x.Status == ConstantHelpers.Status.ACTIVE).Select(x => new
                    {
                        CustomerId = x.CustomerId,
                        Names = x.Names,
                        LastNames = x.LastNames,
                        DocumentIdentity = x.DocumentIdentity,
                        Password = x.Password,
                        BirthdayDate = x.BirthdayDate,
                        Username = x.Username,
                        Status = x.Status,
                        DepartmentId = x.DepartmentId,
                        ProvinceId = x.ProvinceId,
                        DistrictId = x.DistrictId,
                        Phone = x.Phone
                    }).ToList();

                    response.Error = false;
                    response.Message = "Success";
                    ts.Complete();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }
        [HttpGet]
        [Route("customers/{customerid}")]
        public IHttpActionResult ReadCustomer(Int32? customerid)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    if (customerid.HasValue)
                    {
                        response.Data = context.Customer.Where(x => x.CustomerId == customerid && x.Status == ConstantHelpers.Status.ACTIVE).Select(x => new
                        {
                            CustomerId = x.CustomerId,
                            Names = x.Names,
                            LastNames = x.LastNames,
                            DocumentIdentity = x.DocumentIdentity,
                            Password = x.Password,
                            BirthdayDate = x.BirthdayDate,
                            Username = x.Username,
                            Status = x.Status,
                            DepartmentId = x.DepartmentId,
                            ProvinceId = x.ProvinceId,
                            DistrictId = x.DistrictId,
                            Phone = x.Phone
                        }).ToList();
                        response.Error = false;
                        response.Message = "Success";
                    }
                    ts.Complete();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }
        [HttpDelete]
        [Route("customers/{customerid}")]
        public IHttpActionResult DeleteCustomer(Int32? customerid)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    if (customerid.HasValue)
                    {
                        Customer customer = new Customer();
                        customer = context.Customer.FirstOrDefault(x => x.CustomerId == customerid);

                        customer.Status = ConstantHelpers.Status.INACTIVE;

                        context.SaveChanges();
                        response.Data = null;
                        response.Error = false;
                        response.Message = "Success, deleted customer";
                    }
                    ts.Complete();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }
        [HttpPut]
        [Route("customers")]
        public IHttpActionResult UpdateCustomer(CustomerEntities model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    if (model == null)
                    {
                        response.Data = null;
                        response.Error = true;
                        response.Message = "Error, Empty model";
                        return Content(HttpStatusCode.BadRequest, response);
                    }
                    else
                    {
                        Customer customer = new Customer();


                        if (model.CustomerId.HasValue)
                        {
                            customer = context.Customer.FirstOrDefault(x => x.CustomerId == model.CustomerId);

                            customer.Names = model.Names;
                            customer.LastNames = model.LastNames;
                            customer.DocumentIdentity = model.DocumentIdentity;
                            customer.Password = CipherLogic.Cipher(CipherAction.Encrypt, CipherType.UserPassword, model.Password);
                            customer.BirthdayDate = model.Birthday;
                            customer.Username = model.Username;
                            customer.DepartmentId = model.DepartmentId;
                            customer.ProvinceId = model.ProvinceId;
                            customer.DistrictId = model.DistrictId;
                            customer.Phone = model.Phone;

                            context.SaveChanges();

                            response.Data = null;
                            response.Error = false;
                            response.Message = "Success, updated customer";
                        }
                        else
                        {
                            response.Data = null;
                            response.Error = true;
                            response.Message = "Error, empty customer";
                        }
                        ts.Complete();
                    }
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }

    }
}