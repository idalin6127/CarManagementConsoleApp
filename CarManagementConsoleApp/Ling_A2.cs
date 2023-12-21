using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LING_A2
{
    /* Project Name: Car Management Console App
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("LING LIN -- 301292283 ");
            TestDealership();
        }
        static void TestDealership()
        {
            Console.WriteLine("Assignment 2 Output\n");

            Dealership dealership1 = new Dealership("D1_22_T501", "The Six Cars", "1 Main Street, Toronto");
            Console.WriteLine(dealership1.ToString());

            Dealership dealership2 = new Dealership("D2_22_B321", "Car Street", "5th avenue, Brampton");
            Console.WriteLine(dealership2.ToString());

            Console.WriteLine("\nToyota Cars available in Dealership 1");
            dealership1.ShowCars("Toyota");

            Console.WriteLine("\nToyota Cars available in Dealership 2");
            dealership2.ShowCars("Toyota");

            Car favCar = new Car("Hyundai", 2020, "Elantra", 30000.00, CarType.Sedan);
            Console.WriteLine($"\nCar to match : {favCar.ToString()}");

            Console.WriteLine("\nMatching car(s) from Dealership 1 : ");
            dealership1.ShowCars(favCar);

            Console.WriteLine("\nMatching car(s) from Dealership 2 : ");
            dealership2.ShowCars(favCar);

            //favCar = new Car("Honda", 2018, "Civic", 20000.00, CarType.SUV, CarSpecifications.FogLights | CarSpecifications.TintendGlasses);
            favCar = new Car("Honda", 2018, "Civic", 20000.00, CarType.SUV);

            Console.WriteLine($"\nCar to match : {favCar.ToString()}");

            Console.WriteLine("\nMatching car(s) from Dealership 1 : ");
            dealership1.ShowCars(favCar);

            Console.WriteLine("\nMatching car(s) from Dealership 2 : ");
            dealership2.ShowCars(favCar);

            Console.WriteLine("\nList of similiar car models available in both dealership : ");

            foreach (Car firstCar in dealership1.CarList)
            {
                foreach (Car secondsCar in dealership2.CarList)
                {
                    if (firstCar == secondsCar)
                    {
                        Console.WriteLine($"Dealership 1 : {firstCar.ToString()}");
                        Console.WriteLine($"Dealership 2 : {secondsCar.ToString()}");
                    }
                }
            }
        }

        public enum CarType
        {
            SUV, Hatchback, Sedan, Truck
        }

        public class Car
        {
            public string Manufacturer { get; }
            public int Make { get; }
            public string Model { get; }
            public int VI_NUMBER = 1021;
            public int VIN;
            public double BasePrice { get; }
            public CarType Type { get; }

            public Car(string manufacturer, int make, string model, double basePrice, CarType type)
            {

                this.Manufacturer = manufacturer;
                this.Make = make;
                this.Model = model;
                this.BasePrice = basePrice;
                this.Type = type;
                VI_NUMBER += 100;
                VIN = VI_NUMBER;

            }

            public static bool operator == (Car first, Car second)

            {
                bool result = false;
                if (first.Manufacturer == second.Manufacturer && first.Model == second.Model && first.Type == second.Type)
                {
                    result = true;
                }

                return result;
            }

            public static bool operator != (Car first, Car second)
            {
                bool result = false;

                if (first.Manufacturer != second.Manufacturer || first.Model != second.Model || first.Type != second.Type)
                {
                    result = true;
                }

                return result;
            }

            public override string ToString()
            {
                return $"{this.VIN}, {this.Manufacturer}, {this.Make}, {this.Model}, {this.BasePrice}, {this.Type}";
            }
        }


        public class Dealership
        {
            private static string fileName = "Dealership_Cars.txt";
            public List<Car> CarList;
            public string ID { get; }
            public string Name { get; }
            public string Address { get; }

            public Dealership(string ID, string name, string address)
            {
                this.ID = ID;
                this.Name = name;
                this.Address = address;
                this.CarList = new List<Car>();

                using (StreamReader reader = new StreamReader(fileName))
                {
                    string dataLine;
                    while ((dataLine = reader.ReadLine()) != null)
                    {
                        string[] values = dataLine.Split(',');
                        if (values[0] == this.ID)
                        {
                            string manufacturer = values[1];
                            int make = Int32.Parse(values[2]);
                            string model = values[3];
                            double basePrice = double.Parse(values[4]);
                            CarType type = (CarType)Enum.Parse(typeof(CarType), values[5]);
                            Car newCar = new Car(manufacturer, make, model, basePrice, type);
                            this.CarList.Add(newCar);
                        }
                    }
                    reader.Close();
                }
            }

            public void ShowCars()
            {
                foreach (Car car in this.CarList)
                {
                    Console.WriteLine(car);
                }
            }
            public void ShowCars(string manufacturer)
            {
                foreach (Car car in this.CarList)
                {
                    if (car.Manufacturer == manufacturer)
                    {
                        Console.WriteLine($"\n{car}");
                    }
                }
            }
            public void ShowCars(Car carToBeSearched)
            {
                var matches = new List<Car>();
                for (int i = 0; i < CarList.Count; i++)
                {
                    if (CarList[i] == carToBeSearched)
                    {
                        matches.Add(CarList[i]);
                    }
                }
                if (matches.Count == 0)
                {
                    Console.WriteLine("None");
                }
                else
                {
                    foreach (Car car in matches)
                    {
                        Console.WriteLine($"\n{car}");
                    }
                }
            }
            public override string ToString()
            {
                return $"{this.ID}, {this.Name}, {this.Address}";
            }
        }
    }
}









