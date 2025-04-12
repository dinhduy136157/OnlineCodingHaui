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
    public class FunctionTemplateRepository : GenericRepository<FunctionTemplate>, IFunctionTemplateRepository
    {
        public FunctionTemplateRepository(OnlineCodingHauiContext context) : base(context)
        {
        }
        public async Task<FunctionTemplate?> GetByExerciseAndLanguageAsync(int exerciseId, string language)
        {
            return await _context.FunctionTemplates
                .FirstOrDefaultAsync(t => t.ExerciseID == exerciseId && t.Language == language);
        }
    }
}
