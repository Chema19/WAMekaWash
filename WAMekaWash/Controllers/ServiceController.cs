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
    [RoutePrefix("wamekawash/v4")]
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
                    if (providerid.HasValue)
                    {
                        response.Data = context.Local.Where(x => x.ProviderId == providerid).Select(x => new
                        {
                            LocalId = x.LocalId,
                            Address = x.Address,
                            DistrictId = x.DistrictId,
                            ProvinceId = x.ProvinceId,
                            DepartmentId = x.DepartmentId,
                            ProviderId = x.ProviderId,
                            Punctuation = x.Punctuation,
                            Status = x.Status,
                        }).ToList();

                        response.Error = false;
                        response.Message = "Success";
                    }
                    else
                    {
                        response.Data = null;
                        response.Error = true;
                        response.Message = "Error, Provider id empty";
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
                    if (providerid.HasValue && localid.HasValue)
                    {
                        response.Data = context.Local.Where(x => x.ProviderId == providerid && x.LocalId == localid).Select(x => new
                        {
                            LocalId = x.LocalId,
                            Address = x.Address,
                            DistrictId = x.DistrictId,
                            ProvinceId = x.ProvinceId,
                            DepartmentId = x.DepartmentId,
                            ProviderId = x.ProviderId,
                            Punctuation = x.Punctuation,
                            Status = x.Status,
                        }).ToList();
                        response.Error = false;
                        response.Message = "Success";
                    }
                    else
                    {
                        response.Data = null;
                        response.Error = true;
                        response.Message = "Error, provider or local id empty";
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
                        Local local = new Local();

                        if (!providerid.HasValue)
                        {
                            response.Data = null;
                            response.Error = true;
                            response.Message = "Error, provider empty";
                            return Content(HttpStatusCode.BadRequest, response);
                        }
                        else
                        {

                            context.Local.Add(local);

                            local.Address = model.Address;
                            local.ProviderId = providerid;
                            local.Punctuation = model.Punctuation;
                            local.Status = ConstantHelpers.Status.ACTIVE;
                            local.DepartmentId = model.DepartmentId.Value;
                            local.ProvinceId = model.ProvinceId.Value;
                            local.DistrictId = model.DistrictId.Value;

                            context.SaveChanges();

                            response.Data = null;
                            response.Error = false;
                            response.Message = "Success, saved local";
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
                    if (providerid.HasValue && localid.HasValue)
                    {
                        Local local = new Local();
                        local = context.Local.FirstOrDefault(x => x.ProviderId == providerid && x.LocalId == localid);

                        local.Status = ConstantHelpers.Status.INACTIVE;

                        context.SaveChanges();
                        response.Data = null;
                        response.Error = false;
                        response.Message = "Success, deleted local";
                    }
                    else
                    {
                        response.Data = null;
                        response.Error = true;
                        response.Message = "Error, provider or local id empty";
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
                        Local local = new Local();


                        if (model.LocalId.HasValue && providerid.HasValue)
                        {
                            local = context.Local.FirstOrDefault(x => x.ProviderId == providerid && x.LocalId == model.LocalId);

                            local.Address = model.Address;
                            local.ProviderId = providerid;
                            local.Punctuation = model.Punctuation;
                            local.Status = ConstantHelpers.Status.ACTIVE;
                            local.DepartmentId = model.DepartmentId.Value;
                            local.ProvinceId = model.ProvinceId.Value;
                            local.DistrictId = model.DistrictId.Value;
                            context.SaveChanges();

                            response.Data = null;
                            response.Error = false;
                            response.Message = "Success, updated customer";
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
