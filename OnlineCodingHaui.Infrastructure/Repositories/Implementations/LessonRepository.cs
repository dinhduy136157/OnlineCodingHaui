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
    public class LessonRepository : GenericRepository<Lesson>, ILessonRepository
    {
        public LessonRepository(OnlineCodingHauiContext context) : base(context)
        {

        }
        public async Task<List<Lesson>> GetLessonsByClassIdAsync(int classId)
        {
            return await _context.Lessons
                .Where(l => l.ClassID == classId)
                .ToListAsync();
        }
    }
}
