using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Enities;

namespace Talabat.Repository.Data.DataSeeding
{
    public static class IdentityUserSeeding
    {

        public static async Task UserSeedAsync(UserManager<AppUser> _UserManager, ILogger logger)
        {

            try
            {
                if (_UserManager.Users.Count() == 0)
                {
                    var user = new AppUser()
                    {
                        DisplayName = "Ahmed Safwat",
                        Email = "ahmedsfwat069@gmail.com",
                        UserName = "ahmedsfwat069",
                        PhoneNumber = "01110351045"
                    };

                    var result = await _UserManager.CreateAsync(user, "Pa$$w0rd");
                    if (result.Succeeded)
                    {
                        logger.LogInformation("User created successfully.");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            logger.LogError("Error creating user: {ErrorDescription}", error.Description);
                        }
                        throw new Exception("User creation failed.");
                    }
                }
                else
                {
                    logger.LogInformation("Users already exist. Seeding skipped.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the user.");
                throw; // rethrow the exception to ensure the error is not swallowed
            }
        }


    }
}

