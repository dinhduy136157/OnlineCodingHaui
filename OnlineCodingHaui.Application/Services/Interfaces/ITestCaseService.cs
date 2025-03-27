using OnlineCodingHaui.Application.DTOs.TestCases;
using OnlineCodingHaui.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.Services.Interfaces
{
    public interface ITestCaseService
    {
        Task<IEnumerable<TestCase>> GetAllTestCaseAsync();
        Task<TestCase> GetByIdAsync(int id);
        Task AddTestCaseAsync(TestCase testCase);
        Task DeleteTestCaseAsync(int id);
        Task UpdateTestCaseAsync(TestCase testCase);
        Task<List<TestCaseDto>> GetTestCaseByExerciseId(int exerciseId);
        //Task<TestCase> GetTestCaseByIdAsync(int id);
    }
}
