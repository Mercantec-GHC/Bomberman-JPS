using DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bomberman_Backend.Data
{
    internal sealed class GyroscopeConfiguration : IEntityTypeConfiguration<Gyroscope>
    {
        public void Configure(EntityTypeBuilder<Gyroscope> builder)
        {
            builder.HasKey(cl => cl.Id);
            builder.HasIndex(cl => cl.Id).IsUnique();
        }
    }
}
