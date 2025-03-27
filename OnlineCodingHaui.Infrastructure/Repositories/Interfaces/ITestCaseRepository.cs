using OnlineCodingHaui.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Infrastructure.Repositories.Interfaces
{
    public interface ITestCaseRepository : IGenericRepository<TestCase>
    {
        Task<List<TestCase>> GetTestCaseByExerciseId(int classId);

    }
}
