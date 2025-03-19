using OnlineCodingHaui.Application.Services.Interfaces;
using OnlineCodingHaui.Domain.Entity;
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
    }
}
