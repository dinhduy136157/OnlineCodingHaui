﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineCodingHaui.Infrastructure.Context;

#nullable disable

namespace OnlineCodingHaui.Infrastructure.Migrations
{
    [DbContext(typeof(OnlineCodingHauiContext))]
    partial class OnlineCodingHauiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OnlineCodingHaui.Domain.Entity.Class", b =>
                {
                    b.Property<int>("ClassID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClassID"));

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("SubjectID")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("SubjectID1")
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("TeacherID")
                        .HasColumnType("int");

                    b.Property<int?>("TeacherID1")
                        .HasColumnType("int");

                    b.HasKey("ClassID");

                    b.HasIndex("SubjectID");

                    b.HasIndex("SubjectID1");

                    b.HasIndex("TeacherID");

                    b.HasIndex("TeacherID1");

                    b.ToTable("Classes", (string)null);
                });

            modelBuilder.Entity("OnlineCodingHaui.Domain.Entity.ClassStudent", b =>
                {
                    b.Property<int>("ClassID")
                        .HasColumnType("int");

                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.Property<DateTime>("EnrollmentDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.HasKey("ClassID", "StudentID");

                    b.HasIndex("StudentID");

                    b.ToTable("ClassStudents");
                });

            modelBuilder.Entity("OnlineCodingHaui.Domain.Entity.CodingExercise", b =>
                {
                    b.Property<int>("ExerciseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExerciseID"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExampleInput")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExampleOutput")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LessonID")
                        .HasColumnType("int");

                    b.Property<int?>("TeacherID")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("ExerciseID");

                    b.HasIndex("LessonID");

                    b.HasIndex("TeacherID");

                    b.ToTable("CodingExercises", (string)null);
                });

            modelBuilder.Entity("OnlineCodingHaui.Domain.Entity.Lesson", b =>
                {
                    b.Property<int>("LessonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LessonID"));

                    b.Property<int>("ClassID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("LessonTitle")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("TeacherID")
                        .HasColumnType("int");

                    b.HasKey("LessonID");

                    b.HasIndex("ClassID");

                    b.HasIndex("TeacherID");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("OnlineCodingHaui.Domain.Entity.LessonContent", b =>
                {
                    b.Property<int>("ContentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContentID"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FileUrl")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("LessonID")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("ContentID");

                    b.HasIndex("LessonID");

                    b.ToTable("LessonContents", (string)null);
                });

            modelBuilder.Entity("OnlineCodingHaui.Domain.Entity.Student", b =>
                {
                    b.Property<int>("StudentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentID"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("StudentID");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("OnlineCodingHaui.Domain.Entity.Subject", b =>
                {
                    b.Property<string>("SubjectID")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("SubjectID");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("OnlineCodingHaui.Domain.Entity.Submission", b =>
                {
                    b.Property<int>("SubmissionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubmissionID"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ExecutionTime")
                        .HasColumnType("int");

                    b.Property<int>("ExerciseID")
                        .HasColumnType("int");

                    b.Property<double?>("MemoryUsage")
                        .HasColumnType("float");

                    b.Property<string>("ProgrammingLanguage")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Result")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Score")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.Property<DateTime>("SubmittedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<int?>("TestCasesPassed")
                        .HasColumnType("int");

                    b.Property<int?>("TotalTestCases")
                        .HasColumnType("int");

                    b.HasKey("SubmissionID");

                    b.HasIndex("ExerciseID");

                    b.HasIndex("StudentID");

                    b.ToTable("Submissions", (string)null);
                });

            modelBuilder.Entity("OnlineCodingHaui.Domain.Entity.Teacher", b =>
                {
                    b.Property<int>("TeacherID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeacherID"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("TeacherID");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("OnlineCodingHaui.Domain.Entity.TestCase", b =>
                {
                    b.Property<int>("TestCaseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TestCaseID"));

                    b.Property<int>("ExerciseID")
                        .HasColumnType("int");

                    b.Property<string>("ExpectedOutput")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InputData")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsHidden")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.HasKey("TestCaseID");

                    b.HasIndex("ExerciseID");

                    b.ToTable("TestCases", (string)null);
                });

            modelBuilder.Entity("OnlineCodingHaui.Domain.Entity.Class", b =>
                {
                    b.HasOne("OnlineCodingHaui.Domain.Entity.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("OnlineCodingHaui.Domain.Entity.Subject", null)
                        .WithMany("Classes")
                        .HasForeignKey("SubjectID1");

                    b.HasOne("OnlineCodingHaui.Domain.Entity.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("OnlineCodingHaui.Domain.Entity.Teacher", null)
                        .WithMany("Classes")
                        .HasForeignKey("TeacherID1");

                    b.Navigation("Subject");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("OnlineCodingHaui.Domain.Entity.ClassStudent", b =>
                {
                    b.HasOne("OnlineCodingHaui.Domain.Entity.Class", "Class")
                        .WithMany("ClassStudents")
                        .HasForeignKey("ClassID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineCodingHaui.Domain.Entity.Student", "Student")
                        .WithMany("ClassStudents")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("OnlineCodingHaui.Domain.Entity.CodingExercise", b =>
                {
                    b.HasOne("OnlineCodingHaui.Domain.Entity.Lesson", "Lesson")
                        .WithMany("CodingExercises")
                        .HasForeignKey("LessonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineCodingHaui.Domain.Entity.Teacher", null)
                        .WithMany("CodingExercises")
                        .HasForeignKey("TeacherID");

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("OnlineCodingHaui.Domain.Entity.Lesson", b =>
                {
                    b.HasOne("OnlineCodingHaui.Domain.Entity.Class", "Class")
                        .WithMany("Lessons")
                        .HasForeignKey("ClassID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineCodingHaui.Domain.Entity.Teacher", null)
                        .WithMany("Lessons")
                        .HasForeignKey("TeacherID");

                    b.Navigation("Class");
                });

            modelBuilder.Entity("OnlineCodingHaui.Domain.Entity.LessonContent", b =>
                {
                    b.HasOne("OnlineCodingHaui.Domain.Entity.Lesson", "Lesson")
                        .WithMany("LessonContents")
                        .HasForeignKey("LessonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("OnlineCodingHaui.Domain.Entity.Submission", b =>
                {
                    b.HasOne("OnlineCodingHaui.Domain.Entity.CodingExercise", "Exercise")
                        .WithMany("Submissions")
                        .HasForeignKey("ExerciseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineCodingHaui.Domain.Entity.Student", "Student")
                        .WithMany("Submissions")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("OnlineCodingHaui.Domain.Entity.TestCase", b =>
                {
                    b.HasOne("OnlineCodingHaui.Domain.Entity.CodingExercise", "Exercise")
                        .WithMany("TestCases")
                        .HasForeignKey("ExerciseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");
                });

            modelBuilder.Entity("OnlineCodingHaui.Domain.Entity.Class", b =>
                {
                    b.Navigation("ClassStudents");

                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("OnlineCodingHaui.Domain.Entity.CodingExercise", b =>
                {
                    b.Navigation("Submissions");

                    b.Navigation("TestCases");
                });

            modelBuilder.Entity("OnlineCodingHaui.Domain.Entity.Lesson", b =>
                {
                    b.Navigation("CodingExercises");

                    b.Navigation("LessonContents");
                });

            modelBuilder.Entity("OnlineCodingHaui.Domain.Entity.Student", b =>
                {
                    b.Navigation("ClassStudents");

                    b.Navigation("Submissions");
                });

            modelBuilder.Entity("OnlineCodingHaui.Domain.Entity.Subject", b =>
                {
                    b.Navigation("Classes");
                });

            modelBuilder.Entity("OnlineCodingHaui.Domain.Entity.Teacher", b =>
                {
                    b.Navigation("Classes");

                    b.Navigation("CodingExercises");

                    b.Navigation("Lessons");
                });
#pragma warning restore 612, 618
        }
    }
}
