using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using WAMekaWash.Models;
using WAMekaWash.Entities;
using WAMekaWash.Logics;
using System.Transactions;
using WAMekaWash.Helpers;

namespace WAMekaWash.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("wamekawash/v1")]
    public class LoginController : BaseApiController
    {
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(LoginRequest model)
        {
            try
            {
                if (model == null)
                {
                    response.Data = null;
                    response.Error = true;
                    response.Message = "Error, empty model";
                    //throw new HttpResponseException(HttpStatusCode.BadRequest);
                    return Content(HttpStatusCode.BadRequest, response);
                }
                if (String.IsNullOrEmpty(model.Username) || String.IsNullOrEmpty(model.Password)) {
                    response.Data = null;
                    response.Error = true;
                    response.Message = "Error, empty data";
                    return Content(HttpStatusCode.BadRequest, response);
                }

                Customer customer = context.Customer.FirstOrDefault(x => x.Username == model.Username);

                String password =  CipherLogic.Cipher(CipherAction.Encrypt, CipherType.UserPassword, model.Password);

                bool isCredentialValid = (password == customer.Password);
                
                if (isCredentialValid)
                {
                    var token = TokenGenerator.GenerateTokenJwt(model.Username);
                    response.Data = token;
                    response.Error = false;
                    response.Message = "Success";
                    return Ok(response);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex) {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("customers")]
        public IHttpActionResult RegisterCustomer(CustomerEntities model)
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
                        
                        if (context.Customer.FirstOrDefault(x => x.Username == model.Username) != null)
                        {
                            response.Data = null;
                            response.Error = true;
                            response.Message = "Error, Existing customer";
                            return Content(HttpStatusCode.BadRequest,response);
                        }
                        else
                        {

                            context.Customer.Add(customer);

                            customer.Names = model.Names;
                            customer.LastNames = model.LastNames;
                            customer.DocumentIdentity = model.DocumentIdentity;
                            customer.Password = CipherLogic.Cipher(CipherAction.Encrypt, CipherType.UserPassword, model.Password);
                            customer.BirthdayDate = model.Birthday;
                            customer.Username = model.Username;
                            customer.Status = ConstantHelpers.Status.ACTIVE;
                            customer.DepartmentId = model.DepartmentId;
                            customer.ProvinceId = model.ProvinceId;
                            customer.DistrictId = model.DistrictId;
                            customer.Phone = model.Phone;

                            context.SaveChanges();

                            response.Data = null;
                            response.Error = false;
                            response.Message = "Success, saved customer";
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
        [HttpPost]
        [Route("providers")]
        public IHttpActionResult RegisterProvider(ProviderEntities model) {
            try {
                using (var ts = new TransactionScope()) {
                    if (model == null)
                    {
                        response.Data = null;
                        response.Error = true;
                        response.Message = "Error, Empty model";
                        return Content(HttpStatusCode.BadRequest, response);
                    }
                    else {
                        var provider = new Provider();
                        ;

                        if (context.Provider.FirstOrDefault(x => x.RUC == model.Ruc) != null)
                        {
                            response.Data = null;
                            response.Error = true;
                            response.Message = "Error, Existing provider";
                            return Content(HttpStatusCode.BadRequest, response);
                        }
                        else {
                            context.Provider.Add(provider);

                            provider.BusinessName = model.BusinessName;
                            provider.RUC = model.Ruc;
                            provider.Telephone = model.Telephone;
                            provider.Email = model.Email;
                            provider.Status = ConstantHelpers.Status.ACTIVE;
                            provider.CategoryId = model.CategoryId;
                            provider.Password = CipherLogic.Cipher(CipherAction.Encrypt, CipherType.UserPassword, model.Password);
                            provider.Logo = model.Logo;
                            provider.Description = model.Description;

                            context.SaveChanges();

                            response.Data = null;
                            response.Error = false;
                            response.Message = "Success, saved provider";

                        }
                    }
                    ts.Complete();
                }
                return Ok(response);
            } catch (Exception ex){
                return Unauthorized();
            }
        }
        [HttpGet]
        [Route("categories")]
        public IHttpActionResult ListCategories()
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Category.Select(x => new { CategoryId = x.CategoryId, Name = x.Name }).ToList(); ;
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
        [Route("departments")]
        public IHttpActionResult ListDepartments()
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Department.Select(x => new { DepartmentId = x.DepartmentId, Name = x.Name }).ToList(); ;
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
        [Route("provinces/{departmentid}")]
        public IHttpActionResult ListProvinces(Int32? departmentid)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Province.Where(x=>x.DepartmentId == departmentid).Select(x => new { ProvinceId = x.ProvinceId, Name = x.Name }).ToList(); ;
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
        [Route("districts/{provinceid}")]
        public IHttpActionResult ListDistricts(Int32? provinceid)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.District.Where(x=>x.ProvinceId == provinceid).Select(x => new { DistrictId = x.DistrictId, Name = x.Name }).ToList(); ;
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
        [Route("brands")]
        public IHttpActionResult ListBrands()
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Brand.Select(x => new { BrandId = x.BrandId, Name = x.Name }).ToList(); ;
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
    }
}