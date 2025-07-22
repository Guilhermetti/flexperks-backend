using FlexPerks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlexPerks.Infrastructure.Configurations
{
    public class BenefitCategoryConfiguration : IEntityTypeConfiguration<BenefitCategory>
    {
        public void Configure(EntityTypeBuilder<BenefitCategory> builder)
        {
            builder.ToTable("BenefitCategories");
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name).IsRequired().HasMaxLength(50);

            builder.HasMany(b => b.Wallets)
                   .WithOne(w => w.Category)
                   .HasForeignKey(w => w.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
