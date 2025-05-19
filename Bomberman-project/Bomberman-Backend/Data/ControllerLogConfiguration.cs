using DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bomberman_Backend.Data;

internal sealed class ControllerLogConfiguration : IEntityTypeConfiguration<ControllerLogs>
{
    public void Configure(EntityTypeBuilder<ControllerLogs> builder)
    {
        builder.HasKey(cl => cl.Id);
        builder.Property(cl => cl.PlayerID).HasMaxLength(200);
        builder.HasIndex(cl => cl.PlayerID).IsUnique();
        builder.HasOne(cl => cl.Player).WithMany().HasForeignKey(cl => cl.PlayerID);
    }
}