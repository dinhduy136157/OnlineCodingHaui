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
    public class ClassStudentConfiguration : IEntityTypeConfiguration<ClassStudent>
    {
        public void Configure(EntityTypeBuilder<ClassStudent> builder)
        {
            builder.HasKey(cs => new { cs.ClassID, cs.StudentID }); // Định nghĩa khóa chính

            builder.Property(cs => cs.EnrollmentDate)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(cs => cs.Class)
                .WithMany(c => c.ClassStudents)
                .HasForeignKey(cs => cs.ClassID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cs => cs.Student)
                .WithMany(s => s.ClassStudents)
                .HasForeignKey(cs => cs.StudentID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }


}
