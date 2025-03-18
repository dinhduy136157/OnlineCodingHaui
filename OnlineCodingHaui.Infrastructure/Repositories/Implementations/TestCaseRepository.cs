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
    }
}
