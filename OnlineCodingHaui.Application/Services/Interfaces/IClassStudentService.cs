using OnlineCodingHaui.Application.DTOs.Authentication;
using OnlineCodingHaui.Application.DTOs.Classes;
using OnlineCodingHaui.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.Services.Interfaces
{
    public interface IClassStudentService
    {
        Task<IEnumerable<ClassStudent>> GetAllClassStudentAsync();
        Task<ClassStudent> GetByIdAsync(int id);
        Task AddClassStudentAsync(ClassStudent classStudent);
        Task DeleteClassStudentAsync(int id);
        Task UpdateClassStudentAsync(ClassStudent classStudent);
        Task<List<StudentDto>> GetStudentByClass(int classId);

        //Task<ClassStudent> GetClassStudentByIdAsync(int id);
    }
}
