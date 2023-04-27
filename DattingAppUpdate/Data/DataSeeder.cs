using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.DependencyInjection;
using DattingAppUpdate.Entites;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.IO;
using DattingAppUpdate.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DattingAppUpdate.Data
{
    public class DataSeeder
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetRequiredService<UserDbCxt>())
            {
                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();        

                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

                await context.Database.EnsureCreatedAsync();

                try
                {
                    if (!context.Users.Any())
                    {                      
    
                        await roleManager.CreateAsync(new IdentityRole<int>(UserRoles.Admin));
                        await roleManager.CreateAsync(new IdentityRole<int>(UserRoles.Moderator));
                        await roleManager.CreateAsync(new IdentityRole<int>(UserRoles.Member));

                        var usersDataTxt = File.ReadAllText("Data/SeedData/UserSeedData.json");
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };
                        var usersToSeed = JsonSerializer.Deserialize<List<UserToSeed>>(usersDataTxt, options);

                        foreach (var userToSeed in usersToSeed)
                        {
                            User user = new()
                            {
                                Email = userToSeed.Username + "@gmail.com",
                                SecurityStamp = Guid.NewGuid().ToString(),
                                UserName = userToSeed.Username,
                                DateOfBirth = userToSeed.DateOfBirth,
                                Gender = userToSeed.Gender,
                                KnowsAs = userToSeed.KnowsAs,
                                Created = userToSeed.Created,
                                LastActive = userToSeed.LastActive,
                                Introduction = userToSeed.Introduction,
                                LookingFor = userToSeed.LookingFor,
                                Interests = userToSeed.Interests,
                                City = userToSeed.City,
                                Country = userToSeed.Country
                            };                       

                            var result = await userManager.CreateAsync(user, userToSeed.Password);

                            if (!result.Succeeded)
                                throw new Exception("failed to create user !");                          

                            var createdUser = await context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);

                            createdUser.Photos = userToSeed.Photos;

                            await userManager.AddToRoleAsync(createdUser, UserRoles.Member);

                            //context.Users.Update(createdUser);  

                        }

                        var admin = new User
                        {
                            UserName = "admin"
                        };

                        await userManager.CreateAsync(admin, "Password@123");
                        await userManager.AddToRolesAsync(admin, new[] {"Admin", "Moderator"});

                        await context.SaveChangesAsync();
                    }                   
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<DataSeeder>();
                    logger.LogError(ex.Message);
                }

            }
        }
    }
}
