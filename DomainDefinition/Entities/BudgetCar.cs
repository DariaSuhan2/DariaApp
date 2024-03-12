using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DomainDefinition
{
    public class BudgetCar : Car
    {
        public BudgetCar(int vin, string color, string brand, int doorNr, CarCategory cat, Enums.Fuel fuel): base(vin, color, brand, doorNr, cat, false, false, false, false, false, false, Enums.RadioType.ANALOG, fuel)
        //semnatura meth, constructor, fct, param, prop, meth, apel
        //def constructor a unei cl
        //derived-constructor(parameter-list) : base(argument - list)

        //semnatura : base apel
        {
            this.Type = "Budget";
        }

        public override string GetaAditionalProprieties()
        {
            return "Buget Car cu aer conditionat: " + this.AirConditioning + "\ncu geamuri electrice: " + this.ElectricWindow + " \ncu senzor de parcare: " + this.ParkingSenzor;
              
        }

        public static BudgetCar InitializeFromCar(Car car)
        {
            return new BudgetCar(car.vin, car.Color, car.Brand, car.DoorNr, car.Category, car.Fuel);
        }
    }
}
