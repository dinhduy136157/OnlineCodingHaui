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
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasKey(l => l.LessonID);

            builder.Property(l => l.LessonTitle)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(l => l.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(l => l.Class)
                .WithMany(c => c.Lessons)
                .HasForeignKey(l => l.ClassID)
                .OnDelete(DeleteBehavior.Cascade);

            // Cấu hình quan hệ One-to-Many với CodingExercises
            builder.HasMany(l => l.CodingExercises)
                .WithOne(e => e.Lesson)
                .HasForeignKey(e => e.LessonID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    //public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    //{
    //    public void Configure(EntityTypeBuilder<Lesson> builder)
    //    {
    //        builder.HasKey(l => l.LessonID);
    //        builder.Property(l => l.LessonTitle).IsRequired().HasMaxLength(255);
    //        builder.Property(l => l.LessonContent).IsRequired();
    //        builder.Property(l => l.CreatedAt).HasDefaultValueSql("GETDATE()");

    //        builder.HasOne(l => l.Teacher)
    //           .WithMany(t => t.Lessons)
    //           .HasForeignKey(l => l.TeacherID)
    //           .OnDelete(DeleteBehavior.NoAction);
    //    }
    //}

}
