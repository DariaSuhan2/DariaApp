using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainDefinition.Contracts
{
    public interface ICarRepository
    {
        List<Car> GetAll();
        Car? GetOne(int vin);
        int GetCategoryCount(string name);
        Car? Insert(Car carInstance);
                
        void DeleteOne(int vin);
        void DeleteAll();

        List<Car> GetCarsWithAirConditioning(bool airConditioning);
        //Car UpdateColor(int vin, string color);
        Car UpdateCar(Car carInstance);
    }

}

