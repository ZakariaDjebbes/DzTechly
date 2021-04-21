using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        private const string PRODUCTS_BRANDS_PATH = "../Infrastructure/Data/SeedData/brands.json";
        private const string PRODUCTS_TYPES_PATH = "../Infrastructure/Data/SeedData/types.json";
        private const string PRODUCTS_PATH = "../Infrastructure/Data/SeedData/products.json";
        private const string DELIVERY_METHOD_PATH = "../Infrastructure/Data/SeedData/delivery.json";
        private const string USERS_PATH = "../Infrastructure/Data/SeedData/users.json";
        private const string ROLES_PATH = "../Infrastructure/Data/SeedData/roles.json";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<Pending>")]
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            try
            {
                if (!userManager.Users.Any())
                {
                    var userData = File.ReadAllText(USERS_PATH);
                    var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

                    // var rolesData = File.ReadAllText(ROLES_PATH);
                    // var roles = JsonSerializer.Deserialize<List<AppRole>>(rolesData);

                    // foreach (var role in roles)
                    // {
                    //     await roleManager.CreateAsync(role);
                    // }

                    foreach (var user in users)
                    {
                        await userManager.CreateAsync(user, "passw0rd");
                        //await userManager.AddToRoleAsync(user, "Client");

                        // if (user.UserName == "Admin")
                        //     await userManager.AddToRolesAsync(user, new[] { "Administrator", "Moderator" });
                        // if (user.UserName == "Moderator")
                        //     await userManager.AddToRoleAsync(user, "Moderator");

                    }
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex, "An error occured while seeding data");
            }
        }
    }
}