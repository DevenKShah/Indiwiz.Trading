using Indiwiz.Trading.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Indiwiz.Trading.Data.Configurations;

public class InstrumentConfiguration : IEntityTypeConfiguration<Instrument>
{
    public void Configure(EntityTypeBuilder<Instrument> builder)
    {
        builder.ToTable(nameof(Instrument));
        builder
            .Property(i => i.CurrencyName)
            .HasConversion(new EnumToStringConverter<CurrencyName>());
    }
}
