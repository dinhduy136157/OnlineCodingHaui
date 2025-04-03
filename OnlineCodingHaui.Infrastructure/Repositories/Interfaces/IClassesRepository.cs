using OnlineCodingHaui.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Infrastructure.Repositories.Interfaces
{
    public interface IClassesRepository : IGenericRepository<Class>
    {
        Task<List<Class>> GetClassByTeacherId(int teacherId);

    }
}
