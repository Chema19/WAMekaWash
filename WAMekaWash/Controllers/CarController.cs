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
    [RoutePrefix("wamekawash/v7")]
    public class CarController : BaseApiController
    {
        [HttpGet]
        [Route("customers/{customerid}/cars")]
        public IHttpActionResult ListCars(Int32? customerid)
        {

            try
            {
                using (var ts = new TransactionScope())
                {
                    if (customerid.HasValue)
                    {
                        response.Data = context.Car.Where(x => x.CustomerId == customerid).Select(x => new
                        {
                            CarId = x.CarId,
                            Description = x.Description,
                            Brand = x.Brand.Name,
                            CustomerId = x.CustomerId,
                            Placa = x.Placa,
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
        [Route("customers/{customerid}/cars/{carid}")]
        public IHttpActionResult ReadCar(Int32? customerid, Int32? carid)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    if (customerid.HasValue && carid.HasValue)
                    {
                        response.Data = 
                        context.Car.Where(x => x.CustomerId == customerid && x.CarId == carid).Select(x => new
                        {
                            CarId = x.CarId,
                            Description = x.Description,
                            Brand = x.Brand.Name,
                            CustomerId = x.CustomerId,
                            Placa = x.Placa,
                        }).ToList();
                        response.Error = false;
                        response.Message = "Success";
                    }
                    else
                    {
                        response.Data = null;
                        response.Error = true;
                        response.Message = "Error, customer or car id empty";
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
        [Route("customers/{customerid}/cars")]
        public IHttpActionResult RegisterCar(Int32? customerid, CarEntities model)
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
                        Car car = new Car();

                        if (!customerid.HasValue)
                        {
                            response.Data = null;
                            response.Error = true;
                            response.Message = "Error, customer empty";
                            return Content(HttpStatusCode.BadRequest, response);
                        }
                        else
                        {

                            context.Car.Add(car);

                            car.BrandId = model.BrandId;
                            car.Description = model.Description;
                            car.Placa = model.Placa;
                            car.CustomerId = model.CustomerId;


                            context.SaveChanges();

                            response.Data = null;
                            response.Error = false;
                            response.Message = "Success, saved car";
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
        //[Route("customers/{customerid}/cars/{carid}")]
        //public IHttpActionResult DeleteCar(Int32? customerid, Int32? carid)
        //{
        //    try
        //    {
        //        using (var ts = new TransactionScope())
        //        {
        //            if (serviceid.HasValue && localid.HasValue)
        //            {
        //                Service service = new Service();
        //                service = context.Service.FirstOrDefault(x => x.ServiceId == serviceid && x.LocalId == localid);

        //                service.Status = ConstantHelpers.Status.INACTIVE;

        //                context.SaveChanges();
        //                response.Data = null;
        //                response.Error = false;
        //                response.Message = "Success, deleted service";
        //            }
        //            else
        //            {
        //                response.Data = null;
        //                response.Error = true;
        //                response.Message = "Error, service or local id empty";
        //            }
        //            ts.Complete();
        //        }
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Unauthorized();
        //    }
        //}
        [HttpPut]
        [Route("customers/{customerid}/cars")]
        public IHttpActionResult UpdateCar(Int32? customerid, CarEntities model)
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
                        Car car = new Car();


                        if (model.CarId.HasValue && customerid.HasValue)
                        {
                            car = context.Car.FirstOrDefault(x => x.CustomerId == customerid && x.CarId == model.CarId);

                            car.BrandId = model.BrandId;
                            car.Description = model.Description;
                            car.Placa = model.Placa;
                            car.CustomerId = model.CustomerId;
                            context.SaveChanges();

                            response.Data = null;
                            response.Error = false;
                            response.Message = "Success, updated car";
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
