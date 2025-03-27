using OnlineCodingHaui.Application.DTOs.PistonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.Services.Interfaces
{
    public interface IPistonApiService
    {
        Task<PistonResultDto> ExecuteAsync(PistonExecutionDto request);
    }
}
