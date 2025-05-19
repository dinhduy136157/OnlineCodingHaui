using OnlineCodingHaui.Application.DTOs.Lessons;
using OnlineCodingHaui.Application.Services.Interfaces;
using OnlineCodingHaui.Domain.Entity;
using OnlineCodingHaui.Infrastructure.Repositories.Implementations;
using OnlineCodingHaui.Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.Services.Implementations
{
    public class LessonService : ILessonService
    {
        private readonly IUnitOfWork _unitOfWork;
        public LessonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddLessonAsync(Lesson lesson)
        {
            await _unitOfWork.LessonRepository.AddAsync(lesson);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteLessonAsync(int id)
        {
            var lesson = await _unitOfWork.LessonRepository.GetByIdAsync(id);
            if (lesson != null)
            {
                await _unitOfWork.LessonRepository.DeleteAsync(lesson);
                await _unitOfWork.SaveChangeAsync();
            }
        }

        public async Task<IEnumerable<Lesson>> GetAllLessonAsync()
        {
            return await _unitOfWork.LessonRepository.GetAllAsync();
        }

        public async Task<Lesson> GetByIdAsync(int id)
        {
            return await _unitOfWork.LessonRepository.GetByIdAsync(id);
        }

        //public async Task<Lesson?> GetLessonByIdAsync(int id) => await _unitOfWork.LessonRepository.GetByIdAsync(id);


        public async Task UpdateLessonAsync(Lesson lesson)
        {
            await _unitOfWork.LessonRepository.UpdateAsync(lesson);
            await _unitOfWork.SaveChangeAsync();
        }
        public async Task<List<LessonDto>> GetLessonsByClassIdAsync(int classId)
        {
            var lessons = await _unitOfWork.LessonRepository.GetLessonsByClassIdAsync(classId);

            return lessons.Select(lesson => new LessonDto
            {
                LessonID = lesson.LessonID,
                LessonTitle = lesson.LessonTitle,
                Label = "Lý thuyết",
                Files = "TaiLieu.pdf",
                Progess = "0 / 5",
            }).ToList();
        }
        public async Task CloneLessonsFromSubjectToClassAsync(int targetClassId, string subjectId)
        {
            await _unitOfWork.LessonRepository.CopyLessonsAndContentsFromSampleClassAsync(targetClassId, subjectId);
        }

    }
}
