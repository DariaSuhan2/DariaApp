using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using WebApi.Models;
using System.Linq;
using AutoMapper;
using DomainDefinition;
using Services;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("api/category")]
    public class AppControllerCategory: ControllerBase
    {
        private IMapper mapper;
        private CarCategoryService service = new CarCategoryService();

        public AppControllerCategory()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CarCategory, CategoryDto>();


                cfg.CreateMap<CategoryDto, CarCategory>();
            });

            mapper = config.CreateMapper();
            
        }

        [HttpGet]
        public ActionResult<List<CategoryDto>> GetAllCategories()
        {
            var domainCategories = service.GetAllCategories();

            return mapper.Map<List<CategoryDto>>(domainCategories);
        }

        [HttpGet("{name}")]
        public ActionResult<CategoryDto> GetCategory(string name)
        {
            var domainCategory = service.GetCategory(name);
                                 
            if (domainCategory == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(mapper.Map<CategoryDto>(domainCategory));
            }

        }

         [HttpPost]
         public ActionResult<CategoryDto> PostCategory(string name, int engineCapacity, int weight)
         {

            var domainCategory = service.GetCategory(name);
            if (domainCategory == null)
            {
                 var newCategory = service.AddCategory(name, engineCapacity, weight);
                 return Ok(mapper.Map<CategoryDto>(newCategory));
            }
            else return Ok(); 

         }

         [HttpDelete("all")]
         public ActionResult<string> DeleteAllCategories()
         {
            var message = service.DeleteAllCategories();
            return Ok(message);
         }

         [HttpDelete("one/{name}")]
         public ActionResult<string> DeleteCategory(string name)
         {
            var message = service.DeleteCategory(name);
            return Ok(message);
         }
    }
}
