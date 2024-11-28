using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TeddyCourseYT.Models;

namespace TeddyCourseYT.Data;

public class ApplicationDBContext : IdentityDbContext<AppUser>
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Basket> Baskets { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Basket>(x => x.HasKey(p => new { p.AppUserId, p.ProductId }));
        builder.Entity<Basket>()
            .HasOne(u => u.AppUser)
            .WithMany(u => u.Baskets)
            .HasForeignKey(p => p.AppUserId);

        builder.Entity<Basket>()
            .HasOne(u => u.Product)
            .WithMany(u => u.Baskets)
            .HasForeignKey(p => p.ProductId);

        

        List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole {Name = "Admin", NormalizedName = "ADMIN"},
            new IdentityRole {Name = "User", NormalizedName = "USER"}
        };
        builder.Entity<IdentityRole>().HasData(roles);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.ConfigureWarnings(warnings =>
            warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

}
