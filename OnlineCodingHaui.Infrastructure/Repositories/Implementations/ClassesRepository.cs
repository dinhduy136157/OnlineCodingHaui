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
    public class ClassesRepository : GenericRepository<Class>, IClassesRepository
    {
        public ClassesRepository(OnlineCodingHauiContext context) : base(context)
        {
        }

        public async Task<List<Class>> GetClassByTeacherId(int teacherId)
        {
            return await _context.Classes
                .Where(t => t.TeacherID == teacherId)
                .Include(c => c.Subject) // Join với bảng Class
                .ToListAsync();
        }
    }
}
