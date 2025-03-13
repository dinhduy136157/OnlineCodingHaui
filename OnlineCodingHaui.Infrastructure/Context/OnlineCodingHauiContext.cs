using Microsoft.EntityFrameworkCore;
using OnlineCodingHaui.Domain.Entity;
using OnlineCodingHaui.Infrastructure.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Infrastructure.Context
{
    public class OnlineCodingHauiContext : DbContext
    {
        public OnlineCodingHauiContext(DbContextOptions<OnlineCodingHauiContext> options) : base(options)
        {
        }

        public DbSet<CodingExercise> CodingExercises { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Submission> Submissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply tất cả configurations
            modelBuilder.ApplyConfiguration(new CodingExerciseConfiguration());
            modelBuilder.ApplyConfiguration(new LessonConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new SubjectConfiguration());
            modelBuilder.ApplyConfiguration(new SubmissionConfiguration());

        }
    }
}
