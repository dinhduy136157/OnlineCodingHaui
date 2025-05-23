﻿using OnlineCodingHaui.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Infrastructure.Repositories.Interfaces
{
    public interface ICodingExerciseRepository : IGenericRepository<CodingExercise>
    {
        Task<List<CodingExercise>> GetCodingExerciseAsync(int lessonId);
        Task<CodingExercise> GetCodingExerciseDetailAsync(int exerciseId);
        Task<List<CodingExercise>> GetAllCodingExerciseByClassID(int classId);

    }
}
