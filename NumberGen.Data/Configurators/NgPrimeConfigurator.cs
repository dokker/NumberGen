using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NumberGen.Model;

namespace NumberGen.Data.Configurators;

public class NgPrimeConfigurator :IEntityTypeConfiguration<NgPrime>
{
    public void Configure(EntityTypeBuilder<NgPrime> builder)
    {
        builder.ToTable(nameof(NgPrime));
        builder.HasKey(x => x.Number);
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.GenerationTime).IsRequired();
    }
}