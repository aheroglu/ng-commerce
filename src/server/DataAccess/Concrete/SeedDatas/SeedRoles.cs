using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace DataAccess.Concrete.SeedDatas
{
    public class SeedRoles
    {
        public static async Task SetData(RoleManager<AppRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                var roles = File.ReadAllText("../DataAccess/Concrete/SeedDatas/roles.json");
                var listOfRoles = JsonConvert.DeserializeObject<List<AppRole>>(roles);

                foreach (var role in listOfRoles)
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }
    }
}
