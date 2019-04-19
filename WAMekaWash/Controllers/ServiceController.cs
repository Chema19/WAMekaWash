using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;
using WAMekaWash.Entities;
using WAMekaWash.Helpers;
using WAMekaWash.Models;

namespace WAMekaWash.Controllers
{
    [Authorize]
    [RoutePrefix("wamekawash/v5")]
    public class ServiceController : BaseApiController
    {
        [HttpGet]
        [Route("locals/{localid}/services")]
        public IHttpActionResult ListServices(Int32? localid)
        {

            try
            {
                using (var ts = new TransactionScope())
                {
                    if (localid.HasValue)
                    {
                        response.Data = context.Service.Where(x => x.LocalId == localid && x.Status == ConstantHelpers.Status.ACTIVE).Select(x => new
                        {
                            ServiceId = x.ServiceId,
                            Name = x.Name,
                            Detail = x.Detail,
                            LocalId = x.LocalId,
                            UrlPhoto = x.UrlPhoto,
                            Cost = x.Cost,
                            Status = x.Status,
                        }).ToList();

                        response.Error = false;
                        response.Message = "Success";
                    }
                    else
                    {
                        response.Data = null;
                        response.Error = true;
                        response.Message = "Error, local id empty";
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
        [HttpGet]
        [Route("locals/{localid}/services/{serviceid}")]
        public IHttpActionResult ReadService(Int32? localid, Int32? serviceid)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    if (serviceid.HasValue && localid.HasValue)
                    {
                        response.Data = context.Service.Where(x => x.ServiceId == serviceid && x.LocalId == localid && x.Status == ConstantHelpers.Status.ACTIVE).Select(x => new
                        {
                            ServiceId = x.ServiceId,
                            Name = x.Name,
                            Detail = x.Detail,
                            LocalId = x.LocalId,
                            UrlPhoto = x.UrlPhoto,
                            Cost = x.Cost,
                            Status = x.Status,
                        }).ToList();
                        response.Error = false;
                        response.Message = "Success";
                    }
                    else
                    {
                        response.Data = null;
                        response.Error = true;
                        response.Message = "Error, service or local id empty";
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
        [HttpPost]
        [Route("locals/{localid}/services")]
        public IHttpActionResult RegisterService(Int32? localid, ServiceEntities model)
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
                        Service service = new Service();

                        if (!localid.HasValue)
                        {
                            response.Data = null;
                            response.Error = true;
                            response.Message = "Error, local empty";
                            return Content(HttpStatusCode.BadRequest, response);
                        }
                        else
                        {

                            context.Service.Add(service);

                            service.Name = model.Name;
                            service.Detail = model.Detail;
                            service.LocalId = localid;
                            service.UrlPhoto = model.UrlPhoto;
                            service.Cost = model.Cost;
                            service.Status = ConstantHelpers.Status.ACTIVE;

                            context.SaveChanges();

                            response.Data = null;
                            response.Error = false;
                            response.Message = "Success, saved service";
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
        [HttpDelete]
        [Route("locals/{localid}/services/{serviceid}")]
        public IHttpActionResult DeleteService(Int32? localid, Int32? serviceid)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    if (serviceid.HasValue && localid.HasValue)
                    {
                        Service service = new Service();
                        service = context.Service.FirstOrDefault(x => x.ServiceId == serviceid && x.LocalId == localid);

                        service.Status = ConstantHelpers.Status.INACTIVE;

                        context.SaveChanges();
                        response.Data = null;
                        response.Error = false;
                        response.Message = "Success, deleted service";
                    }
                    else
                    {
                        response.Data = null;
                        response.Error = true;
                        response.Message = "Error, service or local id empty";
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
        [Route("locals/{localid}/services")]
        public IHttpActionResult UpdateService(Int32? localid, ServiceEntities model)
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
                        Service service = new Service();


                        if (model.ServiceId.HasValue && localid.HasValue)
                        {
                            service = context.Service.FirstOrDefault(x => x.LocalId == localid && x.ServiceId == model.ServiceId);

                            service.Name = model.Name;
                            service.Detail = model.Detail;
                            service.LocalId = localid;
                            service.UrlPhoto = model.UrlPhoto;
                            service.Cost = model.Cost;
                            context.SaveChanges();

                            response.Data = null;
                            response.Error = false;
                            response.Message = "Success, updated service";
                        }
                        else
                        {
                            response.Data = null;
                            response.Error = true;
                            response.Message = "Error, Local or provider id empty";
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
