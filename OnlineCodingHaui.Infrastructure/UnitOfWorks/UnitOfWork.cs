﻿using OnlineCodingHaui.Domain.Entity;
using OnlineCodingHaui.Infrastructure.Context;
using OnlineCodingHaui.Infrastructure.Repositories.Implementations;
using OnlineCodingHaui.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly OnlineCodingHauiContext _context;

        public UnitOfWork(OnlineCodingHauiContext context)
        {
            _context = context;
            CodingExerciseRepository = new CodingExerciseRepository(_context);
            LessonRepository = new LessonRepository(_context);
            StudentRepository = new StudentRepository(_context);
            SubjectRepository = new SubjectRepository(_context);
            SubmissionRepository = new SubmissionRepository(_context);
            ClassesRepository = new ClassesRepository(_context);
            ClassStudentRepository = new ClassStudentRepository(_context);
            LessonContentRepository = new LessonContentRepository(_context);
            TestCaseRepository = new TestCaseRepository(_context);
            TeacherRepository = new TeacherRepository(_context);
            FunctionTemplateRepository = new FunctionTemplateRepository(_context);

        }
        public ICodingExerciseRepository CodingExerciseRepository { get; }
        public ILessonRepository LessonRepository { get; }
        public IStudentRepository StudentRepository { get; }
        public IClassesRepository ClassesRepository { get; }
        public IClassStudentRepository ClassStudentRepository { get; }
        public ILessonContentRepository LessonContentRepository { get; }
        public ITestCaseRepository TestCaseRepository { get; }

        public ITeacherRepository TeacherRepository { get; }

        public ISubjectRepository SubjectRepository { get; }

        public ISubmissionRepository SubmissionRepository { get; }
        public IFunctionTemplateRepository FunctionTemplateRepository { get; }


        public void Dispose()
        {
            _context.Dispose();
        }

        public override bool Equals(object? obj)
        {
            return obj is UnitOfWork work &&
                   EqualityComparer<OnlineCodingHauiContext>.Default.Equals(_context, work._context);
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
