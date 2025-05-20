using OnlineCodingHaui.Application.DTOs.Lessons;
using OnlineCodingHaui.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.Services.Interfaces
{
    public interface ILessonService
    {
        Task<IEnumerable<Lesson>> GetAllLessonAsync();
        Task<Lesson> GetByIdAsync(int id);
        Task AddLessonAsync(Lesson lesson);
        Task DeleteLessonAsync(int id);
        Task UpdateLessonAsync(Lesson lesson);
        Task<List<LessonDto>> GetLessonsByClassIdAsync(int classId);
        Task CloneLessonsFromSubjectToClassAsync(int targetClassId, string subjectId);
        Task<List<LessonDto>> GetLessonsBySubjectIdAsync(string subjectId);

        //Task<Lesson> GetLessonByIdAsync(int id);
    }
}
