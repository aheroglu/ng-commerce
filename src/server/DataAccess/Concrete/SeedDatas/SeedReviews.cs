using DataAccess.Concrete.Contexts;
using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DataAccess.Concrete.SeedDatas
{
    public class SeedReviews
    {
        public static async Task SetData(AppDbContext context, UserManager<AppUser> userManager)
        {
            if (!context.Reviews.Any())
            {
                var reviews = File.ReadAllText("../DataAccess/Concrete/SeedDatas/reviews.json");
                var listOfReviews = JsonConvert.DeserializeObject<List<Review>>(reviews);

                foreach (var review in listOfReviews)
                {
                    await context.Reviews.AddAsync(review);
                    await context.SaveChangesAsync();
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
