// See https://aka.ms/new-console-template for more information
using DomainDefinition;
using Services;
using System;
using System.ComponentModel.Design;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using System.Data;
using System.Collections.Generic;
using System.Net.WebSockets;

public class Program
{
    //ShowMainMenu();


    static void Main(string[] args)
    {
       
        var carCategoryService = new CarCategoryService();

        var all = carCategoryService.GetAllCategories();
        var carService = new CarService();

        /* string name = "Smallcar";
         int engineCapacity = 2000;
         int weight = 4;

         carCategoryService.DeleteCategory(name);
         carCategoryService.DeleteAllCategories();
         carCategoryService.AddCategory(name, engineCapacity, weight);
         carCategoryService.AddCategory("Bus", 4000, 6);
         var cart = carCategoryService.GetCategory(name);




         carService.AddBudgetCar(1, "Red", "Ford", 2, cart);
         carService.AddBudgetCar(2, "Black", "Opel", 4, cart );
         carService.AddBudgetCar(3, "White", "Ford", 4, cart);
         carService.AddPremiumCar(4, "Red", "Nissan", 4, cart);
         carService.AddLuxuryCar(5, "Green", "Hyundai", 4, cart);


         var allcars = carService.GetAllCars();


         string color = "Red";
         int vin = 1;
         bool airConditioning = true;*/

        //carService.GetCar(vin);
        //carService.ChangeColor(vin, color);
        //carService.GetCarsWithAirConditioning(airConditioning);
        //carService.DeleteCar(vin);
        //carService.DeleteAllCars();



        AfiseazaMeniu();

        string option = Console.ReadLine();
        do
        {
            if (option == "0")
            {
                ReseteazaDate(carCategoryService, carService);
            }
            else if (option == "1")
            {
                AdaugaCategorie(carCategoryService);
            }
            else if (option == "2")
            {
                AdaugaMasina(carService, carCategoryService);
            }
            else if (option == "3")
            {
                ListareCategorii(carCategoryService);
            }
            else if (option == "4")
            {
                ListareMasini(carService);
            }
            else if (option == "5")
            {
                StergeCategoria(carCategoryService);
            }
            else if (option == "6")
            {
                StergeMasina(carService);
            }
            else if (option == "7")
            {
                AfisareCategorie(carCategoryService);
            }
            else if (option == "8")
            {
                AfisareMasina(carService);
            }
            else if (option == "9")
            {
                ListareMasinaAerConditionat(carService);
            }
            else if (option == "10")
            {
                SchimbareCuloareMasina(carService);
            }
            else
            {
                Console.WriteLine("Ai introdus o comanda gresita!");
                //break;
            }

            AfiseazaMeniu();
            option = Console.ReadLine();

        }
        while (option.ToLower() != "x");

        Console.ReadLine();
    }

    public static void AfiseazaMeniu()
    {
        Console.WriteLine("Meniu");
        Console.WriteLine("-----");
        Console.WriteLine("Apasa 0 pentru resetare date...");
        Console.WriteLine("Apasa 1 pentru adaugare categorie de masini...");
        Console.WriteLine("Apasa 2 pentru adaugare masina...");
        Console.WriteLine("Apasa 3 pentru listare categorii de masini...");
        Console.WriteLine("Apasa 4 pentru listare masini...");
        Console.WriteLine("Apasa 5 pentru stergere categorie de masini...");
        Console.WriteLine("Apasa 6 pentru stergere masina...");
        Console.WriteLine("Apasa 7 pentru afisare categorie de masini...");
        Console.WriteLine("Apasa 8 pentru afisare masina...");
        Console.WriteLine("Apasa 9 pentru listare masini cu aer conditionat...");
        Console.WriteLine("Apasa 10 pentru schimbare culoare de la o masina...");

        Console.WriteLine("-----");
        Console.WriteLine("Apasa X pentru iesire.");
    }

    public static void ReseteazaDate(CarCategoryService carCategoryService, CarService carService)
    {
        carCategoryService.DeleteAllCategories();
        carService.DeleteAllCars();
        Console.WriteLine("Toate categoriile de masini si masinile au fost sterse!");
    }

    public static void AdaugaCategorie(CarCategoryService carCategoryService)
    {
        Console.WriteLine("Introdu nume categorie...");
        var nume = Console.ReadLine();
       
        
        if (String.IsNullOrWhiteSpace(nume))
        {
            Console.WriteLine("Nume categorie invalid!");
            return;
        }

        Console.WriteLine("Introdu capacitate motor...");
        var cc = Console.ReadLine();
        
        if (String.IsNullOrWhiteSpace(cc))
        {
            Console.WriteLine("Capacitate motor invalida!");
            return;
        }

        Console.WriteLine("Introdu greutate...");
        var weight = Console.ReadLine();
      
        if (String.IsNullOrWhiteSpace(weight))
        {
            Console.WriteLine("Greutate invalida!");
            return;
        }

        carCategoryService.AddCategory(nume, int.Parse(cc), int.Parse(weight));
        Console.WriteLine("Categoria a fost adaugata!");
    }

