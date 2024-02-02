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
    public class SeedDistricts
    {
        public static async Task SetData(AppDbContext context)
        {
            if (!context.Districts.Any())
            {
                var districts = File.ReadAllText("../DataAccess/Concrete/SeedDatas/districts.json");
                var listOfDistricts = JsonConvert.DeserializeObject<List<District>>(districts);

                foreach (var district in listOfDistricts)
                {
                    await context.Districts.AddAsync(district);
                    await context.SaveChangesAsync();
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
