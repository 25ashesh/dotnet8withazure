using FrontLineCleaners.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FrontLineCleaners.Infrastructure.Persistence;
internal class FrontLineCleanersDbContext(DbContextOptions<FrontLineCleanersDbContext> options) : IdentityDbContext<User>(options)
{
    internal DbSet<Cleaner> Cleaners { get; set; }
    internal DbSet<Service> Services { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer();
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cleaner>()
            .OwnsOne(r => r.Address);

        modelBuilder.Entity<Cleaner>()
            .HasMany(r => r.Services)
            .WithOne() //from Service perspective, each service has one Cleaner Company
            .HasForeignKey(d => d.CleanerId); //service has a foreign key to the Cleaner Company

    }
}
