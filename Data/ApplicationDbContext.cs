using Microsoft.EntityFrameworkCore;
using SalesService.Models.Entities;

namespace SalesService.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Constructor for dynamic connection string
        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured && !string.IsNullOrEmpty(_connectionString))
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        public DbSet<Login> Logins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Login entity
            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("Login");

                // EmployeeId is the primary key
                entity.HasKey(e => e.EmployeeId);

                // Map properties to exact column names
                entity.Property(e => e.EmployeeId)
                    .HasColumnName("EmployeeId")
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Username)
                    .HasColumnName("Login")
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasColumnName("Pwd")
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .HasColumnName("IsActive")
                    .IsRequired();
            });
        }
    }
}