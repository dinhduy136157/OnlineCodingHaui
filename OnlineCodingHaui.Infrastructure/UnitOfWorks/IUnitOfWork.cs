using OnlineCodingHaui.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Infrastructure.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        ICodingExerciseRepository CodingExerciseRepository { get; }
        ILessonRepository LessonRepository { get; }
        IStudentRepository StudentRepository { get; }
        ISubjectRepository SubjectRepository { get; }
        ISubmissionRepository SubmissionRepository { get; }
        Task<int> SaveChangeAsync();

    }
}
