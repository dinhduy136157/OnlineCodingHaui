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
    public class LessonContentRepository : GenericRepository<LessonContent>, ILessonContentRepository
    {
        public LessonContentRepository(OnlineCodingHauiContext context) : base(context)
        {
        }
        public async Task<List<LessonContent>> GetLessonContentAsync(int lessonId)
        {
            return await _context.LessonContents
                .Where(l => l.LessonID == lessonId)
                .ToListAsync();
        }
    }
}
