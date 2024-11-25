using Microsoft.EntityFrameworkCore;
using NumberGen.Data.Configurators;
using NumberGen.Models;

namespace NumberGen.Data;

public class NumberGenDbContext : DbContext
{
    public DbSet<NgPrime> NgPrimes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost\\mssql,1433;Database=numbergen;User Id=sa;Password=administrator1234567!;Encrypt=Yes;TrustServerCertificate=Yes"
            );
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("NumberGen");

        modelBuilder.ApplyConfiguration(new NgPrimeConfigurator());
        
        base.OnModelCreating(modelBuilder);
    }
}