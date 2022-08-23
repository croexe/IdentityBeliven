using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Models;

namespace TaskManager.Infrastructure.TableConfigurations;

public class PriorityTableConfiguration : IEntityTypeConfiguration<Priority>
{
    public void Configure(EntityTypeBuilder<Priority> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.PriorityName).IsRequired()
            .HasColumnType("varchar(20)");

        builder.HasOne(p => p.Task).WithOne(t => t.Priority).HasForeignKey<Domain.Models.Task>(t => t.PriorityId);

        builder.ToTable("Priorities");
    }
}