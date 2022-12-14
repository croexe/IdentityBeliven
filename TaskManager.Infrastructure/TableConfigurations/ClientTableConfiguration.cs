using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.TableConfigurations;

public class ClientTableConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).IsRequired()
            .HasColumnType("varchar(150)");

        builder.Property(c => c.Sector).IsRequired(false)
            .HasColumnType("varchar(80)");

        builder.Property(c => c.Note).IsRequired(false)
            .HasColumnType("varchar(250)");

        builder.Navigation(c => c.Projects);
        builder.HasMany(c => c.Projects)
            .WithOne(p => p.Client)
            .HasForeignKey(p => p.ClientId);

        builder.ToTable("Clients");
    }
}