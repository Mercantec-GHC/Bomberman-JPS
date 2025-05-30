using DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Bomberman_Backend.Data
{
    internal sealed class ButtonsConfiguration : IEntityTypeConfiguration<Buttons>
    {
        public void Configure(EntityTypeBuilder<Buttons> builder)
        {
            builder.HasKey(b => b.Id);
            builder.HasIndex(b => b.Id).IsUnique();
        }
    }
}
