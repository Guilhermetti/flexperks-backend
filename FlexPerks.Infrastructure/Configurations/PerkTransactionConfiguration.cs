using FlexPerks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlexPerks.Infrastructure.Configurations
{
    public class PerkTransactionConfiguration : IEntityTypeConfiguration<PerkTransaction>
    {
        public void Configure(EntityTypeBuilder<PerkTransaction> builder)
        {
            builder.ToTable("PerkTransactions");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Amount).IsRequired().HasColumnType("decimal(10,2)");
            builder.Property(t => t.Type).IsRequired().HasMaxLength(20);
            builder.Property(t => t.OccurredAt).HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(t => t.Wallet)
                   .WithMany(w => w.Transactions)
                   .HasForeignKey(t => t.WalletId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
