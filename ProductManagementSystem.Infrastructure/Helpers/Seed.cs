using Microsoft.AspNetCore.Identity;
using ProductManagementSystem.Domain.Users;

namespace ProductManagementSystem.Infrastructure.Helpers
{
    public class Seed
    {
        public static async Task SeedData(UserManager<AppUser> userManager)
        {
            if (userManager.Users.Any()) return;

            var user = new AppUser
            {
                FirstName = "Jose",
                LastName =  "De Leon",
                DisplayName = "Jose",
                UserName = "jdeleon",
                Email = "josealbertodeleon8@gmail.com"
            };

            await userManager.CreateAsync(user, "myPassw@ord");

        }
    }
}
