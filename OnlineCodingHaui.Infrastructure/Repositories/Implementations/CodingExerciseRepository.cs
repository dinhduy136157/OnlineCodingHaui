using Microsoft.EntityFrameworkCore;
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
    public class CodingExerciseRepository : GenericRepository<CodingExercise>, ICodingExerciseRepository
    {
        public CodingExerciseRepository(OnlineCodingHauiContext context) : base(context)
        {
        }
        //Lấy ra theo lessonId
        public async Task<List<CodingExercise>> GetCodingExerciseAsync(int lessonId)
        {
            return await _context.CodingExercises
                .Where(l => l.LessonID == lessonId)
                .ToListAsync();
        }

        //Lấy ra theo exercise (đây là chi tiết bài làm)
        public async Task<CodingExercise> GetCodingExerciseDetailAsync(int exerciseId)
        {
            return await _context.CodingExercises
                .Include(e => e.TestCases) // Load TestCases luôn
                .FirstOrDefaultAsync(e => e.ExerciseID == exerciseId);
        }


    }
}
