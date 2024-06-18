using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingSite.Models;
namespace ShoppingSite.Data;
public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Packages> Packages { get; set; }
    public DbSet<Products> Products { get; set; }
    public DbSet<Reviews> Reviews { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<Favorites> Favorites { get; set; }
    public DbSet<BankAccounts> BankAccounts { get; set; }
    public DbSet<Credits> Credits { get; set; }
    public DbSet<Categories> Categories { get; set; }
    public DbSet<Carts> Carts { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Carts>()
            .HasOne(c => c.User)               // Cart エンティティは User エンティティとの関連を持つ
            .WithMany(u => u.Carts)            // User エンティティは複数の Cart を持つ
            .HasForeignKey(c => c.UserId)     // Cart テーブルの UserId 列が外部キー
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Favorites>()
            .HasOne(c => c.User)
            .WithMany(u => u.Favorites)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