    public static void AdaugaMasina(CarService carService, CarCategoryService carCategoryService)
    {
        Console.WriteLine("Introdu numele categoriei de masini...");
        var numeCategorie = Console.ReadLine();

        if (String.IsNullOrWhiteSpace(numeCategorie))
        {
            Console.WriteLine("Categorie de masini invalida");
            return;
        }
        else
        {

            var catt = carCategoryService.GetCategory(numeCategorie);
            if (catt == null)
            {
                Console.WriteLine("Categoria de masini nu exista in lista");
                return;
            }
            else
            {
                
                Console.WriteLine("Categoria de masini exista in lista");
            }
        }

        Console.WriteLine("Introdu VIN masina...");
        var vin = Console.ReadLine();
        if (String.IsNullOrWhiteSpace(vin))
        {
            Console.WriteLine("VIN invalid!");
            return;
        }

        Console.WriteLine("Introdu culoarea masinii..");
        var color = Console.ReadLine();
        if (String.IsNullOrWhiteSpace(color))
        {
            Console.WriteLine("Culoare invalida!");
            return;
        }

        Console.WriteLine("Introdu brandul masinii...");
        var brand = Console.ReadLine();
        if (String.IsNullOrWhiteSpace(brand))
        {
            Console.WriteLine("Brand invalid");
            return;
        }

        Console.WriteLine("Introdu numarul de usi masinii...");
        var doornr = Console.ReadLine();
        if (String.IsNullOrWhiteSpace(doornr))
        {
            Console.WriteLine("Campul numarul de usi este invalid");
            return;
        }

        //var all = carCategoryService.GetAllCategories();


        var cat = carCategoryService.GetCategory(numeCategorie);
        Console.WriteLine("Introdu tipul de masina (buget, premium, luxury)");
        var tipmasina = Console.ReadLine();
        do
        {
            if (String.IsNullOrWhiteSpace(tipmasina))
            {
                Console.WriteLine("Tip masina invalid");
                return;
            }
            else
            {
                tipmasina = tipmasina.ToUpper();
            }

            if (tipmasina == "BUGET")
            {
               // carService.AddBudgetCar(int.Parse(vin), color, brand, int.Parse(doornr), cat);
                break;
            }
            else if (tipmasina == "PREMIUM")
            {
                //carService.AddPremiumCar(int.Parse(vin), color, brand, int.Parse(doornr), cat);
                break;
            }
            else if (tipmasina == "LUXURY")
            {
                //carService.AddPremiumCar(int.Parse(vin), color, brand, int.Parse(doornr), cat);
                break;
            }
            else
            {
                Console.WriteLine("Tipul de masina nu este valid!");
                var tipmasina2 = Console.ReadLine();
                
                if (String.IsNullOrWhiteSpace(tipmasina2))
                {
                    Console.WriteLine("Tip masina invalid");
                    return;
                }
                else
                {
                    tipmasina = tipmasina2.ToUpper();
                }
            }
        }
        while ((tipmasina != "BUGET") || (tipmasina != "PREMIUM") || (tipmasina != "LUXURY")) ;
        
        Console.WriteLine("Masina a fost adaugata!");
    }

    public static void ListareCategorii(CarCategoryService carCategoryService)
    {
        var all = carCategoryService.GetAllCategories();

        if (all != null && all.Count == 0)
        {
            Console.WriteLine("Lista de categorii este goala!");
            return;
        }
        else
        {
            Console.WriteLine("Lista de categorii este...");
            foreach (var item in all)
            {
                Console.WriteLine("Categoria cu numele " + item.Name + " are capacitatea motorului de " + item.EngineCapacity + " si greutatea de " + item.Weight);

            }
        }
    }

    public static void ListareMasini(CarService carService)
    {
        var allcars = carService.GetAllCars();
        if (allcars != null && allcars.Count == 0)
        {
            Console.WriteLine("Lista de masini este goala!");
            return;
        }
        else if (allcars == null)
        {
            Console.WriteLine("Lista de masini este goala!");
            return;
        }
        else
        {
            Console.WriteLine("Lista de masini este...");
            foreach (var item in allcars)
            {
                Console.WriteLine($"Masina cu VIN-ul " + item.vin + " cu culoarea " + item.Color + " cu brandul " + item.Brand + " si cu numarul de usi " + item.DoorNr);

                //var x = item.GetaAditionalProprieties();

                // Console.WriteLine(item.GetaAditionalProprieties());

                //Console.WriteLine(item.GetType().Name);
                Console.WriteLine($"Masina are tipul de confort " + item.Type);
            }
        }
    }

