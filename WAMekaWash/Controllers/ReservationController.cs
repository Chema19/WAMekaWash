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
                        response.Data = context.Reservation.Where(x => x.LocalId == localid && x.Status == ConstantHelpers.Status.ACTIVE || x.Status == ConstantHelpers.Status.ACCEPTED || x.Status == ConstantHelpers.Status.CANCEL || x.Status == ConstantHelpers.Status.FINISH).Select(x => new
                        {
                            ReservationId = x.ReservationId,
                            CustomerId = x.CustomerId,
                            LocalId = x.LocalId,
                            ServiceId = x.ServiceId,
                            Schedule = x.Schedule,
                            Detail = x.Detail,
                            Status = x.Status,
                            Car = x.CarId,
                            Fecha = x.Fecha,
                            Cotizacion = x.Cotization,
                            MessageProvider = x.MessageProvider
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
                        response.Data = context.Reservation.Where(x => x.ReservationId == reservationid && x.LocalId == localid && x.Status == ConstantHelpers.Status.ACTIVE).Select(x => new
                        {
                            ReservationId = x.ReservationId,
                            CustomerId = x.CustomerId,
                            LocalId = x.LocalId,
                            ServiceId = x.ServiceId,
                            Schedule = x.Schedule,
                            Detail = x.Detail,
                            Status = x.Status,
                            Car = x.CarId,
                            Fecha = x.Fecha,
                            Cotizacion = x.Cotization,
                            MessageProvider = x.MessageProvider
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
        [HttpPost]
        [Route("locals/{localid}/reservations")]
        public IHttpActionResult AcceptCancelReservationProvider(Int32? localid, ReservationEntities model)
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

                        if (!localid.HasValue)
                        {
                            response.Data = null;
                            response.Error = true;
                            response.Message = "Error, local empty";
                            return Content(HttpStatusCode.BadRequest, response);
                        }
                        else
                        {

                            reservation = context.Reservation.FirstOrDefault(x => x.LocalId == localid && x.ReservationId == model.ReservationId);

                            if (model.Status == ConstantHelpers.Status.ACCEPTED)
                            {
                                reservation.Status = ConstantHelpers.Status.ACCEPTED;
                            }
                            else if(model.Status == ConstantHelpers.Status.CANCEL){
                                reservation.Status = ConstantHelpers.Status.CANCEL;
                            }else{
                                reservation.Status = ConstantHelpers.Status.FINISH;
                            }
                            reservation.MessageProvider = model.MessageProvider;

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
                        response.Data = context.Reservation.Where(x => x.CustomerId == customerid && x.Status == ConstantHelpers.Status.ACTIVE).Select(x => new
                        {
                            ReservationId = x.ReservationId,
                            CustomerId = x.CustomerId,
                            LocalId = x.LocalId,
                            ServiceId = x.ServiceId,
                            Schedule = x.Schedule,
                            Detail = x.Detail,
                            Status = x.Status,
                            Car = x.CarId,
                            Fecha = x.Fecha,
                            Cotizacion = x.Cotization,
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
                        response.Data = context.Reservation.Where(x => x.ReservationId == reservationid && x.CustomerId == customerid && x.Status == ConstantHelpers.Status.ACTIVE).Select(x => new
                        {
                            ReservationId = x.ReservationId,
                            CustomerId = x.CustomerId,
                            LocalId = x.LocalId,
                            ServiceId = x.ServiceId,
                            Schedule = x.Schedule,
                            Detail = x.Detail,
                            Status = x.Status,
                            Car = x.CarId,
                            Fecha = x.Fecha,
                            Cotizacion = x.Cotization,
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
        public IHttpActionResult RegisterReservationCustomer(Int32? customerid, ReservationEntities model)
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
                            reservation.CarId = model.CarId;
                            reservation.Fecha = model.Fecha;
                            reservation.Cotization = model.Cotización;

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
        public IHttpActionResult DeleteReservationCustomer(Int32? customerid, Int32? reservationid)
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
        public IHttpActionResult UpdateReservationCustomer(Int32? customerid, ReservationEntities model)
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


                        if (model.ReservationId.HasValue && customerid.HasValue)
                        {
                            reservation = context.Reservation.FirstOrDefault(x => x.CustomerId == customerid && x.ReservationId == model.ReservationId);

                            reservation.CustomerId = customerid.Value;
                            reservation.LocalId = model.LocalId;
                            reservation.ServiceId = model.ServiceId;
                            reservation.Schedule = model.Schedule;
                            reservation.Detail = model.Detail;
                            reservation.Status = ConstantHelpers.Status.ACTIVE;
                            reservation.CarId = model.CarId;
                            reservation.Fecha = model.Fecha;
                            reservation.Cotization = model.Cotización;

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
