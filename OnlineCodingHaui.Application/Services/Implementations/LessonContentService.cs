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
    public class LessonContentService : ILessonContentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public LessonContentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddLessonContentAsync(LessonContent lessonContent)
        {
            await _unitOfWork.LessonContentRepository.AddAsync(lessonContent);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteLessonContentAsync(int id)
        {
            var lessonContent = await _unitOfWork.LessonContentRepository.GetByIdAsync(id);
            if (lessonContent != null)
            {
                await _unitOfWork.LessonContentRepository.DeleteAsync(lessonContent);
                await _unitOfWork.SaveChangeAsync();
            }
        }

        public async Task<IEnumerable<LessonContent>> GetAllLessonContentAsync()
        {
            return await _unitOfWork.LessonContentRepository.GetAllAsync();
        }

        public async Task<LessonContent> GetByIdAsync(int id)
        {
            return await _unitOfWork.LessonContentRepository.GetByIdAsync(id);
        }

        public async Task<LessonContent?> GetLessonContentByIdAsync(int id) => await _unitOfWork.LessonContentRepository.GetByIdAsync(id);


        public async Task UpdateLessonContentAsync(LessonContent lessonContent)
        {
            await _unitOfWork.LessonContentRepository.UpdateAsync(lessonContent);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
