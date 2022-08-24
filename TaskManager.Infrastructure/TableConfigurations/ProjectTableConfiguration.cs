using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Models;

namespace TaskManager.Infrastructure.TableConfigurations;

public class ProjectTableConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).IsRequired()
            .HasColumnType("varchar(150)");

        builder.Property(p => p.ClientId).IsRequired();

        builder.HasMany(p => p.Tasks)
            .WithOne(t => t.Project)
            .HasForeignKey(t => t.ProjectId);
        
        builder.HasOne(p => p.ProjectManager)
            .WithOne().HasForeignKey<Project>();

        builder.ToTable("Projects");
    }
}