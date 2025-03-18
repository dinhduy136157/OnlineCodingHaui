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
    public class TestCaseConfiguration : IEntityTypeConfiguration<TestCase>
    {
        public void Configure(EntityTypeBuilder<TestCase> builder)
        {
            builder.ToTable("TestCases");

            builder.HasKey(tc => tc.TestCaseID);

            builder.Property(tc => tc.InputData)
                .IsRequired();

            builder.Property(tc => tc.ExpectedOutput)
                .IsRequired();

            builder.Property(tc => tc.IsHidden)
                .HasDefaultValue(true);

            // Relationships
            builder.HasOne(tc => tc.Exercise)
                .WithMany(e => e.TestCases)
                .HasForeignKey(tc => tc.ExerciseID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
