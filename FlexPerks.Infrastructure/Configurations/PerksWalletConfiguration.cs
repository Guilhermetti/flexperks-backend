using FlexPerks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlexPerks.Infrastructure.Configurations
{
    public class PerksWalletConfiguration : IEntityTypeConfiguration<PerksWallet>
    {
        public void Configure(EntityTypeBuilder<PerksWallet> builder)
        {
            builder.ToTable("PerksWallets");
            builder.HasKey(w => w.Id);
            builder.Property(w => w.Balance).IsRequired().HasColumnType("decimal(10,2)");
        }
    }
}
