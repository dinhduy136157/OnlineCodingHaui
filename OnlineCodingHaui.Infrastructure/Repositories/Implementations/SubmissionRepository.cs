﻿using Microsoft.EntityFrameworkCore;
using OnlineCodingHaui.Domain.Entity;
using OnlineCodingHaui.Infrastructure.Context;
using OnlineCodingHaui.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Infrastructure.Repositories.Implementations
{
    public class SubmissionRepository : GenericRepository<Submission>, ISubmissionRepository
    {
        public SubmissionRepository(OnlineCodingHauiContext context) : base(context)
        {

        }
        public async Task<List<Submission>> GetSubmissionsByStudentIdAndClassId(int studentId, int classId)
        {
            return await _context.Submissions
            .Include(s => s.Exercise)
                .ThenInclude(e => e.Lesson)
            .Where(s => s.StudentID == studentId &&
                       s.Exercise.Lesson.ClassID == classId)
            .OrderByDescending(s => s.SubmittedAt)
            .ToListAsync();
        }
        public async Task<List<Submission>> GetSubmissionsByStudentIdAndLessonId(int studentId, int lessonId)
        {
            return await _context.Submissions
            .Include(s => s.Exercise)
                .ThenInclude(e => e.Lesson)
            .Where(s => s.StudentID == studentId &&
                       s.Exercise.Lesson.LessonID == lessonId)
            .OrderByDescending(s => s.SubmittedAt)
            .ToListAsync();
        }

        public async Task<List<Submission>> GetSubmissionsByExerciseId(int exerciseId)
        {
            var latestSubmissionIds = await _context.Submissions
            .Where(s => s.ExerciseID == exerciseId)
            .GroupBy(s => s.StudentID)
            .Select(g => g.OrderByDescending(s => s.SubmittedAt).First().SubmissionID)
            .ToListAsync();

            // Bước 2: Truy vấn lại Submissions + Include
            var latestSubmissions = await _context.Submissions
                .Where(s => latestSubmissionIds.Contains(s.SubmissionID))
                .Include(s => s.Student)
                .ToListAsync();

            return latestSubmissions;
        }
    }
}
