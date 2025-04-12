using OnlineCodingHaui.Application.DTOs.CodingExercises;
using OnlineCodingHaui.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.Services.Interfaces
{
    public interface IFunctionTemplateService
    {
        Task<IEnumerable<FunctionTemplate>> GetAllFunctionTemplateAsync();
        Task<FunctionTemplate> GetByIdAsync(int id);
        Task AddFunctionTemplateAsync(FunctionTemplate functionTemplate);
        Task DeleteFunctionTemplateAsync(int id);
        Task UpdateFunctionTemplateAsync(FunctionTemplate functionTemplate);
    }
}
