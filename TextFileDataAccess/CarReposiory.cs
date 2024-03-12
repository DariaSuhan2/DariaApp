using DomainDefinition.Contracts;
using DomainDefinition;
using System.Text.Json;
using System.Xml.Linq;
using System.Text;
using System.Collections;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace TextFileDataAccess
{
    public class CarRepository : ICarRepository
    {
        private string filePath;

        public CarRepository()
        {
            this.filePath = Path.Combine(@"C:Temp", "cars.json");
            File.Delete(this.filePath);
        }
               
        private void WriteJsonFile(List<Car> cars)
        {
            using (StreamWriter sw = new StreamWriter(this.filePath, false))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                sw.WriteLine("[");
                var serializer = new Newtonsoft.Json.JsonSerializer();
                var carsCount = cars.Count();
                foreach (var item in cars)
                {
                    var indexOf = cars.IndexOf(item);
                    serializer.Serialize(writer, item);

                    if (carsCount > 1)
                    {
                        if (indexOf < carsCount - 1)
                        {
                            sw.Write(Environment.NewLine + ",");
                        }
                        else
                        {
                            writer.WriteWhitespace(Environment.NewLine);
                        }
                    }
                    else
                    {
                        writer.WriteWhitespace(Environment.NewLine);
                    }
                }
                sw.WriteLine("]");
            }
        }

        private List<Car> ReadJsonFile()
        {
            try
            {
                if (File.Exists(this.filePath))
                {

                    List<Car> result = new List<Car>();
                    using (StreamReader r = new StreamReader(this.filePath))

                    {
                        string json = r.ReadToEnd();
                        if (json == null || json.Length == 0)
                        {
                            return new List<Car>();
                        }
                        else
                        {
                            result = JsonSerializer.Deserialize<List<Car>>(json, new JsonSerializerOptions() { IncludeFields = true });
                        }
                    }

                    List<Car> list = new List<Car>();
                    if (result.Count > 0) { 
                        result.ForEach(car => {
                            if (car.Type == "Budget")
                                list.Add((BudgetCar.InitializeFromCar(car)));
                            else if (car.Type == "Luxury")
                                list.Add((LuxuryCar.InitializeFromCar(car)));
                            else if (car.Type == "Premium")
                                list.Add((PremiumCar.InitializeFromCar(car)));
                        });
                    }
                    return list;
                }
                else return new List<Car>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                return new List<Car>();
            }
        }

        public List<Car> GetAll()
        {
            return this.ReadJsonFile();
        }

        public Car? GetOne(int vin)
        {
            var all = this.ReadJsonFile();
            var found = all.Where(x => x.vin == vin).FirstOrDefault();
            return found;
        }

        public Car Insert(Car carInstance)
        {
            var all = this.ReadJsonFile();
            var found = GetOne(carInstance.vin);
            if (found == null)
            {
                all.Add(carInstance);
                this.WriteJsonFile(all);
                return carInstance;
            }
            else return found;
        }

        public void DeleteOne(int vin)
        {
            var all = this.ReadJsonFile();
            var found = GetOne(vin);
            if (found != null)
            {
                all.Remove(found);
            }
            this.WriteJsonFile(all);
        }

        public void DeleteAll()
        {
            File.Delete(this.filePath);
        }

        public List<Car> GetCarsWithAirConditioning(bool airConditioning)
        {
            var all = this.ReadJsonFile();
           
            var found = all.Where(x => x.AirConditioning == true).ToList<Car>();
            return found;
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

       /* public Car UpdateColor(int vin, string color)
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
            var all = this.ReadJsonFile();
            return all.Count;
        }
    }
}
