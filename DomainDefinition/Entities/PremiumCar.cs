using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainDefinition
{
    public class PremiumCar : Car
    {
        public PremiumCar(int vin, string color, string brand, int doornr, CarCategory carCategory, Enums.Fuel fuel) : base(vin, color, brand, doornr, carCategory, true, true, true, true, false, false, Enums.RadioType.DIGITAL, fuel)
        {
            this.Type = "Premium";
        }
        public override string GetaAditionalProprieties()
        {
            return "PremiumCar cu aer conditionat: " + this.AirConditioning + "\ncu geamuri electrice: " + this.ElectricWindow + "\ncu senzor de parcare: " + this.ParkingSenzor;
        }

        public static PremiumCar InitializeFromCar(Car car)
        {
            return new PremiumCar(car.vin, car.Color, car.Brand, car.DoorNr, car.Category, car.Fuel);
        }
    }
}
