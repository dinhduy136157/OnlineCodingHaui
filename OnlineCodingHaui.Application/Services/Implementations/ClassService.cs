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
    public class ClassService : IClassService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClassService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddClassAsync(Class student)
        {
            await _unitOfWork.ClassesRepository.AddAsync(student);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteClassAsync(int id)
        {
            var student = await _unitOfWork.ClassesRepository.GetByIdAsync(id);
            if (student != null)
            {
                await _unitOfWork.ClassesRepository.DeleteAsync(student);
                await _unitOfWork.SaveChangeAsync();
            }
        }

        public async Task<IEnumerable<Class>> GetAllClassAsync()
        {
            return await _unitOfWork.ClassesRepository.GetAllAsync();
        }

        public async Task<Class> GetByIdAsync(int id)
        {
            return await _unitOfWork.ClassesRepository.GetByIdAsync(id);
        }

        public async Task<Class?> GetClassByIdAsync(int id) => await _unitOfWork.ClassesRepository.GetByIdAsync(id);


        public async Task UpdateClassAsync(Class student)
        {
            await _unitOfWork.ClassesRepository.UpdateAsync(student);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
