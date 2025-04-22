using OnlineCodingHaui.Application.DTOs.FunctionTemplate;
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
    public class FunctionTemplateService : IFunctionTemplateService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FunctionTemplateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<FunctionTemplateDto?> GetTemplateAsync(int exerciseId, string language)
        {
            var template = await _unitOfWork.FunctionTemplateRepository.GetByExerciseAndLanguageAsync(exerciseId, language);
            if (template == null) return null;

            return new FunctionTemplateDto
            {
                TemplateID = template.TemplateID,
                ExerciseID = template.ExerciseID,
                Language = template.Language,
                FunctionTemplateContent = template.FunctionTemplateContent
            };
        }

        public async Task AddFunctionTemplateAsync(FunctionTemplate student)
        {
            await _unitOfWork.FunctionTemplateRepository.AddAsync(student);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteFunctionTemplateAsync(int id)
        {
            var student = await _unitOfWork.FunctionTemplateRepository.GetByIdAsync(id);
            if (student != null)
            {
                await _unitOfWork.FunctionTemplateRepository.DeleteAsync(student);
                await _unitOfWork.SaveChangeAsync();
            }
        }

        public async Task<IEnumerable<FunctionTemplate>> GetAllFunctionTemplateAsync()
        {
            return await _unitOfWork.FunctionTemplateRepository.GetAllAsync();
        }

        public async Task<FunctionTemplate> GetByIdAsync(int id)
        {
            return await _unitOfWork.FunctionTemplateRepository.GetByIdAsync(id);
        }

        public async Task UpdateFunctionTemplateAsync(FunctionTemplate functionTemplate)
        {
            await _unitOfWork.FunctionTemplateRepository.UpdateAsync(functionTemplate);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
