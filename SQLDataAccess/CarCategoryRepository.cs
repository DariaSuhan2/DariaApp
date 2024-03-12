using DomainDefinition;
using DomainDefinition.Contracts;
using System.Data;
using System.Data.SqlClient;
using System.Net.Quic;
using System.Data.Linq;
//using System.Data.SqlClient;
using QC = Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using SQLDataAccess.Entities;
using AutoMapper;

namespace SQLDataAccess
{
    public class CarCategoryRepository : SQL_DAL, ICarCategoryRepository
    {
        
        private IMapper mapper;


        public CarCategoryRepository()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CarCategory, SqlCarCategory>();

                cfg.CreateMap<SqlCarCategory, CarCategory>();
                    
            });
            mapper = config.CreateMapper();
        }

        private List<CarCategory> GetCategories(string queryString)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet ds = new DataSet();


                    adapter.Fill(ds, "Categories");

                    var dbCarCategory = new List<SqlCarCategory>();
                    if (ds.Tables["Categories"] == null)
                    {
                        return new List<CarCategory>();
                    }
                    else
                    {
                        foreach (DataRow pRow in ds.Tables["Categories"].Rows)
                        {
                            SqlCarCategory carCategory = new SqlCarCategory();
                            carCategory.Name = pRow["Name"].ToString();
                            carCategory.EngineCapacity = Convert.ToInt32(pRow["EngineCapacity"]);
                            carCategory.Weight = Convert.ToInt32(pRow["Weight"]);

                            dbCarCategory.Add(carCategory);
                        }
                    }

                    return mapper.Map<List<CarCategory>>(dbCarCategory);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new List<CarCategory>();
                }
            }
        }

        public void DeleteAll()
        {
            GetCategories("DELETE FROM CarCategory");
        }

        public void DeleteOne(string name)
        { 
           GetCategories("DELETE FROM CarCategory WHERE Name = '" + name + "'");

        }
        
        public List<CarCategory> GetAll()
        {
            return GetCategories("SELECT * FROM CarCategory");

        }

        public CarCategory? GetOne(string name)
        {
            var allResults = GetCategories(@"SELECT * FROM CarCategory WHERE Name = '" + name + "'");
            if (allResults.Count == 1)
                return allResults[0];
            else return null;
        }

        public CarCategory? Insert(string name, int engineCapacity, int weight)
        {
            bool wasInserted = false;
            
            using (var connection = new System.Data.SqlClient.SqlConnection(_connectionString))
            {
                CarCategory newCat = new CarCategory(name, engineCapacity, weight);
                var sqlCarCategory = mapper.Map<SqlCarCategory>(newCat);

                //var insertQuery = "INSERT INTO CarCategory (Name, EngineCapacity, Weight)" +
                //   $" VALUES({sqlCarCategory.Name}, {sqlCarCategory.EngineCapacity}, {sqlCarCategory.Weight});";

                var insertQuery = "INSERT INTO CarCategory (Name, EngineCapacity, Weight) VALUES (@Name, @EngineCapacity, @Weight)";

                SqlCommand cmd = new SqlCommand(insertQuery, connection);

                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = sqlCarCategory.Name;
                cmd.Parameters.Add("@EngineCapacity", SqlDbType.Int).Value = sqlCarCategory.EngineCapacity;
                cmd.Parameters.Add("@Weight", SqlDbType.Int).Value = sqlCarCategory.Weight;

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    wasInserted = true;
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
                finally 
                {
                    connection.Close();
                }
                if (wasInserted)
                    return GetOne(name);
                else return null;
            }
        }
    } 
           
}