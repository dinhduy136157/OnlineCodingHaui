using Microsoft.EntityFrameworkCore;
using OnlineCodingHaui.Application.DTOs.CodingExercises;
using OnlineCodingHaui.Application.DTOs.Submissions;
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
    public class SubmissionService : ISubmissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SubmissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddSubmissionAsync(Submission submission)
        {
            await _unitOfWork.SubmissionRepository.AddAsync(submission);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteSubmissionAsync(int id)
        {
            var submission = await _unitOfWork.SubmissionRepository.GetByIdAsync(id);
            if (submission != null)
            {
                await _unitOfWork.SubmissionRepository.DeleteAsync(submission);
                await _unitOfWork.SaveChangeAsync();
            }
        }

        public async Task<IEnumerable<Submission>> GetAllSubmissionAsync()
        {
            return await _unitOfWork.SubmissionRepository.GetAllAsync();
        }

        public async Task<Submission> GetByIdAsync(int id)
        {
            return await _unitOfWork.SubmissionRepository.GetByIdAsync(id);
        }

        //public async Task<Submission?> GetSubmissionByIdAsync(int id) => await _unitOfWork.SubmissionRepository.GetByIdAsync(id);


        public async Task UpdateSubmissionAsync(Submission submission)
        {
            await _unitOfWork.SubmissionRepository.UpdateAsync(submission);
            await _unitOfWork.SaveChangeAsync();
        }
        public async Task<List<SubmissionDto>> GetSubmissionByStudentIdAndClassID(int studentId, int classId)
        {
            var submissions = await _unitOfWork.SubmissionRepository.GetSubmissionsByStudentIdAndClassId(studentId, classId);

            if (submissions == null) return null;

            return submissions.Select(data => new SubmissionDto
            {
                SubmissionID = data.SubmissionID,
                ExerciseID = data.ExerciseID,
                Status = data.Status,
                Score = data.Score,
                SubmittedAt = data.SubmittedAt,
            }).ToList();
        }
        public async Task<List<SubmissionDto>> GetSubmissionByStudentIdAndLessonID(int studentId, int lessonId)
        {
            var submissions = await _unitOfWork.SubmissionRepository.GetSubmissionsByStudentIdAndLessonId(studentId, lessonId);

            if (submissions == null) return null;

            return submissions.Select(data => new SubmissionDto
            {
                SubmissionID = data.SubmissionID,
                ExerciseID = data.ExerciseID,
                Status = data.Status,
                Score = data.Score,
                SubmittedAt = data.SubmittedAt,
            }).ToList();
        }

        public async Task<List<SubmissionStudentDto>> GetSubmissionsByExerciseId(int exerciseId)
        {
            var submissions = await _unitOfWork.SubmissionRepository.GetSubmissionsByExerciseId(exerciseId);

            if (submissions == null) return null;

            return submissions.Select(data => new SubmissionStudentDto
            {
                SubmissionID = data.SubmissionID,
                ExerciseID = data.ExerciseID,
                Status = data.Status,
                Score = data.Score,
                SubmittedAt = data.SubmittedAt,
                ProgrammingLanguage = data.ProgrammingLanguage,
                StudentID = data.StudentID,
                StudentCode = data.Student.StudentCode,
                StudentName = data.Student.FirstName + " " + data.Student.LastName,
                

            }).ToList();

        }


    }
}
