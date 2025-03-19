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
        Task<Subject> GetByIdAsync(string id);
        Task AddSubjectAsync(Subject subject);
        Task DeleteSubjectAsync(string id);
        Task UpdateSubjectAsync(Subject subject);
        Task<Subject> GetSubjectByIdAsync(string id);
    }
}
