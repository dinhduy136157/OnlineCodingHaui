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
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasKey(t => t.TeacherID);

            builder.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(t => t.Password)
             .IsRequired()
             .HasMaxLength(255);
            builder.Property(t => t.PhoneNumber)
                .HasMaxLength(15);

            builder.Property(t => t.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }


}
