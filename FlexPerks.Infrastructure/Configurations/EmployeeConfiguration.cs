using FlexPerks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlexPerks.Infrastructure.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FullName).IsRequired().HasMaxLength(150);
            builder.Property(e => e.Email).IsRequired().HasMaxLength(150);

            builder.Property(e => e.Document).HasMaxLength(20);
            builder.Property(e => e.Registration).HasMaxLength(30);

            builder.Property(e => e.HireDate).IsRequired();

            builder.HasIndex(e => new { e.CompanyId, e.Email }).IsUnique();

            builder.HasIndex(e => new { e.CompanyId, e.Registration })
                .IsUnique()
                .HasFilter("[Registration] IS NOT NULL");

            builder.HasOne(e => e.Company)
                   .WithMany(c => c.Employees)
                   .HasForeignKey(e => e.CompanyId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Manager)
                   .WithMany(m => m.DirectReports)
                   .HasForeignKey(e => e.ManagerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
