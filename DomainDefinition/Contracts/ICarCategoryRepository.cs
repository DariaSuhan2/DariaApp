using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainDefinition.Contracts
{
    public interface ICarCategoryRepository
    {
        List<CarCategory> GetAll();
        CarCategory? GetOne(string name);
        CarCategory Insert(string name, int engineCapacity, int weight);

       // CarCategory Update(string name, int engineCapacity, int weight);
        void DeleteOne(string name);
        void DeleteAll();
    }
}
