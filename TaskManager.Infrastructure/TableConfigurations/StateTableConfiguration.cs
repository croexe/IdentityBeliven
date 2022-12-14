using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.TableConfigurations;

public class StateTableConfiguration : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.StateName).IsRequired()
            .HasColumnType("varchar(20)");

        builder.ToTable("States");
    }
}