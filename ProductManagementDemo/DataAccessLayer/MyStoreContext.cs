using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BusinessObjects;
using System;
using System.IO;

namespace DataAccessLayer
{
    public partial class MyStoreContext : DbContext
    {
        public MyStoreContext() { }

        public MyStoreContext(DbContextOptions<MyStoreContext> options)
            : base(options) { }

        public virtual DbSet<AccountMember> AccountMember { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = GetConnectionString();
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        private string GetConnectionString()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var configFile = Path.Combine(basePath, "appsettings.json");

            if (!File.Exists(configFile))
            {
                throw new FileNotFoundException("appsettings.json not found: " + configFile);
            }

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration["ConnectionStrings:DefaultConnectionString"];

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string is missing or empty.");
            }

            return connectionString;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountMember>(entity =>
            {
                entity.HasKey(e => e.MemberId);
                entity.Property(e => e.MemberId).HasMaxLength(20);
                entity.Property(e => e.MemberPassword).HasMaxLength(80);
                entity.Property(e => e.FullName).HasMaxLength(80);
                entity.Property(e => e.EmailAddress).HasMaxLength(100);
                entity.Property(e => e.MemberRole);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId);
                entity.Property(e => e.CategoryName).HasMaxLength(15);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductID);
                entity.Property(e => e.ProductName).HasMaxLength(40);
                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.UnitsInStock);

                entity.HasOne(d => d.Category)
                      .WithMany(p => p.Products)
                      .HasForeignKey(d => d.CategoryId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
