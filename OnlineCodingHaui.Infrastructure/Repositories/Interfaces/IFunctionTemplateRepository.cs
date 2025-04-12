using OnlineCodingHaui.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Infrastructure.Repositories.Interfaces
{
    public interface IFunctionTemplateRepository : IGenericRepository<FunctionTemplate>
    {
        Task<FunctionTemplate?> GetByExerciseAndLanguageAsync(int exerciseId, string language);

    }
}
