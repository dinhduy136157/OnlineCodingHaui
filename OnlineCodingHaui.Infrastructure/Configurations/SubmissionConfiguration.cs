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
            builder.HasKey(s => s.SubmissionID);
            builder.Property(s => s.Code).IsRequired();
            builder.Property(s => s.Status).HasMaxLength(20);
            builder.Property(s => s.SubmittedAt).HasDefaultValueSql("GETDATE()");

            builder.HasOne(s => s.Student)
                   .WithMany(st => st.Submissions)
                   .HasForeignKey(s => s.StudentID)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(s => s.CodingExercise)
                   .WithMany(e => e.Submissions)
                   .HasForeignKey(s => s.ExerciseID)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }

}