    public static void StergeCategoria(CarCategoryService carCategoryService)
    {
        Console.WriteLine("Introdu numele categoriei pentru sters...");
        var name = Console.ReadLine();
        if (String.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Numele categoriei de masini este invalid");
            return;
        }

        var all = carCategoryService.GetCategory(name);
        if (all == null)
        {
            Console.WriteLine("Numele categoriei de masini este invalid pentru ca nu exista in memorie");
        }
        else
        {
            carCategoryService.DeleteCategory(name);
            Console.WriteLine("Categoria a fost stearsa...");
        }
    }

    public static void StergeMasina(CarService carService)
    {
        Console.WriteLine("Introdu VIN-ul masinii pentru sters...");
        var vin = Console.ReadLine();
        if (String.IsNullOrWhiteSpace(vin))
        {
            Console.WriteLine("VIN-ul masinii este invalid");
            return;
        }
      
        bool vinn;
        int a;
        vinn = int.TryParse(vin, out a);
        if (vinn == false)
        {
            Console.WriteLine("VIN masina invalid pentru ca este un caracter");
        }
        else
        {
            var carr = carService.GetCar(int.Parse(vin));
            if (carr == null)
            {
                Console.WriteLine("VIN-ul masinii de sters este invalid pentru ca nu exista in memorie");
            }
            else
            {
                carService.DeleteCar(int.Parse(vin));

                Console.WriteLine($"Masina cu " + int.Parse(vin) + " a fost stearsa...");
            }
        } 
    }

    public static void AfisareCategorie(CarCategoryService carCategoryService)
    {
        Console.WriteLine("Introdu numele categoriei pentru afisat...");
        var name = Console.ReadLine();
        if (String.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Numele categoriei de masini este invalid");
            return;
        }
       
        var all = carCategoryService.GetCategory(name);
        if (all == null)
        {
            Console.WriteLine("Numele categoriei de masini este invalid pentru ca nu exista in memorie");
        }
        else
        {
            Console.WriteLine($"Numele categoriei de afisat este " + all.Name + " si are capacitatea motorului de " + all.EngineCapacity + " cc si greutatea " + all.Weight + " tone");
        }
                
    }

    public static void AfisareMasina(CarService carService)
    {
        Console.WriteLine("Introdu VIN-ul pentru masina de afisat...");
        var vin = Console.ReadLine();
       
        if (String.IsNullOrWhiteSpace(vin))
        {
            Console.WriteLine("VIN masina invalid");
            return;
        }
                        
        bool vinn;
        int a;
        vinn = int.TryParse(vin, out a);
        if (vinn == false)
        {
            Console.WriteLine("VIN masina invalid pentru ca este un caracter");
        }
        else
        {
            var carr = carService.GetCar(a);
            //var b = carService.GetCar(int.Parse(vin));   
           
            if (carr == null)
            {
                Console.WriteLine("VIN masina invalid pentru ca nu exista in memorie");
                return;
            } 
            else if (a == int.Parse(vin))
            {
                Console.WriteLine("Masina de afisat...");

                Console.WriteLine($"are VIN-ul " + carr.vin + " cu culoarea " + carr.Color + " cu brand-ul " + carr.Brand + " si cu " + carr.DoorNr + " usi");
                //Console.WriteLine(carr.GetaAditionalProprieties());
               // Console.WriteLine(carr.GetType().Name);
                Console.WriteLine($"Are tipul de confort " + carr.Type);
            }
        }                      
    }

    public static void ListareMasinaAerConditionat(CarService carService)
    {

        var allair = carService.GetCarsWithAirConditioning(true);

        if (allair != null && allair.Count == 0)
        {
            Console.WriteLine("Lista de masini este goala!");
            return;
        }
        else if (allair == null)
        {
            Console.WriteLine("Lista de masini este goala!");
            return;
        }
        else
        {
            Console.WriteLine("Lista de masini cu aer conditionat este...");

            foreach (var item in allair)
            {
                Console.WriteLine("Masina cu VIN-UL " + item.vin);
            }
        }
    }
    public static void SchimbareCuloareMasina(CarService carService)
    {
        Console.WriteLine("Introdu VIN masina pentru care vrei sa schimbi culoarea...");
        var vin = Console.ReadLine();
        if (String.IsNullOrWhiteSpace(vin))
        {
            Console.WriteLine("VIN masina invalid");
            return;
        }
        else
        {
            Console.WriteLine("Introdu o noua culoare pentru masina...");
            var color = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(color))
            {
                Console.WriteLine("Culoarea de masina este invalida");
                return;
            }
            else
            {
                //carService.ChangeColor(int.Parse(vin), color);
                Console.WriteLine("Culoarea la masina cu VIN-ul " + int.Parse(vin) + " a fost schimbata in " + color);
            }
        }


    }
}