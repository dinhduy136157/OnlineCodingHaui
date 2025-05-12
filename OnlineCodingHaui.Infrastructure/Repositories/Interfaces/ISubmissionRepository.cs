using OnlineCodingHaui.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Infrastructure.Repositories.Interfaces
{
    public interface ISubmissionRepository : IGenericRepository<Submission>
    {
        Task<List<Submission>> GetSubmissionsByStudentIdAndClassId(int studentId, int classId);
        Task<List<Submission>> GetSubmissionsByStudentIdAndLessonId(int studentId, int lessonId);

    }
}
