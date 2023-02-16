using MarketPlace.Core.Entities;
using MarketPlace.Core.Entities.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Infastructure.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Product> Products { get; set; }    
    public DbSet<Vaucer> Vaucers { get; set; }
    public DbSet<IdentityUserRole<string>> IdentityUserRoles { get; set; }
    public DbSet<UserAccount> UserAccounts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Product>()
             .HasOne(x => x.AppUser)
             .WithMany(x => x.Products)
             .HasForeignKey(x => x.OwnerUserId);

        //UserProductCard Configuration
        builder.Entity<UserProductCard>()
            .HasOne(x => x.AppUser)
            .WithMany(x => x.UserProductCards)
             .HasForeignKey(x => x.UserId);

        builder.Entity<UserProductCard>()
            .HasOne(x => x.Product)
            .WithMany(x => x.UserProductCards)
            .HasForeignKey(x=>x.ProductId);

        //Vaucer Configuration
        builder.Entity<Vaucer>()
             .HasOne(x => x.AppUser)
             .WithMany(x => x.Vaucers)
             .HasForeignKey(x => x.UserId);

        builder.Entity<Vaucer>()
             .HasOne(x => x.Product)
             .WithOne(x => x.Vaucer)
             .HasForeignKey<Vaucer>(x => x.ProductId);

        builder.Entity<Vaucer>()
            .HasIndex(x => x.VaucerName)
            .IsUnique();

        //UserAccount Configuration
        builder.Entity<UserAccount>()
            .HasOne(x => x.AppUser)
            .WithMany(x => x.UserAccounts)
            .HasForeignKey(x => x.UserId);

        builder.Entity<AppUser>()
            .HasIndex(x => x.Email)
            .IsUnique();       

        base.OnModelCreating(builder);
    }
}
