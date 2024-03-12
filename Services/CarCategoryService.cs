using DomainDefinition;
using DomainDefinition.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
//using TextFileDataAccess;
using SQLDataAccess;
using static Azure.Core.HttpHeader;

namespace Services
{
    public class CarCategoryService
    {
        private ICarCategoryRepository repository;
        public ICarRepository carRepository;

        public CarCategoryService()
        {
            repository = new CarCategoryRepository();
            carRepository = new CarRepository();
        }

        public List<CarCategory> GetAllCategories()
        {
            return this.repository.GetAll();
        }

        public CarCategory? GetCategory(string name)
        {
            return this.repository.GetOne(name);
        }

        public CarCategory AddCategory(string name, int engineCapacity, int weight)
        {
            return this.repository.Insert(name, engineCapacity, weight);
        }

        public List<string> DeleteAllCategories()
        {
            var result = new List<string>();
            foreach (var category in this.repository.GetAll())
            {
                result.Add(DeleteCategory(category.Name));
            }
            return result;
        }

        public string DeleteCategory(string name)
        {
            var a = this.carRepository.GetCategoryCount(name);
            if (a == 0)
            {
                this.repository.DeleteOne(name);
                return $"Categoria {name} a fost stearsa";
            }
            else
            {
                return $"Categoria {name} nu poate fi stearsa pentru ca este folosita ca si categorie pentru o masina";
            }
        }
    }
}
