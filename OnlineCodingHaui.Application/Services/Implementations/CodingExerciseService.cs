using OnlineCodingHaui.Application.DTOs.CodingExercises;
using OnlineCodingHaui.Application.DTOs.Lessons;
using OnlineCodingHaui.Application.DTOs.TestCases;
using OnlineCodingHaui.Application.Services.Interfaces;
using OnlineCodingHaui.Domain.Entity;
using OnlineCodingHaui.Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.Services.Implementations
{
    public class CodingExerciseService : ICodingExerciseService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CodingExerciseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddCodingExerciseAsync(CodingExercise codingExercise)
        {
            await _unitOfWork.CodingExerciseRepository.AddAsync(codingExercise);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteCodingExerciseAsync(int id)
        {
            var codingExercise = await _unitOfWork.CodingExerciseRepository.GetByIdAsync(id);
            if (codingExercise != null)
            {
                await _unitOfWork.CodingExerciseRepository.DeleteAsync(codingExercise);
                await _unitOfWork.SaveChangeAsync();
            }
        }

        public async Task<IEnumerable<CodingExercise>> GetAllCodingExerciseAsync()
        {
            return await _unitOfWork.CodingExerciseRepository.GetAllAsync();
        }

        public async Task<CodingExercise> GetByIdAsync(int id)
        {
            return await _unitOfWork.CodingExerciseRepository.GetByIdAsync(id);
        }

        //public async Task<CodingExercise?> GetCodingExerciseByIdAsync(int id) => await _unitOfWork.CodingExerciseRepository.GetByIdAsync(id);


        public async Task UpdateCodingExerciseAsync(CodingExercise codingExercise)
        {
            await _unitOfWork.CodingExerciseRepository.UpdateAsync(codingExercise);
            await _unitOfWork.SaveChangeAsync();
        }
        //Lấy ra coding exercise dựa vào lesson id
        public async Task<List<CodingExerciseDto>> GetCodingExerciseAsync(int lessonId)
        {
            var codingExercises = await _unitOfWork.CodingExerciseRepository.GetCodingExerciseAsync(lessonId);

            return codingExercises.Select(data => new CodingExerciseDto
            {
                ExerciseID = data.ExerciseID,
                LessonID = data.LessonID,
                Title = data.Title,
                Description = data.Description,
                ExampleInput = data.ExampleInput,
                ExampleOutput = data.ExampleOutput,
                CreatedAt = data.CreatedAt

            }).ToList();
        }

        //Lấy ra coding exercise chi tieest dựa vào exercise id
        public async Task<CodingExerciseDto> GetExerciseDetail(int exerciseId)
        {
            var exercise = await _unitOfWork.CodingExerciseRepository.GetCodingExerciseDetailAsync(exerciseId);

            if (exercise == null) return null;

            return new CodingExerciseDto
            {
                ExerciseID = exercise.ExerciseID,
                LessonID = exercise.LessonID,
                Title = exercise.Title,
                Description = exercise.Description,
                ExampleInput = exercise.ExampleInput,
                ExampleOutput = exercise.ExampleOutput,
                CreatedAt = exercise.CreatedAt,
                TestCases = exercise.TestCases.Select(tc => new TestCaseDto
                {
                    TestCaseID = tc.TestCaseID,
                    InputData = tc.InputData,
                    ExpectedOutput = tc.ExpectedOutput,
                    IsHidden = tc.IsHidden
                }).ToList()
            };
        }


    }
}
