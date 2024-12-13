using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Auctions.Models;


namespace Auctions.Data
{
    public static class DatabaseSeeder
    {

        public static void Seed(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // ############################  add users  ############################
            SeedUsers(context);

            // ############################  add products  ############################
           SeedProducts(context);
        }

         
        private static void SeedUsers(ApplicationDbContext context)
        {
            // ############################  add admin user  ############################        
            const string ADMIN_USER_ID = "22e40406-8a9d-2d82-912c-5d6a640ee696";
            const string ADMIN_ROLE_ID = "b421e928-0613-9ebd-a64c-f10b6a706e73";

            // Add an admin role
            var identityRoleAdmin = new IdentityRole
            {
                Id = ADMIN_ROLE_ID,
                Name = "admin",
                NormalizedName = "ADMIN"
            };

            context.Roles.Add(identityRoleAdmin);

            // Add a user to be added to the admin role
            var identityUserAdmin = new IdentityUser
            {
                Id = ADMIN_USER_ID,
                Email = "admin@calicot.com",
                EmailConfirmed = true,
                UserName = "admin@calicot.com",
                NormalizedEmail = "admin@calicot.com".ToUpper(),
                NormalizedUserName = "admin@calicot.com".ToUpper(),
                SecurityStamp = "I5MOLV6IDX2DRGZMNIQ6KEUQKW3QIG3A",
                ConcurrencyStamp = "c4736b7b-4dcf-be6b-8b03-e299b4836146"
            };

            var ph = new PasswordHasher<IdentityUser>();
            identityUserAdmin.PasswordHash = ph.HashPassword(identityUserAdmin, "Toto12345!");

            context.Users.Add(identityUserAdmin);

            // Add the user to the admin role
            context.UserRoles.Add(
            new IdentityUserRole<string>
            {
                RoleId = ADMIN_ROLE_ID,
                UserId = ADMIN_USER_ID
            });

            // ############################  add user 1  ############################


            const string USER1_ID = "33e40406-8a9d-2d82-912c-5d6a640ee696";
            const string USER_ROLE_ID = "b421e928-0613-9ebd-a64c-f10b6a706e74";

            // Add an user role
            var identityRoleUser = new IdentityRole
            {
                Id = USER_ROLE_ID,
                Name = "user",
                NormalizedName = "USER"
            };

            context.Roles.Add(identityRoleUser);

            // create a new user
            var identityUser1 = new IdentityUser
            {
                Id = USER1_ID,
                Email = "user1@calicot.com",
                EmailConfirmed = true,
                UserName = "user1@calicot.com",
                NormalizedEmail = "user1@calicot.com".ToUpper(),
                NormalizedUserName = "user1@calicot.com".ToUpper(),
                SecurityStamp = "I5MOLV6IDX2DRGZMNIQ6KEUQKW3QIG3B",
                ConcurrencyStamp = "c4736b7b-4dcf-be6b-8b03-e299b4836147"
            };

            ph = new PasswordHasher<IdentityUser>();
            identityUser1.PasswordHash = ph.HashPassword(identityUser1, "Toto12345!");

            context.Users.Add(identityUser1);

            // Add the user to the user role
            context.UserRoles.Add(
            new IdentityUserRole<string>
            {
                RoleId = USER_ROLE_ID,
                UserId = USER1_ID
            });


            // ############################  add user 2  ############################

            const string USER2_ID = "44e40406-8a9d-2d82-912c-5d6a640ee696";

            // create a new user
            var identityUser2 = new IdentityUser
            {
                Id = USER2_ID,
                Email = "user2@calicot.com",
                EmailConfirmed = true,
                UserName = "user2@calicot.com",
                NormalizedEmail = "user2@calicot.com".ToUpper(),
                NormalizedUserName = "user2@calicot.com".ToUpper(),
                SecurityStamp = "I5MOLV6IDX2DRGZMNIQ6KEUQKW3QIG3C",
                ConcurrencyStamp = "c4736b7b-4dcf-be6b-8b03-e299b4836148"
            };

            ph = new PasswordHasher<IdentityUser>();
            identityUser2.PasswordHash = ph.HashPassword(identityUser2, "Toto12345!");

            context.Users.Add(identityUser2);

            // Add the user to the user role
            context.UserRoles.Add(
            new IdentityUserRole<string>
            {
                RoleId = USER_ROLE_ID,
                UserId = USER2_ID
            });

            // ############################  save changes  ############################
            context.SaveChanges();
        }
    

        private static void SeedProducts(ApplicationDbContext context)
        {
            var identityUserAdmin = context.Users.FirstOrDefault(u => u.Email == "admin@calicot.com");

            if (!context.Listings.Any())
            {
                context.Listings.AddRange(
                    new Listing
                        {
                            Title = "Xbox 360",
                            Description = "Xbox 360 usagée mais en excellente condition, avec 2 manettes et 5 jeux.",
                            Price = 199,
                            ImagePath = "xbox360.jpg",
                            IdentityUserId = identityUserAdmin?.Id
                        },
                        new Listing
                        {
                            Title = "iPhone 15 Pro",
                            Description = "iPhone 15 Pro, 256GB, couleur or rose, en excellente condition.",
                            Price = 699,
                            ImagePath = "iphone-15-pro.jpg",
                            IdentityUserId = identityUserAdmin?.Id
                        },
                        new Listing
                        {
                            Title = "MacBook Air M2",
                            Description = "MacBook Air 13 pouces, M2, CPU 10 coeurs, 16Go RAM, 512Go SSD, en excellente condition.",
                            Price = 1199,
                            ImagePath = "macbook-air-13.jpg",
                            IdentityUserId = identityUserAdmin?.Id
                        },
                        new Listing
                        {
                            Title = "Samsung Galaxy Watch 5",
                            Description = "Montre intelligente Samsung Galaxy Watch 5, jamais utilisée, dans sa boîte.",
                            Price = 375,
                            ImagePath = "samsung-galaxy-watch5.jpg",
                            IdentityUserId = identityUserAdmin?.Id
                        }
                );
            }

            // ############################  save changes  ############################
            context.SaveChanges();
        }
    }
}