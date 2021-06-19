using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities.Products;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Core.Entities.Order;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        private const string PRODUCTS_CATEGORIES_PATH = "../Infrastructure/Data/SeedData/categories.json";
        private const string INFO_CATEGORIES_PATH = "../Infrastructure/Data/SeedData/infoCategories.json";
        private const string INFO_NAMES_PATH = "../Infrastructure/Data/SeedData/infoNames.json";
        private const string TECHNICAL_SHEETS_PATH = "../Infrastructure/Data/SeedData/technicalsheets.json";
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
                if (!context.ProductCategories.Any())
                {
                    var data = File.ReadAllText(PRODUCTS_CATEGORIES_PATH);
                    var list = JsonSerializer.Deserialize<List<ProductCategory>>(data);

                    foreach (var item in list)
                    {
                        context.ProductCategories.Add(item);
                    }
                }

                if (!context.ProductTypes.Any())
                {
                    var data = File.ReadAllText(PRODUCTS_TYPES_PATH);
                    var list = JsonSerializer.Deserialize<List<ProductType>>(data);

                    foreach (var item in list)
                    {
                        context.ProductTypes.Add(item);
                    }
                }

                if (!context.AdditionalInfoCategories.Any())
                {
                    var data = File.ReadAllText(INFO_CATEGORIES_PATH);
                    var list = JsonSerializer.Deserialize<List<AdditionalInfoCategory>>(data);

                    foreach (var item in list)
                    {
                        context.AdditionalInfoCategories.Add(item);
                    }
                }

                // if(!context.AdditionalInfoNames.Any())
                // {
                //     var data = File.ReadAllText(INFO_NAMES_PATH);
                //     var list = JsonSerializer.Deserialize<List<AdditionalInfoName>>(data);

                //     foreach (var item in list)
                //     {
                //         context.AdditionalInfoNames.Add(item);
                //     }
                // }



                if (!context.Products.Any())
                {
                    var data = File.ReadAllText(PRODUCTS_PATH);
                    var list = JsonSerializer.Deserialize<List<Product>>(data);

                    foreach (var item in list)
                    {
                        context.Products.Add(item);
                    }
                }
                if (!context.TechnicalSheets.Any())
                {
                    var data = File.ReadAllText(TECHNICAL_SHEETS_PATH);
                    var list = JsonSerializer.Deserialize<List<TechnicalSheet>>(data);

                    foreach (var item in list)
                    {
                        context.TechnicalSheets.Add(item);
                    }
                }
                if (!context.DeliveryMethods.Any())
                {
                    var dmData = File.ReadAllText(DELIVERY_METHOD_PATH);
                    var dms = JsonSerializer.Deserialize<List<DeliveryMethod>>(dmData);

                    foreach (var dm in dms)
                    {
                        context.DeliveryMethods.Add(dm);
                    }

                    await context.SaveChangesAsync();
                }

                if (!userManager.Users.Any())
                {
                    var userData = File.ReadAllText(USERS_PATH);
                    var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

                    var rolesData = File.ReadAllText(ROLES_PATH);
                    var roles = JsonSerializer.Deserialize<List<AppRole>>(rolesData);

                    foreach (var role in roles)
                    {
                        await roleManager.CreateAsync(role);
                    }

                    foreach (var user in users)
                    {
                        await userManager.CreateAsync(user, "passw0rd");

                        if (user.UserName != "Nassim")
                            await userManager.AddToRoleAsync(user, "Client");
                        if (user.UserName == "Nassim")
                            await userManager.AddToRolesAsync(user, new[] { "Administrator", "Client" });
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