﻿using OnlineCodingHaui.Application.DTOs.CodingExercises;
using OnlineCodingHaui.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.Services.Interfaces
{
    public interface ICodingExerciseService
    {
        Task<IEnumerable<CodingExercise>> GetAllCodingExerciseAsync();
        Task<CodingExercise> GetByIdAsync(int id);
        Task AddCodingExerciseAsync(CodingExercise codingExercise);
        Task DeleteCodingExerciseAsync(int id);
        Task UpdateCodingExerciseAsync(CodingExercise codingExercise);
        Task<List<CodingExerciseDto>> GetCodingExerciseAsync(int lessonId);
        Task<CodingExerciseDto> GetExerciseDetail(int exerciseId);
        Task<List<AllCodingExerciseByClassID>> GetAllCodingExerciseByClassID(int classId);

        //Task<CodingExercise> GetCodingExerciseByIdAsync(int id);
    }
}
