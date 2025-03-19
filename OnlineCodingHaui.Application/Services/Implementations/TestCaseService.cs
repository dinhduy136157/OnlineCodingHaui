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
    public class TestCaseService : ITestCaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TestCaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddTestCaseAsync(TestCase testCase)
        {
            await _unitOfWork.TestCaseRepository.AddAsync(testCase);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteTestCaseAsync(int id)
        {
            var testCase = await _unitOfWork.TestCaseRepository.GetByIdAsync(id);
            if (testCase != null)
            {
                await _unitOfWork.TestCaseRepository.DeleteAsync(testCase);
                await _unitOfWork.SaveChangeAsync();
            }
        }

        public async Task<IEnumerable<TestCase>> GetAllTestCaseAsync()
        {
            return await _unitOfWork.TestCaseRepository.GetAllAsync();
        }

        public async Task<TestCase> GetByIdAsync(int id)
        {
            return await _unitOfWork.TestCaseRepository.GetByIdAsync(id);
        }

        //public async Task<TestCase?> GetTestCaseByIdAsync(int id) => await _unitOfWork.TestCaseRepository.GetByIdAsync(id);


        public async Task UpdateTestCaseAsync(TestCase testCase)
        {
            await _unitOfWork.TestCaseRepository.UpdateAsync(testCase);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
