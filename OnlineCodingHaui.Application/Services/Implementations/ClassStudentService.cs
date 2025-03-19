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
    public class ClassStudentService : IClassStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClassStudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddClassStudentAsync(ClassStudent classStudent)
        {
            await _unitOfWork.ClassStudentRepository.AddAsync(classStudent);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteClassStudentAsync(int id)
        {
            var classStudent = await _unitOfWork.ClassStudentRepository.GetByIdAsync(id);
            if (classStudent != null)
            {
                await _unitOfWork.ClassStudentRepository.DeleteAsync(classStudent);
                await _unitOfWork.SaveChangeAsync();
            }
        }

        public async Task<IEnumerable<ClassStudent>> GetAllClassStudentAsync()
        {
            return await _unitOfWork.ClassStudentRepository.GetAllAsync();
        }

        public async Task<ClassStudent> GetByIdAsync(int id)
        {
            return await _unitOfWork.ClassStudentRepository.GetByIdAsync(id);
        }

        //public async Task<ClassStudent?> GetClassStudentByIdAsync(int id) => await _unitOfWork.ClassStudentRepository.GetByIdAsync(id);


        public async Task UpdateClassStudentAsync(ClassStudent classStudent)
        {
            await _unitOfWork.ClassStudentRepository.UpdateAsync(classStudent);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
