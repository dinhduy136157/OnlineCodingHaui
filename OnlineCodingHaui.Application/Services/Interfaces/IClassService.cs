using OnlineCodingHaui.Application.DTOs.Classes;
using OnlineCodingHaui.Application.DTOs.Lessons;
using OnlineCodingHaui.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.Services.Interfaces
{
    public interface IClassService
    {
        Task<IEnumerable<Class>> GetAllClassAsync();
        Task<Class> GetByIdAsync(int id);
        Task AddClassAsync(Class classes);
        Task DeleteClassAsync(int id);
        Task UpdateClassAsync(Class classes);
        //Task<Class> GetClassByIdAsync(int id);
        //Lấy ra thông tin lớp học theo giáo viên và join thằng subject
        Task<List<ClassDto>> GetClassByTeacherId(int teacherId);
    }
}
