using System.Drawing;
using System.Text;

namespace DomainDefinition
{
    public class Car
    {

        public int vin { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public int DoorNr { get; set; }
        public CarCategory? Category { get; set; }

        public bool AirConditioning { get; set; }
        public bool ElectricWindow { get; set; }
        public bool ParkingSenzor { get; set; }

        public bool USBPort { get; set; }
        public bool ParktronicSystem { get; set; }
        public bool InfotainmentSystem { get; set; }
        public Enums.RadioType Radio { get; set; }
        public string Type { get; set; }
        public Enums.Fuel Fuel { get; set; }

         public DateTime CreatedOn  { get; set; }
         public DateTime UpdatedOn  { get; set; }
         

          
    


        public Car()
        {

        }

        public Car(int vin, string color, string brand, int doorNr, CarCategory carCategory, bool airConditioning, bool electricWindow, bool parkingSenzor, bool usbPort, bool parktronicSystem, bool infotainmentSystem, Enums.RadioType radio, Enums.Fuel fuel)
        //public Car(int vin, string color, string brand, int doorNr, CarCategory carCategory, bool airConditioning, bool electricWindow, bool parkingSenzor, bool usbPort, bool parktronicSystem, bool infotainmentSystem, Enums.RadioType radio, Enums.Fuel fuel, DateTime CreatedOn, DateTime UpdatedOn)
        {

            this.vin = vin;
            this.Color = color;
            this.Brand = brand;
            this.DoorNr = doorNr;
            this.Category = carCategory;

            this.AirConditioning = airConditioning;
            this.ElectricWindow = electricWindow;
            this.ParkingSenzor = parkingSenzor;
            this.USBPort = usbPort;

            this.ParktronicSystem = parktronicSystem;
            this.InfotainmentSystem = infotainmentSystem;
            this.Radio = radio;
            this.Fuel = fuel;
           // this.CreatedOn = CreatedOn;
           // this.UpdatedOn = UpdatedOn;
        }

        public virtual string GetaAditionalProprieties()
        {
            return string.Empty;

        }
    }
}


