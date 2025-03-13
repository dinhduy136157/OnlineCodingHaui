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
    public class CodingExerciseConfiguration : IEntityTypeConfiguration<CodingExercise>
    {
        public void Configure(EntityTypeBuilder<CodingExercise> builder)
        {
            builder.HasKey(e => e.ExerciseID);
            builder.Property(e => e.Title).IsRequired().HasMaxLength(255);
            builder.Property(e => e.ProgrammingLanguage)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.HasOne(e => e.Lesson)
                   .WithMany(l => l.CodingExercises)
                   .HasForeignKey(e => e.LessonID)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }

}
