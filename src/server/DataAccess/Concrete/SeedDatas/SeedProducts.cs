using DataAccess.Concrete.Contexts;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DataAccess.Concrete.SeedDatas
{
    public class SeedProducts
    {
        public static async Task SetData(AppDbContext context)
        {
            if (!context.Products.Any())
            {
                var products = File.ReadAllText("../DataAccess/Concrete/SeedDatas/products.json");
                var listOfProduct = JsonConvert.DeserializeObject<List<Product>>(products);

                foreach (var product in listOfProduct)
                {
                    await context.Products.AddAsync(product);
                    await context.SaveChangesAsync();
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
