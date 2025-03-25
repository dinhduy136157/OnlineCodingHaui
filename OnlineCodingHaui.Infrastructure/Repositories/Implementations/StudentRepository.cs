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
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(OnlineCodingHauiContext context) : base(context)
        {

        }
        public async Task<List<Class>> GetStudentClassesAsync(int studentId)
        {
            return await _context.ClassStudents
                .Where(cs => cs.StudentID == studentId)
                .Include(cs => cs.Class) // Join với bảng Class
                .ThenInclude(c => c.Subject) // Join tiếp với Subject
                .Select(cs => cs.Class) // Chỉ lấy danh sách Class
                .ToListAsync();
        }

       
    }
}
