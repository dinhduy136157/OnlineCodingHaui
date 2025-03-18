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
    public class ClassConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.ToTable("Classes");

            builder.HasKey(c => c.ClassID);

            builder.Property(c => c.ClassName)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(c => c.CreatedAt)
                   .HasDefaultValueSql("GETDATE()");

            builder.HasOne(c => c.Subject)
                   .WithMany()
                   .HasForeignKey(c => c.SubjectID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Teacher)
                   .WithMany()
                   .HasForeignKey(c => c.TeacherID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
