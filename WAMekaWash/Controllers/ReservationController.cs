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
    [RoutePrefix("wamekawash/v6")]
    public class ReservationController : BaseApiController
    {
        [HttpGet]
        [Route("locals/{localid}/reservations")]
        public IHttpActionResult ListReservationsProvider(Int32? localid)
        {

            try
            {
                using (var ts = new TransactionScope())
                {
                    if (localid.HasValue)
                    {
                        response.Data = context.Reservation.Where(x => x.LocalId == localid).Select(x => new
                        {
                            ReservationId = x.ReservationId,
                            CustomerId = x.CustomerId,
                            LocalId = x.LocalId,
                            ServiceId = x.ServiceId,
                            Schedule = x.Schedule,
                            Detail = x.Detail,
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
        [Route("locals/{localid}/reservations/{reservationid}")]
        public IHttpActionResult ReadReservationProvider(Int32? localid, Int32? reservationid)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    if (reservationid.HasValue && localid.HasValue)
                    {
                        response.Data = context.Reservation.Where(x => x.ReservationId == reservationid && x.LocalId == localid).Select(x => new
                        {
                            ReservationId = x.ReservationId,
                            CustomerId = x.CustomerId,
                            LocalId = x.LocalId,
                            ServiceId = x.ServiceId,
                            Schedule = x.Schedule,
                            Detail = x.Detail,
                            Status = x.Status,
                        }).ToList();
                        response.Error = false;
                        response.Message = "Success";
                    }
                    else
                    {
                        response.Data = null;
                        response.Error = true;
                        response.Message = "Error, reservation or local id empty";
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
        [Route("customers/{customerid}/reservations")]
        public IHttpActionResult ListReservationsCustomer(Int32? customerid)
        {

            try
            {
                using (var ts = new TransactionScope())
                {
                    if (customerid.HasValue)
                    {
                        response.Data = context.Reservation.Where(x => x.CustomerId == customerid).Select(x => new
                        {
                            ReservationId = x.ReservationId,
                            CustomerId = x.CustomerId,
                            LocalId = x.LocalId,
                            ServiceId = x.ServiceId,
                            Schedule = x.Schedule,
                            Detail = x.Detail,
                            Status = x.Status,
                        }).ToList();

                        response.Error = false;
                        response.Message = "Success";
                    }
                    else
                    {
                        response.Data = null;
                        response.Error = true;
                        response.Message = "Error, customer id empty";
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
        [Route("customers/{customerid}/reservations/{reservationid}")]
        public IHttpActionResult ReadReservationCustomer(Int32? customerid, Int32? reservationid)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    if (reservationid.HasValue && customerid.HasValue)
                    {
                        response.Data = context.Reservation.Where(x => x.ReservationId == reservationid && x.CustomerId == customerid).Select(x => new
                        {
                            ReservationId = x.ReservationId,
                            CustomerId = x.CustomerId,
                            LocalId = x.LocalId,
                            ServiceId = x.ServiceId,
                            Schedule = x.Schedule,
                            Detail = x.Detail,
                            Status = x.Status,
                        }).ToList();
                        response.Error = false;
                        response.Message = "Success";
                    }
                    else
                    {
                        response.Data = null;
                        response.Error = true;
                        response.Message = "Error, reservation or customer id empty";
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
        [Route("customers/{customerid}/reservations")]
        public IHttpActionResult RegisterServiceCustomer(Int32? customerid, ReservationEntities model)
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
                        Reservation reservation = new Reservation();

                        if (!customerid.HasValue)
                        {
                            response.Data = null;
                            response.Error = true;
                            response.Message = "Error, local empty";
                            return Content(HttpStatusCode.BadRequest, response);
                        }
                        else
                        {

                            context.Reservation.Add(reservation);

                            reservation.CustomerId = customerid.Value;
                            reservation.LocalId = model.LocalId;
                            reservation.ServiceId = model.ServiceId;
                            reservation.Schedule = model.Schedule;
                            reservation.Detail = model.Detail;
                            reservation.Status = ConstantHelpers.Status.ACTIVE;

                            context.SaveChanges();

                            response.Data = null;
                            response.Error = false;
                            response.Message = "Success, saved reservation";
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
        [Route("customers/{customerid}/reservations/{reservationid}")]
        public IHttpActionResult DeleteServiceCustomer(Int32? customerid, Int32? reservationid)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    if (reservationid.HasValue && customerid.HasValue)
                    {
                        Reservation reservation = new Reservation();
                        reservation = context.Reservation.FirstOrDefault(x => x.ReservationId == reservationid && x.CustomerId == customerid);

                        reservation.Status = ConstantHelpers.Status.INACTIVE;

                        context.SaveChanges();
                        response.Data = null;
                        response.Error = false;
                        response.Message = "Success, deleted reservation";
                    }
                    else
                    {
                        response.Data = null;
                        response.Error = true;
                        response.Message = "Error, reservation or local id empty";
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
        [Route("customers/{customerid}/reservations")]
        public IHttpActionResult UpdateServiceCustomer(Int32? customerid, ReservationEntities model)
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
                        Reservation reservation = new Reservation();


                        if (model.ServiceId.HasValue && customerid.HasValue)
                        {
                            reservation = context.Reservation.FirstOrDefault(x => x.CustomerId == customerid && x.ServiceId == model.ServiceId);

                            reservation.CustomerId = customerid.Value;
                            reservation.LocalId = model.LocalId;
                            reservation.ServiceId = model.ServiceId;
                            reservation.Schedule = model.Schedule;
                            reservation.Detail = model.Detail;
                            reservation.Status = ConstantHelpers.Status.ACTIVE;

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
