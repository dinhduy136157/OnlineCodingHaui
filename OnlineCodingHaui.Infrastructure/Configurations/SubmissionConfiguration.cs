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
    public class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
    {
        public void Configure(EntityTypeBuilder<Submission> builder)
        {
            builder.ToTable("Submissions");

            builder.HasKey(s => s.SubmissionID);

            builder.Property(s => s.Code)
                .IsRequired();

            builder.Property(s => s.ProgrammingLanguage)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.Status)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(s => s.Result)
                .IsRequired();

            builder.Property(s => s.SubmittedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(s => s.Score)
                .IsRequired(false);

            builder.Property(s => s.ExecutionTime)
                .IsRequired(false);

            builder.Property(s => s.MemoryUsage)
                .IsRequired(false);

            builder.Property(s => s.TestCasesPassed)
                .IsRequired(false);

            builder.Property(s => s.TotalTestCases)
                .IsRequired(false);

            // Relationships
            builder.HasOne(s => s.Student)
                .WithMany(st => st.Submissions)
                .HasForeignKey(s => s.StudentID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Exercise)
                .WithMany(e => e.Submissions)
                .HasForeignKey(s => s.ExerciseID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
