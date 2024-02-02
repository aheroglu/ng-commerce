using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace DataAccess.Concrete.SeedDatas
{
    public class SeedUsers
    {
        public static async Task SetData(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = File.ReadAllText("../DataAccess/Concrete/SeedDatas/users.json");
                var listOfUsers = JsonConvert.DeserializeObject<List<AppUser>>(users);

                foreach (var user in listOfUsers)
                {
                    var result = await userManager.CreateAsync(user, "Default@password1");

                    if (result.Succeeded)
                    {
                        if (user.UserName == "Ahmet Hakan Eroğlu")
                        {
                            await userManager.AddToRoleAsync(user, "Admin");
                        }

                        else
                        {
                            await userManager.AddToRoleAsync(user, "Member");
                        }
                    }
                }
            }
        }
    }
}
