using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq;
using DomainDefinition.Contracts;
using DomainDefinition;
using System.Runtime.ConstrainedExecution;
using SQLDataAccess.Entities;
using AutoMapper;
using System.Data.SqlTypes;
using Azure;
using System.Reflection;

namespace SQLDataAccess
{
    public class CarRepository : SQL_DAL, ICarRepository
    {
        private IMapper mapper;
        private ICarCategoryRepository repository;
        public CarRepository()
        {
            repository = new CarCategoryRepository();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Car, SqlCar>()
                    .ForMember(d => d.CategoryName,
                        opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : String.Empty)


                    );

                cfg.CreateMap<SqlCar, Car>()
                    .ForMember(d => d.Category, opt => opt.MapFrom(src => new CarCategory(src.CategoryName, 0, 0))
                    //.ForMember(d => d.Fuel, opt => opt.MapFrom(src => src.Fuel != null ? src.Fuel : String.Empty))
                   // .ForMember(d => d.Fuel, opt => opt.ResolveUsing(src => src.Fuel))
                   );

                //cfg.CreateMap<SqlCar, Car>()
                //   .ForMember(d => d.Fuel, opt => opt.MapFrom(f => f.Fuel)
                // .ForMember(d => d.Fuel, opt => opt.MapFrom(p => p.Fuel))
                //    );
            });

            mapper = config.CreateMapper();
        }

        private List<Car> GetCars(string queryString)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "Cars");

                    var dbCars = new List<SqlCar>();
                    if (ds.Tables["Cars"] == null)
                    {
                        return new List<Car>();
                    }
                    else
                    {
                        foreach (DataRow pRow in ds.Tables["Cars"].Rows)
                        {
                            SqlCar car = new SqlCar();
                            car.VIN = Convert.ToInt32(pRow["VIN"]);
                            car.Color = pRow["Color"].ToString();
                            car.Brand = pRow["Brand"].ToString();
                            car.DoorNr = Convert.ToInt32(pRow["DoorNr"]);
                            car.CategoryName = pRow["CategoryName"].ToString();
                            car.AirConditioning = Convert.ToBoolean(pRow["AirConditioning"]);
                            car.ElectricWindow = Convert.ToBoolean(pRow["ElectricWindow"]);
                            car.ParkingSenzor = Convert.ToBoolean(pRow["ParkingSenzor"]);
                            car.USBPort = Convert.ToBoolean(pRow["USBPort"]);
                            car.ParktronicSystem = Convert.ToBoolean(pRow["ParktronicSystem"]);
                            car.InfotainmentSystem = Convert.ToBoolean(pRow["InfotainmentSystem"]);
                            car.Radio = pRow["Radio"].ToString();
                            car.Type = pRow["Type"].ToString();
                            car.Fuel = pRow["Fuel"].ToString();
                           // var a = pRow["Fuel"].ToString();
                            dbCars.Add(car);
                        }
                    }
                    return mapper.Map<List<Car>>(dbCars);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new List<Car>();
                }
            }
        }

        public void DeleteAll()
        {
            GetCars("DELETE FROM Car");
        }

        public void DeleteOne(int vin)
        {
            GetCars(@"DELETE FROM Car WHERE VIN = " + vin);
        }

        public List<Car> GetAll()
        {
            return GetCars("SELECT * FROM Car");
        }

        public List<Car> GetCarsWithAirConditioning(bool airConditioning)
        {
            return GetCars("SELECT * FROM Car WHERE (Type = 'Premium' OR Type = 'Luxury')");
        }

        public Car? GetOne(int vin)
        {
            var allResults = GetCars(@"SELECT * FROM Car WHERE VIN = " + vin);

            if (allResults.Count == 1)
            {
                var car = allResults[0];
                var category1 = car.Category.Name;
                var category = this.repository.GetOne(car.Category.Name);
                car.Category = category;
                return car;
            }
            else return null;

        }

        public int GetCategoryCount(string name)
        {
            
            var allResults = GetCars(@"SELECT * FROM Car WHERE CategoryName = '" + name + "'");
            
            return allResults.Count;
        }


        public Car? Insert(Car carInstance)
        {
            bool wasInserted = false;
            //flag
            using (var connection = new System.Data.SqlClient.SqlConnection(_connectionString))
            {
                //Car newCar = new Car(carInstance);

                var sqlCar = mapper.Map<SqlCar>(carInstance);

                var insertQuery = "INSERT INTO Car (VIN, Color, Brand, DoorNr, CategoryName, AirConditioning, ElectricWindow, ParkingSenzor, USBPort, ParktronicSystem, InfotainmentSystem, Radio, Type, Fuel) " +
                    "VALUES (@VIN, @Color, @Brand, @DoorNr, @CategoryName, @AirConditioning, @ElectricWindow, @ParkingSenzor, @USBPort, @ParktronicSystem, @InfotainmentSystem, @Radio, @Type, @Fuel)";

                SqlCommand cmd = new SqlCommand(insertQuery, connection);

                cmd.Parameters.Add("@VIN", SqlDbType.Int).Value = sqlCar.VIN;
                cmd.Parameters.Add("@Color", SqlDbType.VarChar, 50).Value = sqlCar.Color;
                cmd.Parameters.Add("@Brand", SqlDbType.VarChar, 50).Value = sqlCar.Brand;
                cmd.Parameters.Add("@DoorNr", SqlDbType.Int).Value = sqlCar.DoorNr;
                cmd.Parameters.Add("@CategoryName", SqlDbType.VarChar, 50).Value = sqlCar.CategoryName;
                cmd.Parameters.Add("@AirConditioning", SqlDbType.Bit).Value = sqlCar.AirConditioning;
                cmd.Parameters.Add("@ElectricWindow", SqlDbType.Bit).Value = sqlCar.ElectricWindow;
                cmd.Parameters.Add("@ParkingSenzor", SqlDbType.Bit).Value = sqlCar.ParkingSenzor;
                cmd.Parameters.Add("@USBPort", SqlDbType.Bit).Value = sqlCar.USBPort;
                cmd.Parameters.Add("@ParktronicSystem", SqlDbType.Bit).Value = sqlCar.ParktronicSystem;
                cmd.Parameters.Add("@InfotainmentSystem", SqlDbType.Bit).Value = sqlCar.InfotainmentSystem;
                cmd.Parameters.Add("@Radio", SqlDbType.VarChar, 10).Value = sqlCar.Radio;
                cmd.Parameters.Add("@Type", SqlDbType.VarChar, 10).Value = sqlCar.Type;
                cmd.Parameters.Add("@Fuel", SqlDbType.VarChar, 10).Value = sqlCar.Fuel;

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
            }
            //flag
            if (wasInserted)
                return GetOne(carInstance.vin);
            else return null;

        }


        public Car UpdateCar(Car carInstance)
        {
            //var allResults = GetCars(@"UPDATE Car SET Color = '" + carInstance.Color + "'" + "WHERE VIN = " + carInstance.vin);
            //var allResults = GetCars(@"UPDATE Car SET Color = '" + carInstance.Color + "', Brand = '" + carInstance.Brand + "'" + "WHERE VIN = " + carInstance.vin);
            //var allResults = GetCars(@"UPDATE Car SET Color = '" + carInstance.Color + "', Brand = '" + carInstance.Brand + "', DoorNr = '" + carInstance.DoorNr +  "', Type = '" + carInstance.Type + "'" + "WHERE VIN = " + carInstance.vin);
            //var allResults = GetCars(@"UPDATE Car SET Color = '" + carInstance.Color + "', Brand = '" + carInstance.Brand + "', DoorNr = '" + carInstance.DoorNr + "', CategoryName = '" + carInstance.Category.Name + "', Type = '" + carInstance.Type + "'" + "WHERE VIN = " + carInstance.vin);
            var e = carInstance.Radio;
            var f = carInstance.Fuel;
            //var allResults = GetCars(@"UPDATE Car SET Color = '" + carInstance.Color + "', Brand = '" + carInstance.Brand + "', DoorNr = '" + carInstance.DoorNr + "', CategoryName = '" + carInstance.Category.Name + "', Type = '" + carInstance.Type + "', AirConditioning = '" + carInstance.AirConditioning + "', ElectricWindow = '" + carInstance.ElectricWindow + "', ParkingSenzor = '" + carInstance.ParkingSenzor + "', USBPort = '" + carInstance.USBPort + "', ParktronicSystem = '" + carInstance.ParktronicSystem + "', InfotainmentSystem = '" + carInstance.InfotainmentSystem + "', Radio = '" + e + "'" + "WHERE VIN = " + carInstance.vin);
            var allResults = GetCars(@"UPDATE Car SET Color = '" + carInstance.Color + "', Brand = '" + carInstance.Brand + "', DoorNr = '" + carInstance.DoorNr + "', CategoryName = '" + carInstance.Category.Name + "', Type = '" + carInstance.Type + "', AirConditioning = '" + carInstance.AirConditioning + "', ElectricWindow = '" + carInstance.ElectricWindow + "', ParkingSenzor = '" + carInstance.ParkingSenzor + "', USBPort = '" + carInstance.USBPort + "', ParktronicSystem = '" + carInstance.ParktronicSystem + "', InfotainmentSystem = '" + carInstance.InfotainmentSystem + "', Radio = '" + e + "', Fuel = '" + f + "'" + "WHERE VIN = " + carInstance.vin);
            if (allResults.Count == 1)
                return allResults[0];
            else return null;
        }

        /*public Car UpdateColor(int vin, string color)
        {
            var allResults = GetCars(@"UPDATE Car SET Color = '" + color + "'" + "WHERE VIN = " + vin);
            if (allResults.Count == 1)
                return allResults[0];
            else return null;
        }*/


        
    }
}
