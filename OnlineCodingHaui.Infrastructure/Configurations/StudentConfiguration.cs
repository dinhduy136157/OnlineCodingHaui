using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineCodingHaui.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Infrastructure.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.StudentID);
            // Cấu hình StudentCode (bắt buộc + unique)
            builder.Property(s => s.StudentCode)
                .IsRequired()
                .HasMaxLength(20); // Điều chỉnh độ dài nếu cần

            // Thêm ràng buộc UNIQUE (cách đơn giản nhất)
            builder.HasIndex(s => s.StudentCode)
                .IsUnique();
            builder.Property(s => s.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(s => s.Password)
           .IsRequired()
           .HasMaxLength(255);

            builder.Property(s => s.PhoneNumber)
                .HasMaxLength(15);

            builder.Property(s => s.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
