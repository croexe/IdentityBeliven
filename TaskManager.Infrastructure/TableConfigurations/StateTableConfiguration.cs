using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Models;

namespace TaskManager.Infrastructure.TableConfigurations;

public class StateTableConfiguration : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.StateName).IsRequired()
            .HasColumnType("varchar(20)");

        builder.HasOne(s => s.Task).WithOne(t => t.State).HasForeignKey<Domain.Models.Task>(t => t.StateId);

        builder.ToTable("States");
    }
}