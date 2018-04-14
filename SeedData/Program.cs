using CarShop.Model;
using System;
using System.Linq;

namespace SeedData
{
    class Program
    {
        static void Main(string[] args)
        {
            var connString = "Server=.;Database=CarShopDB;Trusted_Connection=True;";
           
            var context = new CarDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<CarDbContext>());
      
            var rdn = new Random();
            foreach(var carModel in context.CarModels.ToList())
            {
                foreach(var feature in context.CarFeatures)
                {
                    if (rdn.Next(0, 10) < 2) continue;

                    context.CarFeatureModels.Add(new CarFeatureModel()
                    {
                        CarFeature = feature,
                        CarModel = carModel,
                        Price = feature.CarFeatureType == CarFeatureType.Color ? 0 : (100 + rdn.Next(100, 200))
                    });

                }
            }
            context.SaveChanges();


            Console.WriteLine("Hello World!");
        }
    }
}
