using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppingSite.Models;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // アプリケーション固有のエンティティのためのDbSetプロパティを定義します
    public DbSet<BankAccounts> BankAccounts { get; set; }
    public DbSet<Carts> Carts { get; set; }
    public DbSet<Categories> Categories { get; set; }
    public DbSet<Credits> Credits { get; set; }
    public DbSet<Favorites> Favorites { get; set; }
    public DbSet<Gender> Gender { get; set; }
    public DbSet<Orders> Orders { get; set; }
    public DbSet<Packages> Packages { get; set; }
    public DbSet<Payments> Payments { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<Reviews> Reviews { get; set; }
}
