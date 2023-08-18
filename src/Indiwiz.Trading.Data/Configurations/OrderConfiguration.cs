using Indiwiz.Trading.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Indiwiz.Trading.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
            .Property(o => o.TransactionType)
            .HasConversion(new EnumToStringConverter<TransactionType>());
        builder
            .Property(o => o.OrderType)
            .HasConversion(new EnumToStringConverter<OrderType>());
    }
}
