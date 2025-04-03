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
    public class ClassStudentRepository : GenericRepository<ClassStudent>, IClassStudentRepository
    {
        public ClassStudentRepository(OnlineCodingHauiContext context) : base(context)
        {
        }
        public async Task<List<ClassStudent>> GetStudentByClassId(int classId)
        {
            return await _context.ClassStudents
                .Where(c => c.ClassID == classId)
                .Include(s => s.Student)
                .ToListAsync();
        }
    }
}
