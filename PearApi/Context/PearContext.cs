using Microsoft.EntityFrameworkCore;
using PearApi.Authentication;
using PearApi.Models;


public class PearContext : DbContext
{
   public PearContext(DbContextOptions<PearContext> options)
      : base(options)
   {
   }

   public DbSet<Product> Products { get; set; } = null!;
   public DbSet<OptionGroup> OptionGroups { get; set; } = null!;
   public DbSet<OptionRelation> OptionRelations { get; set; } = null!;
   public DbSet<Option> Options { get; set; } = null!;
   public DbSet<OrderItem> OrderItems { get; set; } = null!;
   public DbSet<UserModel> Users { get; set; } = null!;
   public DbSet<LineItem> LineItems { get; set; } = default!;
   public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder.Entity<OptionRelation>()
          .HasKey(o => new { o.OptionGroupId, o.OptionId });
      modelBuilder.Entity<OptionRelation>()
          .HasOne(o => o.Option)
          .WithMany(o => o.OptionRelations)
          .HasForeignKey(o => o.OptionId);
      modelBuilder.Entity<OptionRelation>()
          .HasOne(o => o.OptionGroup)
          .WithMany(o => o.OptionRelations)
          .HasForeignKey(o => o.OptionGroupId);
   }
}