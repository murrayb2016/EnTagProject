using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using EnTag.Models;

namespace EnTag.Data
{
    public class SampleData
    {
        public async static Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            // Ensure db
            context.Database.EnsureCreated();

            // Ensure Stephen (IsAdmin)
            var stephen = await userManager.FindByNameAsync("Stephen.Walther@CoderCamps.com");
            if (stephen == null)
            {
                // create user
                stephen = new ApplicationUser
                {
                    UserName = "Stephen.Walther@CoderCamps.com",
                    Email = "Stephen.Walther@CoderCamps.com"
                };
                await userManager.CreateAsync(stephen, "Secret123!");

                // add claims
                await userManager.AddClaimAsync(stephen, new Claim("IsAdmin", "true"));
            }

            // Ensure Mike (not IsAdmin)
            var mike = await userManager.FindByNameAsync("Mike@CoderCamps.com");
            if (mike == null)
            {
                // create user
                mike = new ApplicationUser
                {
                    UserName = "Mike@CoderCamps.com",
                    Email = "Mike@CoderCamps.com"
                };
                await userManager.CreateAsync(mike, "Secret123!");
            }

            if(!context.OurTokens.Any())
            {
                context.OurTokens.AddRange(
                    new OurToken()
                    {
                        Token = "AIzaSyBYsHBvPPA98VZTGVlfU9RkQPqUdATE4l4",
                        Secret = "Public Key",
                        Service = "YouTube"
                    },
                    new OurToken()
                    {
                        Token = "pH8Qoql342mGPXHNY0Wnk3LZX",
                        Secret = "kzIFhr3VaTW9yZJFh4WVvltFA45RXPjMD8IvxkcA2xvbwG5Lsk",
                        Service = "Twitter"
                    });
                context.SaveChanges();
            }

        }

    }
}
