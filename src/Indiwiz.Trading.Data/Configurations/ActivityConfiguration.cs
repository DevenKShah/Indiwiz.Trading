using Indiwiz.Trading.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Indiwiz.Trading.Data.Configurations;

internal class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.ToTable(nameof(Activity));
        builder
            .Property(t => t.ActivityType)
            .HasConversion(new EnumToStringConverter<ActivityType>());
    }
}
