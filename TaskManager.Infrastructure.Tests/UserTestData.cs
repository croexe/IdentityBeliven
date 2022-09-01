using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Helpers;

namespace TaskManager.Infrastructure.Tests;

internal static class UserTestData
{
    public async static void CreateUsers(UserManager<IdentityUser> _userManager, RoleManager<IdentityRole> _roleManager)
    {
        var users = UserData();
        foreach (var user in users)
        {
            var userExists = await _userManager.FindByNameAsync(user.Username);
            if (userExists == null)
            {
                IdentityUser _user = new IdentityUser()
                {
                    Id = user.Id,
                    Email = user.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = user.Username
                };
                await _userManager.CreateAsync(_user, user.Password);

                if (!await _roleManager.RoleExistsAsync(UserRoles.Manager))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));
                if (!await _roleManager.RoleExistsAsync(UserRoles.Developer))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Developer));

                if (await _roleManager.RoleExistsAsync(UserRoles.Manager))
                {
                    if (user.Usertype == UserRoles.Manager)
                        await _userManager.AddToRoleAsync(_user, UserRoles.Manager);
                }

                if (await _roleManager.RoleExistsAsync(UserRoles.Developer))
                {
                    if (user.Usertype == UserRoles.Developer)
                        await _userManager.AddToRoleAsync(_user, UserRoles.Developer);
                }
            }
        }
    }

    public static List<RegisterTestModel> UserData()
    {
        List<RegisterTestModel> userMockData = new List<RegisterTestModel>()
        {
            new RegisterTestModel()
            {
                Id = "b2334e9a-a8d6-49b8-8a13-b77e1729d1b5",
                Username = "irinejs@gmail.com",
                Password = "/3366Ttxxx@",
                Email="irinejs@gmail.com",
                Usertype="ProjectManager"
            },
            new RegisterTestModel()
            {
                Id = "90149c80-754f-44a6-9f09-6975492ae019",
                Username = "petars@gmail.com",
                Password = "/3367Ttxxx@",
                Email="petars@gmail.com",
                Usertype="Developer"
            },
            new RegisterTestModel()
            {
                Id = "c2e2f9b2-8f26-41ed-8a65-919df73e873b",
                Username = "ivans@gmail.com",
                Password = "/3368Ttxxx@",
                Email="ivans@gmail.com",
                Usertype="Developer"
            }
        };
        return userMockData;
    }
}
