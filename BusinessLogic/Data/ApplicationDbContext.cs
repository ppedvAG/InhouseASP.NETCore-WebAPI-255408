using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Recipe> Recipes { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        Seed.SeedData(modelBuilder);

        // Relationen festlegen (Alternative zu Attributen)
        // Praktisch, wenn das Domain-Modell in einer anderen Assembly liegt
        modelBuilder.Entity<OrderItem>()
            .HasOne(o => o.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(o => o.OrderId);

        // string arrays fuer die DB als string speichern
        modelBuilder.Entity<Recipe>()
            .Property(o => o.Ingredients)
            .HasConversion(v => string.Join('\n', v), v => v.Split('\n', StringSplitOptions.RemoveEmptyEntries));
        modelBuilder.Entity<Recipe>()
            .Property(o => o.Instructions)
            .HasConversion(v => string.Join('\n', v), v => v.Split('\n', StringSplitOptions.RemoveEmptyEntries));
        modelBuilder.Entity<Recipe>()
            .Property(o => o.Tags)
            .HasConversion(v => string.Join('\n', v), v => v.Split('\n', StringSplitOptions.RemoveEmptyEntries));

    }
}
