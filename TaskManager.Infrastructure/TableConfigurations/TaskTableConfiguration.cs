using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

        builder.Property(t => t.ProjectId).IsRequired();

        builder.Property(t => t.StateId).IsRequired();

        builder.Property(t => t.PriorityId).IsRequired();
        
        builder.HasOne(t => t.Developer)
            .WithOne().HasForeignKey<Domain.Entities.Task>();

        builder.ToTable("Tasks");
    }
}