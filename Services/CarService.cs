using DomainDefinition;
using DomainDefinition.Contracts;
//using TextFileDataAccess;
using SQLDataAccess;

namespace Services
{
    public class CarService
    {
        // public static List<Car> _allCars = new List<Car>();

        public ICarRepository carRepository;

        public CarService()
        {
            carRepository = new CarRepository();
        }

        public List<Car> GetAllCars()
        {
            return this.carRepository.GetAll();
        }


        public Car? GetCar(int vin)
        {
            return this.carRepository.GetOne(vin);
        }

        public Car AddCar(Car carInstance)
        {
            return this.carRepository.Insert(carInstance);
        }

        public void AddBudgetCar(int vin, string color, string brand, int doorNr, CarCategory carCategory, Enums.Fuel fuel)
        {
            var carInstance = new BudgetCar(vin, color, brand, doorNr, carCategory, fuel);
            AddCar(carInstance);
        }

        public void AddPremiumCar(int vin, string color, string brand, int doorNr, CarCategory carCategory, Enums.Fuel fuel)
        {
            var carInstance = new PremiumCar(vin, color, brand, doorNr, carCategory, fuel);
            AddCar(carInstance);
        }

        public void AddLuxuryCar(int vin, string color, string brand, int doorNr, CarCategory carCategory, Enums.Fuel fuel)
        {
            var carInstance = new LuxuryCar(vin, color, brand, doorNr, carCategory, fuel);
            AddCar(carInstance);
        }

        /*public void ChangeColor(int vin, string color)
        {
            this.carRepository.UpdateColor(carInstance);
        } */
        public void ChangeCar(Car carInstance)
        {
            this.carRepository.UpdateCar(carInstance);
        }

        public List<Car> GetCarsWithAirConditioning(bool airConditioning)
        {
            return this.carRepository.GetCarsWithAirConditioning(airConditioning);
        }
        public void DeleteAllCars()
        {
            this.carRepository.DeleteAll();
        }

        public void DeleteCar(int vin)
        {

            this.carRepository.DeleteOne(vin);
        }



    }
}
