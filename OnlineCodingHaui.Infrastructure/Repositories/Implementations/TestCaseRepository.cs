using Microsoft.EntityFrameworkCore;
using OnlineCodingHaui.Domain.Entity;
using OnlineCodingHaui.Infrastructure.Context;
using OnlineCodingHaui.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Infrastructure.Repositories.Implementations
{
    public class TestCaseRepository : GenericRepository<TestCase>, ITestCaseRepository
    {
        public TestCaseRepository(OnlineCodingHauiContext context) : base(context)
        {
        }
        public async Task<List<TestCase>> GetTestCaseByExerciseId(int exerciseId)
        {
            return await _context.TestCases
                .Where(l => l.ExerciseID == exerciseId)
                .ToListAsync();
        }
    }
}
