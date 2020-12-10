using MKTFY.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Seeds
{
    public static class UserAndRoleSeeder
    {
        public static async Task SeedUsersAndRoles(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            var roleResult = await roleManager.RoleExistsAsync("administrator");
            if (!roleResult)
            {
                await roleManager.CreateAsync(new IdentityRole("administrator"));
            }
            
            roleResult = await roleManager.RoleExistsAsync("user");
            if (!roleResult)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }

            var userResult = await userManager.FindByNameAsync("thiago@launchpadbyvog.com");
            if (userResult == null)
            {
                var user = new User
                {
                    UserName = "thiago@launchpadbyvog.com",
                    Email = "thiago@launchpadbyvog.com",
                    FirstName = "Thiago",
                    LastName = "Admin"
                };
                IdentityResult result = await userManager.CreateAsync(user, "Password1");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "administrator");
                }
            }
        }
    }
}
