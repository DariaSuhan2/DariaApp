using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainDefinition
{
    public class LuxuryCar : Car
    {

        //public LuxuryCar(int vin, string color, string brand, int doornr, CarCategory carCategory, Enums.Fuel fuel) : base(vin, color, brand, doornr, carCategory, true, true, true, true, true, true, Enums.RadioType.DIGITAL, fuel)
        public LuxuryCar(int vin, string color, string brand, int doornr, CarCategory carCategory, Enums.Fuel fuel, DateTime CreatedOn, DateTime UpdatedOn) : base(vin, color, brand, doornr, carCategory, true, true, true, true, true, true, Enums.RadioType.DIGITAL, fuel, CreatedOn, UpdatedOn)

        {
            this.Type = "Luxury";
        }
        public override string GetaAditionalProprieties()
        {
            return "CarLuxury cu aer conditionat: " + this.AirConditioning + "\ncu geamuri electrice: " + this.ElectricWindow + " \ncu senzor de parcare: " + this.ParkingSenzor;
        }

        public static LuxuryCar InitializeFromCar(Car car)
        {
            //return new LuxuryCar(car.vin, car.Color, car.Brand, car.DoorNr, car.Category, car.Fuel);
            return new LuxuryCar(car.vin, car.Color, car.Brand, car.DoorNr, car.Category, car.Fuel, car.CreatedOn, car.UpdatedOn);
        }
    }

}
