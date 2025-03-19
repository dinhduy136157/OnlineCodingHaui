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
    public class SubmissionService : ISubmissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SubmissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddSubmissionAsync(Submission submission)
        {
            await _unitOfWork.SubmissionRepository.AddAsync(submission);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteSubmissionAsync(int id)
        {
            var submission = await _unitOfWork.SubmissionRepository.GetByIdAsync(id);
            if (submission != null)
            {
                await _unitOfWork.SubmissionRepository.DeleteAsync(submission);
                await _unitOfWork.SaveChangeAsync();
            }
        }

        public async Task<IEnumerable<Submission>> GetAllSubmissionAsync()
        {
            return await _unitOfWork.SubmissionRepository.GetAllAsync();
        }

        public async Task<Submission> GetByIdAsync(int id)
        {
            return await _unitOfWork.SubmissionRepository.GetByIdAsync(id);
        }

        //public async Task<Submission?> GetSubmissionByIdAsync(int id) => await _unitOfWork.SubmissionRepository.GetByIdAsync(id);


        public async Task UpdateSubmissionAsync(Submission submission)
        {
            await _unitOfWork.SubmissionRepository.UpdateAsync(submission);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
