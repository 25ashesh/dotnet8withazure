using FrontLineCleaners.Domain.Constants;
using FrontLineCleaners.Domain.Entities;
using FrontLineCleaners.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace FrontLineCleaners.Infrastructure.Seeders;

internal class FrontLineCleanersSeeder(FrontLineCleanersDbContext dbContext): IFrontLineCleanersSeeder
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Cleaners.Any())
            {
                var cleaners = GetCleaners();
                dbContext.Cleaners.AddRange(cleaners);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }
        }
    }
    private IEnumerable<IdentityRole> GetRoles() 
    {
        List<IdentityRole> roles =
            [
                new(UserRoles.User)
                {
                    NormalizedName = UserRoles.User.ToUpper()
                },
                new(UserRoles.Owner) 
                {
                    NormalizedName = UserRoles.Owner.ToUpper()
                },
                new(UserRoles.Admin) 
                {
                    NormalizedName = UserRoles.Admin.ToUpper()
                },
            ];
        return roles;
    }
    private IEnumerable<Cleaner> GetCleaners()
    {
        List<Cleaner> cleaners = [
            new()
            {
                Name = "K1 Cleaner",
                Category = "Living",
                Description = "We clean your home",
                ContactEmail = "contact@k1cleaners.com",
                HasOwnSupplies = true,
                Services =
                [
                    new()
                    {
                        Name = "Apartment",
                        Description = "clean apartment units",
                        Cost = 100M,
                    },

                    new()
                    {
                        Name = "House",
                        Description = "clean whole house",
                        Cost = 250M,
                    },
                ],
                Address = new()
                {
                    City = "London",
                    Street = "Cork St 5",
                    PostalCode = "WC2N 5DU"
                },

            },
            new()
            {
                Name = "Doe Cleaner",
                Category = "Complete",
                Description = "We clean everything",
                ContactEmail = "contact@doecleaner.com",
                HasOwnSupplies = true,
                Address = new Address()
                {
                    City = "London",
                    Street = "Boots 193",
                    PostalCode = "W1F 8SR"
                }
            }
        ];
        return cleaners;
    }
}
