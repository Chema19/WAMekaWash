using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Transactions;
using WAMekaWash.Entities;
using WAMekaWash.Models;
using WAMekaWash.Helpers;

namespace WAMekaWash.Controllers
{
    [Authorize]
    [RoutePrefix("wamekawash/v4")]
    public class LocalController : BaseApiController
    {
        [HttpGet]
        [Route("providers/{providerid}/locals")]
        public IHttpActionResult ListLocals(Int32? providerid)
        {

            try {
                using (var ts = new TransactionScope())
                {
                    if (providerid.HasValue)
                    {
                        response.Data = context.Local.Where(x => x.ProviderId == providerid && x.Status == ConstantHelpers.Status.ACTIVE).Select(x => new
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
            catch (Exception ex) {
                return Unauthorized();
            }
            
        }
        [HttpGet]
        [Route("providers/{providerid}/locals/{localid}")]
        public IHttpActionResult ReadLocal(Int32? providerid, Int32? localid)
        {

            try
            {
                using (var ts = new TransactionScope())
                {
                    if (providerid.HasValue && localid.HasValue)
                    {
                        response.Data = context.Local.Where(x => x.ProviderId == providerid && x.LocalId == localid && x.Status == ConstantHelpers.Status.ACTIVE).Select(x => new
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
                    else {
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
        [Route("providers/{providerid}/locals")]
        public IHttpActionResult RegisterLocal(Int32? providerid, LocalEntities model)
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
        [Route("providers/{providerid}/locals/{localid}")]
        public IHttpActionResult DeleteLocal(Int32? providerid,Int32? localid)
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
                    else {
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
        [Route("providers/{providerid}/locals")]
        public IHttpActionResult UpdateLocal(Int32? providerid, LocalEntities model)
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
                            local.DepartmentId = model.DepartmentId.Value;
                            local.ProvinceId = model.ProvinceId.Value;
                            local.DistrictId = model.DistrictId.Value;
                            context.SaveChanges();

                            response.Data = null;
                            response.Error = false;
                            response.Message = "Success, updated local";
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
