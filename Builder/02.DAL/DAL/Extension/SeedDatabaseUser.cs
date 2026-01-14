using Entities.TableModels.Membership;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Extension
{
    public static class SeedDatabaseUser
    {
        public static IApplicationBuilder SeedMembership(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

                string roleName = "SuperAdmin";
                string email = "info@orkhan.com";
                string userName = "Orkhan";
                string password = "Orkhan_2025!@0_";


                var role = new AppRole
                {
                    Name = roleName
                };

                if (!roleManager.RoleExistsAsync(role.Name).Result)
                {
                    var roleCreateResult = roleManager.CreateAsync(role).Result;

                }

                Console.WriteLine($"{roleName} rolü başarıyla oluşturuldu.");


                var user = userManager.FindByEmailAsync(email).Result;
                if (user == null)
                {
                    user = new AppUser
                    {
                        FirstName = "Orkhan",
                        LastName = "Mansurov",
                        UserName = userName,
                        Email = email,
                        EmailConfirmed = true
                    };

                    var userCreateResult = userManager.CreateAsync(user, password).Result;
                    if (!userCreateResult.Succeeded)
                    {
                        Console.WriteLine($"Kullanıcı oluşturma sırasında hata: {string.Join(", ", userCreateResult.Errors.Select(e => e.Description))}");
                        return app;
                    }
                    Console.WriteLine($"{userName} kullanıcı başarıyla oluşturuldu.");
                }
                else
                {
                    Console.WriteLine($"{email} emailine sahip kullanıcı zaten mevcut.");
                }


                if (!userManager.IsInRoleAsync(user, role.Name).Result)
                {
                    var addToRoleResult = userManager.AddToRoleAsync(user, role.Name).Result;
                    if (!addToRoleResult.Succeeded)
                    {
                        Console.WriteLine($"Kullanıcıya rol atanırken hata: {string.Join(", ", addToRoleResult.Errors.Select(e => e.Description))}");
                        return app;
                    }
                    Console.WriteLine($"{userName} kullanıcısına {roleName} rolü başarıyla atandı.");
                }
                else
                {
                    Console.WriteLine($"{userName} kullanıcısı zaten {roleName} rolünde.");
                }
            }

            return app;

        }
    }
}
