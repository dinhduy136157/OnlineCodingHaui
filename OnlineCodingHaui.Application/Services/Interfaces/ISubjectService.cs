using OnlineCodingHaui.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.Services.Interfaces
{
    public interface ISubjectService
    {
        Task<IEnumerable<Subject>> GetAllSubjectAsync();
        Task<Subject> GetByIdAsync(int id);
        Task AddSubjectAsync(Subject subject);
        Task DeleteSubjectAsync(int id);
        Task UpdateSubjectAsync(Subject subject);
        Task<Subject> GetSubjectByIdAsync(int id);
    }
}
