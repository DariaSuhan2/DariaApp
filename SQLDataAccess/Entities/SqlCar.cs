using DomainDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDataAccess.Entities
{
    internal class SqlCar
    {
        public int VIN { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public int DoorNr { get; set; }
        public string CategoryName { get; set; }
        public bool AirConditioning { get; set; }
        public bool ElectricWindow { get; set; }
        public bool ParkingSenzor { get; set; }
        public bool USBPort { get; set; }
        public bool ParktronicSystem { get; set; }
        public bool InfotainmentSystem { get; set; }
        public string Radio { get; set; }
        public string Type { get; set; }
        public string Fuel { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}
