using OnlineCodingHaui.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<Teacher>> GetAllTeacherAsync();
        Task<Teacher> GetByIdAsync(int id);
        Task AddTeacherAsync(Teacher teacher);
        Task DeleteTeacherAsync(int id);
        Task UpdateTeacherAsync(Teacher teacher);
        //Task<Teacher> GetTeacherByIdAsync(int id);
    }
}
