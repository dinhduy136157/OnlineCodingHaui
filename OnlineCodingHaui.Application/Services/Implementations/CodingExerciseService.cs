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
    public class CodingExerciseService : ICodingExerciseService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CodingExerciseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddCodingExerciseAsync(CodingExercise codingExercise)
        {
            await _unitOfWork.CodingExerciseRepository.AddAsync(codingExercise);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteCodingExerciseAsync(int id)
        {
            var codingExercise = await _unitOfWork.CodingExerciseRepository.GetByIdAsync(id);
            if (codingExercise != null)
            {
                await _unitOfWork.CodingExerciseRepository.DeleteAsync(codingExercise);
                await _unitOfWork.SaveChangeAsync();
            }
        }

        public async Task<IEnumerable<CodingExercise>> GetAllCodingExerciseAsync()
        {
            return await _unitOfWork.CodingExerciseRepository.GetAllAsync();
        }

        public async Task<CodingExercise> GetByIdAsync(int id)
        {
            return await _unitOfWork.CodingExerciseRepository.GetByIdAsync(id);
        }

        //public async Task<CodingExercise?> GetCodingExerciseByIdAsync(int id) => await _unitOfWork.CodingExerciseRepository.GetByIdAsync(id);


        public async Task UpdateCodingExerciseAsync(CodingExercise codingExercise)
        {
            await _unitOfWork.CodingExerciseRepository.UpdateAsync(codingExercise);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
