using DomainDefinition;
using DomainDefinition.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InMemoryDataAccess
{
    public class CarCategoryRepository : ICarCategoryRepository
    {
        private List<CarCategory> allCategories = new List<CarCategory>();

        public CarCategoryRepository()
        {

        }

        public List<CarCategory> GetAll()
        {
            return this.allCategories;
        }

        public CarCategory? GetOne(string name)
        {
            var found = allCategories.Where(x => x.Name == name).FirstOrDefault();
            return found;
        }
      
        public void DeleteAll()
        {
            allCategories = new List<CarCategory>();
        }

        public void DeleteOne(string name)
        {
            var found = GetOne(name);
            if (found != null)
            {
                allCategories.Remove(found);
            }
        }

        public CarCategory Insert(string name, int engineCapacity, int weight)
        {
            var found = GetOne(name);
            if (found == null)
            {
                var newCategory = new CarCategory(name, engineCapacity, weight);
                allCategories.Add(newCategory);
                return newCategory;
            }
            else return found;
        }
    }
}
