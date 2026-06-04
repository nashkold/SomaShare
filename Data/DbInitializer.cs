using Microsoft.AspNetCore.Identity;
using SomaShare.Models;
using Microsoft.EntityFrameworkCore;

namespace SomaShare.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            context.Database.Migrate();
        }

        public static async Task SeedRolesAndUsers(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // Create roles
            string[] roles = { "Admin", "Seller", "Buyer" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Create default Admin User
            if (await userManager.FindByEmailAsync("admin@somashare.ac.za") == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin@somashare.ac.za",
                    Email = "admin@somashare.ac.za",
                    FullName = "Admin User",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(admin, "Admin@1234");
                await userManager.AddToRoleAsync(admin, "Admin");
            }
            //Create Sample Users
            var buyerEmail = "buyer@somashare.ac.za";
            if (await userManager.FindByEmailAsync("buyer@somashare.ac.za") == null)
            {
                var buyer = new ApplicationUser
                {
                    UserName = "buyer@somashare.ac.za",
                    Email = "buyer@somashare.ac.za",
                    FullName = "Test Buyer",
                    Campus = "Johannesburg",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(buyer, "Buyer@1234");
                await userManager.AddToRoleAsync(buyer, "Buyer");
            }

            var sellerEmail = "seller@somashare.ac.za";
            if (await userManager.FindByEmailAsync("seller@somashare.ac.za") == null)
            {
                var seller = new ApplicationUser
                {
                    UserName = "seller@somashare.ac.za",
                    Email = "seller@somashare.ac.za",
                    FullName = "Test Seller",
                    Campus = "Centurion",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(seller, "Seller@1234");
                await userManager.AddToRoleAsync(seller, "Seller");
            }

            //Create Sample Data
            if (!context.Textbooks.Any())
            {
                var seller = await userManager.FindByEmailAsync("seller@somashare.ac.za");
                if (seller == null)
                    return;

                context.Textbooks.AddRange(
                    new Textbook
                    {
                        Title = "Introduction to Web Programming",
                        Author = "Thomas H. Cormen",
                        ISBN = "9780262033848",
                        Condition = "Good",
                        Price = 650,
                        Campus = "Johannesburg",
                        SellerId = seller.Id
                    },
                    new Textbook
                    {
                        Title = "Database System Concepts",
                        Author = "Silberschatz",
                        ISBN = "9780073523323",
                        Condition = "Like New",
                        Price = 700,
                        Campus = "Centurion",
                        SellerId = seller.Id
                    }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}

