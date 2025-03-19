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
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TeacherService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddTeacherAsync(Teacher teacher)
        {
            await _unitOfWork.TeacherRepository.AddAsync(teacher);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteTeacherAsync(int id)
        {
            var teacher = await _unitOfWork.TeacherRepository.GetByIdAsync(id);
            if (teacher != null)
            {
                await _unitOfWork.TeacherRepository.DeleteAsync(teacher);
                await _unitOfWork.SaveChangeAsync();
            }
        }

        public async Task<IEnumerable<Teacher>> GetAllTeacherAsync()
        {
            return await _unitOfWork.TeacherRepository.GetAllAsync();
        }

        public async Task<Teacher> GetByIdAsync(int id)
        {
            return await _unitOfWork.TeacherRepository.GetByIdAsync(id);
        }

        //public async Task<Teacher?> GetTeacherByIdAsync(int id) => await _unitOfWork.TeacherRepository.GetByIdAsync(id);


        public async Task UpdateTeacherAsync(Teacher teacher)
        {
            await _unitOfWork.TeacherRepository.UpdateAsync(teacher);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
