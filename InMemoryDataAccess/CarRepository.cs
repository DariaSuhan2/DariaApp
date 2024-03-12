using DomainDefinition;
using DomainDefinition.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InMemoryDataAccess
{
    public class CarRepository : ICarRepository 
    {

        private  List<Car> allCars = new List<Car>();


        public CarRepository()
        {

        }

        public List<Car> GetAll()
        {
            return this.allCars;
        }

        public Car? GetOne(int vin)
        {
            var found = allCars.Where(x => x.vin == vin).FirstOrDefault();
            return found;
        }

        public List<Car> GetCarsWithAirConditioning(bool airConditioning)
        {
            return allCars.Where(x => x.AirConditioning == true).ToList<Car>();
        }     
        
        public void DeleteAll()
        {
            allCars = new List<Car>();
        }
        public void DeleteOne(int vin)
        {
            var found = GetOne(vin);
            if (found != null)
            {
               allCars.Remove(found);
            }
        }

        public Car Insert(Car carInstance)
        {
            var found = GetOne(carInstance.vin);
            if (found == null)
            {
                allCars.Add(carInstance);
                return carInstance;
            }
            else return found;
        }

        public Car UpdateCar(Car carInstance)
        {
            var foundCar = GetOne(carInstance.vin);
            if (foundCar != null)
            {
                foundCar.Color = carInstance.Color;
            }
            return foundCar;
        }

        /*public Car UpdateColor(int vin, string color)
        {
            var foundCar = GetOne(vin);
            if (foundCar != null)
            {
                foundCar.Color = color;
            }
            return foundCar;
        }*/

        public int GetCategoryCount(string name)
        {

          return allCars.Count;
        }
    }
}
