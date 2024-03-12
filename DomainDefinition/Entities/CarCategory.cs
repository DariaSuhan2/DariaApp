using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DomainDefinition
{
    public class CarCategory
    {
        public string Name { get; set; }
        public int EngineCapacity { get; set; }
        public int Weight { get; set; }

        public CarCategory(string name, int engineCapacity, int weight)
        {
            this.Name = name;
            this.EngineCapacity = engineCapacity;
            this.Weight = weight;
        }

        public CarCategory()
        {
        }
    }
}
