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
    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SubjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddSubjectAsync(Subject subject)
        {
            await _unitOfWork.SubjectRepository.AddAsync(subject);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteSubjectAsync(string id)
        {
            var subject = await _unitOfWork.SubjectRepository.GetByIdAsync(id);
            if (subject != null)
            {
                await _unitOfWork.SubjectRepository.DeleteAsync(subject);
                await _unitOfWork.SaveChangeAsync();
            }
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectAsync()
        {
            return await _unitOfWork.SubjectRepository.GetAllAsync();
        }

        public async Task<Subject> GetByIdAsync(string id)
        {
            return await _unitOfWork.SubjectRepository.GetByIdAsync(id);
        }

        //public async Task<Subject?> GetSubjectByIdAsync(string id) => await _unitOfWork.SubjectRepository.GetByIdAsync(id);


        public async Task UpdateSubjectAsync(Subject subject)
        {
            await _unitOfWork.SubjectRepository.UpdateAsync(subject);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
