using MellonBank.Migrations;
using MellonBank.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MellonBank.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<AccountData> Accounts { get; set; }
        public DbSet<MellonRates> Rates { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AccountData>()
                .HasOne(e => e.User)
                .WithMany(u => u.Accounts)
                .HasForeignKey(e => e.AccountId)
                .IsRequired();

            modelBuilder.Entity<AccountData>()
               .Property(a => a.AccountBalance)
               .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<MellonRates>()
                .Property(a => a.AUD)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<MellonRates>()
                .Property(r => r.CHF)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<MellonRates>()
                .Property(r => r.GBP)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<MellonRates>()
                .Property(r => r.USD)
                .HasColumnType("decimal(18,2)");
        }
    }
}