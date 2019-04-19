using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;
using WAMekaWash.Entities;
using WAMekaWash.Helpers;
using WAMekaWash.Logics;
using WAMekaWash.Models;

namespace WAMekaWash.Controllers
{
    [Authorize]
    [RoutePrefix("wamekawash/v3")]
    public class ProviderController : BaseApiController
    {
        [HttpGet]
        [Route("providers")]
        public IHttpActionResult ListProviders()
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Provider.Select(x => new
                    {
                        ProviderId = x.ProviderId,
                        BusinessName = x.BusinessName,
                        RUC = x.RUC,
                        Telephone = x.Telephone,
                        Email = x.Email,
                        CategoryId = x.CategoryId,
                        Password = x.Password,
                        Status = x.Status
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
        [Route("providers/{providerid}")]
        public IHttpActionResult ReadProvider(Int32? providerid)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    if (providerid.HasValue)
                    {
                        response.Data = context.Provider.Where(x => x.ProviderId == providerid).Select(x => new
                        {
                            ProviderId = x.ProviderId,
                            BusinessName = x.BusinessName,
                            RUC = x.RUC,
                            Telephone = x.Telephone,
                            Email = x.Email,
                            CategoryId = x.CategoryId,
                            Password = x.Password,
                            Status = x.Status
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
        [Route("providers/{providerid}")]
        public IHttpActionResult DeleteProvider(Int32? providerid)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    if (providerid.HasValue)
                    {
                        Provider provider = new Provider();
                        provider = context.Provider.FirstOrDefault(x => x.ProviderId == providerid);

                        provider.Status = ConstantHelpers.Status.INACTIVE;

                        context.SaveChanges();
                        response.Data = null;
                        response.Error = false;
                        response.Message = "Success, deleted provider";
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
        [Route("providers")]
        public IHttpActionResult UpdateProvider(ProviderEntities model)
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
                        Provider provider = new Provider();


                        if (model.ProviderId.HasValue)
                        {
                            provider = context.Provider.FirstOrDefault(x => x.ProviderId == model.ProviderId);
                        }
                        
                        provider.BusinessName = model.BusinessName;
                        provider.RUC = model.Ruc;
                        provider.Telephone = model.Telephone;
                        provider.Email = model.Email;
                        provider.CategoryId = model.CategoryId;
                        provider.Password = CipherLogic.Cipher(CipherAction.Encrypt, CipherType.UserPassword, model.Password);

                        context.SaveChanges();

                        response.Data = null;
                        response.Error = false;
                        response.Message = "Success, updated provider";

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
