using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineCodingHaui.Domain.Entity;

namespace OnlineCodingHaui.Infrastructure.Configurations
{
    public class CodingExerciseConfiguration : IEntityTypeConfiguration<CodingExercise>
    {
        public void Configure(EntityTypeBuilder<CodingExercise> builder)
        {
            builder.ToTable("CodingExercises");

            builder.HasKey(e => e.ExerciseID);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Description)
                .IsRequired();

            builder.Property(e => e.ExampleInput)
                .IsRequired();

            builder.Property(e => e.ExampleOutput)
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            // 🆕 Wrap metadata
            builder.Property(e => e.FunctionName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.ReturnType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.ParametersJson)
                .IsRequired();

            // Relationships
            builder.HasOne(e => e.Lesson)
                .WithMany(l => l.CodingExercises)
                .HasForeignKey(e => e.LessonID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
