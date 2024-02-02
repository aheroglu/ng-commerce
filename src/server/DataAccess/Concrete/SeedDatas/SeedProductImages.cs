using DataAccess.Concrete.Contexts;
using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.SeedDatas
{
    public class SeedProductImages
    {
        public static async Task SetData(AppDbContext context)
        {
            if (!context.Reviews.Any())
            {
                var productImages = File.ReadAllText("../DataAccess/Concrete/SeedDatas/productimages.json");
                var listOfProductImages = JsonConvert.DeserializeObject<List<ProductImage>>(productImages);

                foreach (var productImage in listOfProductImages)
                {
                    await context.ProductImages.AddAsync(productImage);
                    await context.SaveChangesAsync();
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
