using Microsoft.EntityFrameworkCore;
using Wallet.DAL.Entities;
namespace Wallet.DAL.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasIndex(u => u.Name)
                .IsUnique();

            modelBuilder.Entity<Transaction>()
                .HasIndex(u => u.TransactionDate)
                .IsUnique();
            modelBuilder.Entity<Transaction>()
                    .HasOne(t => t.User)
                    .WithMany(u => u.UsersTransactions)
                    .HasForeignKey(t => t.userId);
        }
    }
}
