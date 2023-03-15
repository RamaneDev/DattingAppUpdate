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
using DattingAppUpdate.Dtos;
using System.Globalization;
using DattingAppUpdate.Helpers;
using Microsoft.AspNetCore.Http;
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

                await context.Database.EnsureCreatedAsync();

                try
                {
                    if (!context.Users.Any())
                    {
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

                            //context.Users.Update(createdUser);  
      
                        }

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
