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
        ITeacherRepository TeacherRepository { get; }

        ISubjectRepository SubjectRepository { get; }
        ISubmissionRepository SubmissionRepository { get; }
        IClassesRepository ClassesRepository { get; }

        IClassStudentRepository ClassStudentRepository { get; }
        ILessonContentRepository LessonContentRepository { get; }
        ITestCaseRepository TestCaseRepository { get; }
        IFunctionTemplateRepository FunctionTemplateRepository { get; }

        Task<int> SaveChangeAsync();

    }
}
