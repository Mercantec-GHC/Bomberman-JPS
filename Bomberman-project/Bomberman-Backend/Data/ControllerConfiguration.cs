using DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bomberman_Backend.Data
{
    internal sealed class ControllerConfiguration : IEntityTypeConfiguration<Controller>
    {
        public void Configure(EntityTypeBuilder<Controller> builder)
        {
            builder.HasKey(r => r.Id);
            builder.HasOne(r => r.Player).WithMany().HasForeignKey(r => r.playerId);
            builder.HasOne(r => r.Gyroscope).WithMany().HasForeignKey(r => r.gyroScopeId);
            builder.HasOne(r => r.Buttons).WithMany().HasForeignKey(r => r.buttonsId);
        }
    }
}
