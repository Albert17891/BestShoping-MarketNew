using MarketPlace.Core.Entities;
using MarketPlace.Core.Entities.Admin;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Infastructure.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Product> Products { get; set; }
    public DbSet<UserProduct> UserProducts { get; set; }
    public DbSet<Vaucer> Vaucers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<UserProduct>()
            .HasKey(x => new { x.UserId, x.ProductId });

        builder.Entity<UserProduct>()
            .HasOne<Product>(x => x.Product)
            .WithMany(x => x.UsersProducts)
            .HasForeignKey(x => x.ProductId);

        builder.Entity<UserProduct>()
            .HasOne<AppUser>(x => x.AppUser)
            .WithMany(x => x.UserProducts)
            .HasForeignKey(x => x.UserId);

        builder.Entity<UserProductCard>()
            .HasOne(x => x.AppUser)
            .WithMany(x => x.UserProductCards)
             .HasForeignKey(x => x.UserId);

        builder.Entity<UserProductCard>()
            .HasOne(x => x.Product)
            .WithMany(x => x.UserProductCards)
            .HasForeignKey(x=>x.ProductId);


        builder.Entity<Vaucer>()
             .HasOne(x => x.AppUser)
             .WithMany(x => x.Vaucers)
             .HasForeignKey(x => x.UserId);

        builder.Entity<Vaucer>()
             .HasOne(x => x.Product)
             .WithMany(x => x.Vaucers)
             .HasForeignKey(x => x.ProductId);

        builder.Entity<Vaucer>()
            .HasIndex(x => x.VaucerName)
            .IsUnique();

        base.OnModelCreating(builder);
    }
}
