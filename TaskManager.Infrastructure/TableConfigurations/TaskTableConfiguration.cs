using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.TableConfigurations;

public class TaskTableConfiguration : IEntityTypeConfiguration<Domain.Entities.Task>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Task> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title).IsRequired()
            .HasColumnType("varchar(170)");

        builder.Property(t => t.Description).IsRequired()
            .HasColumnType("varchar(250)");

        builder.Property(t => t.PriorityId).HasDefaultValue(2);

        builder.Navigation(t => t.Project);

        builder.Navigation(t => t.State);
        builder.HasOne<State>(t => t.State);

        builder.Navigation(t => t.Priority);
        builder.HasOne<Priority>(t => t.Priority);

        builder.Property(t => t.DeveloperId).IsRequired(false);
        builder.Navigation(t => t.Developer);
        builder.HasOne(t => t.Developer)
            .WithOne().HasForeignKey<Domain.Entities.Task>()
            .OnDelete(DeleteBehavior.NoAction);

        builder.ToTable("Tasks");
    }
}