using DomainDefinition;
using DomainDefinition.Contracts;
using System.Text.Json;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace TextFileDataAccess
{
    public class CarCategoryRepository : ICarCategoryRepository
    {
        private string filePath;

        public CarCategoryRepository()
        {
            this.filePath = Path.Combine(@"C:\Temp", "categories.json");
        }

        private void WriteJsonFile(List<CarCategory> categories)
        {
            string jsonString = JsonSerializer.Serialize(categories, new JsonSerializerOptions());
            using (StreamWriter outputFile = new StreamWriter(this.filePath))
            {
                outputFile.WriteLine(jsonString);
            }
        }

        private List<CarCategory> ReadJsonFile()
        {
            List<CarCategory> result = new List<CarCategory>();
            using (StreamReader r = new StreamReader(this.filePath))
            {
                string json = r.ReadToEnd();
                if (json == null || json.Length == 0)
                {
                    return new List<CarCategory>();
                }
                else
                {
                    result = JsonSerializer.Deserialize<List<CarCategory>>(json);
                }
            }
            return result;
        }

        public List<CarCategory> GetAll()
        {
            return this.ReadJsonFile();
        }

        public CarCategory? GetOne(string name)
        {
            var all = this.ReadJsonFile();
            var found = all.Where(x => x.Name == name).FirstOrDefault();
            return found;
        }

        public void DeleteAll()
        {
            this.WriteJsonFile(new List<CarCategory>());
        }

        public void DeleteOne(string name)
        {
            var all = this.ReadJsonFile();
            var found = all.Where(x => x.Name == name).FirstOrDefault();
            if (found != null)
            {
                all.Remove(found);
            }
            this.WriteJsonFile(all);
        }

        public CarCategory Insert(string name, int engineCapacity, int weight)
        {
            var all = this.ReadJsonFile();
            var found = all.Where(x => x.Name == name).FirstOrDefault();
            if (found == null)
            {
                var newCategory = new CarCategory(name, engineCapacity, weight);
                all.Add(newCategory);
                this.WriteJsonFile(all);
                return newCategory;
            }
            else return found;
        }
    }
}
