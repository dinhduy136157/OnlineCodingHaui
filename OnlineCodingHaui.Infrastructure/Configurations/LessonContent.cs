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
    public class LessonContentConfiguration : IEntityTypeConfiguration<LessonContent>
    {
        public void Configure(EntityTypeBuilder<LessonContent> builder)
        {
            builder.ToTable("LessonContents");

            builder.HasKey(lc => lc.ContentID);

            builder.Property(lc => lc.Title)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(lc => lc.ContentType)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(lc => lc.FileUrl)
                   .HasMaxLength(500);

            builder.Property(lc => lc.Category)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasOne(lc => lc.Lesson)
                   .WithMany(l => l.LessonContents)
                   .HasForeignKey(lc => lc.LessonID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
