using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineCodingHaui.Domain.Entity;

namespace OnlineCodingHaui.Infrastructure.Configurations
{
    public class FunctionTemplateConfiguration : IEntityTypeConfiguration<FunctionTemplate>
    {
        public void Configure(EntityTypeBuilder<FunctionTemplate> builder)
        {
            builder.HasKey(ft => ft.TemplateID);

            builder.Property(ft => ft.Language)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(ft => ft.FunctionTemplateContent)
                   .IsRequired();

            builder.HasOne(ft => ft.Exercise)
                   .WithMany()
                   .HasForeignKey(ft => ft.ExerciseID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
