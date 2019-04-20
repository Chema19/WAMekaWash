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
    [RoutePrefix("wamekawash/v8")]
    public class QualificationController : BaseApiController
    {
        [HttpGet]
        [Route("locals/{localid}/qualifications")]
        public IHttpActionResult ListQualifications(Int32? localid)
        {

            try
            {
                using (var ts = new TransactionScope())
                {
                    if (localid.HasValue)
                    {
                        response.Data = context.Qualification.Where(x => x.LocalId == localid).Select(x => new
                        {
                            QualificationId = x.QualificationId,
                            Punctuacion = x.Punctuation,
                            Detail = x.Detail,
                            CustomerId = x.CustomerId,
                            LocalId = localid,
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
        [Route("locals/{localid}/qualifications/{qualificationid}")]
        public IHttpActionResult ReadQualification(Int32? localid, Int32? qualificationid)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    if (qualificationid.HasValue && localid.HasValue)
                    {
                        response.Data = context.Qualification.Where(x => x.QualificationId == qualificationid && x.LocalId == localid).Select(x => new
                        {
                            QualificationId = x.QualificationId,
                            Punctuacion = x.Punctuation,
                            Detail = x.Detail,
                            CustomerId = x.CustomerId,
                         
   LocalId = localid,
                        }).ToList();
                        response.Error = false;
                        response.Message = "Success";
                    }
                    else
                    {
                        response.Data = null;
                        response.Error = true;
                        response.Message = "Error, qualification or local id empty";
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
        [Route("locals/{localid}/qualifications")]
        public IHttpActionResult RegisterQualification(Int32? localid, QualificationEntities model)
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
                        Qualification qualification = new Qualification();

                        if (!localid.HasValue)
                        {
                            response.Data = null;
                            response.Error = true;
                            response.Message = "Error, local empty";
                            return Content(HttpStatusCode.BadRequest, response);
                        }
                        else
                        {

                            context.Qualification.Add(qualification);

                            qualification.QualificationId = model.QualificationId;
                            qualification.Punctuation = model.Punctuation;
                            qualification.Detail = model.Detail;
                            qualification.CustomerId = model.CustomerId;
                            qualification.LocalId = localid;

                            context.SaveChanges();

                            response.Data = null;
                            response.Error = false;
                            response.Message = "Success, saved qualification";
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
