using OnlineCodingHaui.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllStudentAsync();
        Task<Student> GetByIdAsync(int id);
        Task AddStudentAsync(Student student);
        Task DeleteStudentAsync(int id);
        Task UpdateStudentAsync(Student student);
        Task<Student?> AuthenticateStudentAsync(int studentId, string password);
        Task<Student?> GetCurrentStudentAsync(string studentIdString);
        //Task<Student> GetStudentByIdAsync(int id);
    }
}
