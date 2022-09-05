using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.TableConfigurations;

public class PriorityTableConfiguration : IEntityTypeConfiguration<Priority>
{
    public void Configure(EntityTypeBuilder<Priority> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.PriorityName).IsRequired()
            .HasColumnType("varchar(20)");

        builder.ToTable("Priorities");
    }
}