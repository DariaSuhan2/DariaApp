using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using WebApi.Models;
using System.Linq;
using System.Xml.Linq;
using Services;
using AutoMapper;
using DomainDefinition;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("api/car")]
    public class AppControllerCar : ControllerBase
    {

        private IMapper mapper;

        public CarService serviceCar = new CarService();
        public AppControllerCar()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Car, CarDto>();


                cfg.CreateMap<CarDto, Car>();
            });

            mapper = config.CreateMapper();

        }
        [EnableCors("_myAllowSpecificOrigins")]

        [HttpGet]
        public ActionResult<List<CarDto>> GetAllCars()
        {
            //var service = new CarService();
            var domainCars = serviceCar.GetAllCars();
            //return mapper.Map<List<CarDto>>(domainCars);
            return Ok(mapper.Map<List<CarDto>>(domainCars));

        }


        [HttpGet("{vin}")]
        public ActionResult<CarDto> GetCar(int vin)
        {
            //var service = new CarService();
            var domainCar = serviceCar.GetCar(vin);

            if (domainCar == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(mapper.Map<CarDto>(domainCar));
                //return mapper.Map<CarDto>(domainCar);
            }

        }

        [HttpDelete("delete-all")]
        public void DeleteAllCars()
        {
            serviceCar.DeleteAllCars();
            //Ok();
        }

        [HttpDelete("{vin}")]
        public void DeletCar(int vin)
        {
            serviceCar.DeleteCar(vin);
            // Ok();

        }

        [HttpPost]
        public ActionResult<CarDto> PostCar(CarDto carDto)
        {
            var domainCar = mapper.Map<Car>(carDto);
            domainCar = serviceCar.AddCar(domainCar);
            return Ok(mapper.Map<CarDto>(domainCar));
            
        }

        [HttpPut("{vin}")]
        public ActionResult<CarDto> PutCar(CarDto carDto)
        {
            //var domainCar = serviceCar.GetCar(carDto.vin);
            var domainCar = mapper.Map<Car>(carDto);
            if (domainCar != null)
            {
                serviceCar.ChangeCar(domainCar);
                return Ok(mapper.Map<CarDto>(domainCar));
            }
            else
            {
                return NotFound();
            }
        }



        /*[HttpPatch("{vin}")]
        public ActionResult<CarDto> PatchCarColor(int vin, string color)
        {
            var domainCar = serviceCar.GetCar(vin);
            if (domainCar != null)
            {
                serviceCar.ChangeColor(vin, color);
                return Ok(mapper.Map<CarDto>(domainCar));
            }
            else
            {
                return NotFound();
            }
        }*/

    }
}
