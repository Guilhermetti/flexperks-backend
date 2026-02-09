using FlexPerks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlexPerks.Infrastructure.Configurations
{
    public class TimeClockEntryConfiguration : IEntityTypeConfiguration<TimeClockEntry>
    {
        public void Configure(EntityTypeBuilder<TimeClockEntry> builder)
        {
            builder.ToTable("TimeClockEntries");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.TimestampUtc).IsRequired();
            builder.Property(e => e.Type).IsRequired();
            builder.Property(e => e.Source).IsRequired();
            builder.Property(e => e.Note).HasMaxLength(300);

            builder.HasIndex(e => new { e.CompanyId, e.EmployeeId, e.TimestampUtc, e.Type }).IsUnique();

            builder.HasOne(e => e.Company)
                   .WithMany()
                   .HasForeignKey(e => e.CompanyId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Employee)
                   .WithMany()
                   .HasForeignKey(e => e.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
