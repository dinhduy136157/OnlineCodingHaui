using Microsoft.EntityFrameworkCore;
using OnlineCodingHaui.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Infrastructure.Repositories.Interfaces
{
    public interface ILessonContentRepository : IGenericRepository<LessonContent>
    {
        Task<List<LessonContent>> GetLessonContentAsync(int classId);
    }
}
