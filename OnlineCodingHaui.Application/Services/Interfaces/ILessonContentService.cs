using OnlineCodingHaui.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.Services.Interfaces
{
    public interface ILessonContentService
    {
        Task<IEnumerable<LessonContent>> GetAllLessonContentAsync();
        Task<LessonContent> GetByIdAsync(int id);
        Task AddLessonContentAsync(LessonContent lessonContent);
        Task DeleteLessonContentAsync(int id);
        Task UpdateLessonContentAsync(LessonContent lessonContent);
        //Task<LessonContent> GetLessonContentByIdAsync(int id);
    }
}
