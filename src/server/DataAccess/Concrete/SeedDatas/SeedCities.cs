using DataAccess.Concrete.Contexts;
using Entity.Concrete;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.SeedDatas
{
    public class SeedCities
    {
        public static async Task SetData(AppDbContext context)
        {
            if (!context.Cities.Any())
            {
                var cities = File.ReadAllText("../DataAccess/Concrete/SeedDatas/cities.json");
                var listOfCities = JsonConvert.DeserializeObject<List<City>>(cities);

                foreach (var city in listOfCities)
                {
                    await context.Cities.AddAsync(city);
                    await context.SaveChangesAsync();
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
