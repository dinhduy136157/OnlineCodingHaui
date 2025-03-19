using OnlineCodingHaui.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.Services.Interfaces
{
    public interface ISubmissionService
    {
        Task<IEnumerable<Submission>> GetAllSubmissionAsync();
        Task<Submission> GetByIdAsync(int id);
        Task AddSubmissionAsync(Submission submission);
        Task DeleteSubmissionAsync(int id);
        Task UpdateSubmissionAsync(Submission submission);
        Task<Submission> GetSubmissionByIdAsync(int id);
    }
}
