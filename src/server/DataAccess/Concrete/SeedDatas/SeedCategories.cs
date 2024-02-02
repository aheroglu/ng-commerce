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
    public class SeedCategories
    {
        public static async Task SetData(AppDbContext context)
        {
            if (!context.Categories.Any())
            {
                var categories = File.ReadAllText("../DataAccess/Concrete/SeedDatas/categories.json");
                var listOfCategories = JsonConvert.DeserializeObject<List<Category>>(categories);

                foreach (var category in listOfCategories)
                {
                    await context.Categories.AddAsync(category);
                    await context.SaveChangesAsync();
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
