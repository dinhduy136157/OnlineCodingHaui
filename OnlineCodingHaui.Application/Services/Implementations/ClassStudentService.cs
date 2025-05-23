﻿using OnlineCodingHaui.Application.DTOs.Authentication;
using OnlineCodingHaui.Application.DTOs.Classes;
using OnlineCodingHaui.Application.DTOs.CodingExercises;
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
    public class ClassStudentService : IClassStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClassStudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddClassStudentAsync(ClassStudent classStudent)
        {
            await _unitOfWork.ClassStudentRepository.AddAsync(classStudent);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteClassStudentAsync(int id)
        {
            var classStudent = await _unitOfWork.ClassStudentRepository.GetByIdAsync(id);
            if (classStudent != null)
            {
                await _unitOfWork.ClassStudentRepository.DeleteAsync(classStudent);
                await _unitOfWork.SaveChangeAsync();
            }
        }

        public async Task<IEnumerable<ClassStudent>> GetAllClassStudentAsync()
        {
            return await _unitOfWork.ClassStudentRepository.GetAllAsync();
        }

        public async Task<ClassStudent> GetByIdAsync(int id)
        {
            return await _unitOfWork.ClassStudentRepository.GetByIdAsync(id);
        }

        //public async Task<ClassStudent?> GetClassStudentByIdAsync(int id) => await _unitOfWork.ClassStudentRepository.GetByIdAsync(id);


        public async Task UpdateClassStudentAsync(ClassStudent classStudent)
        {
            await _unitOfWork.ClassStudentRepository.UpdateAsync(classStudent);
            await _unitOfWork.SaveChangeAsync();
        }
        public async Task<List<StudentDto>> GetStudentByClass(int classId)
        {
            var data = await _unitOfWork.ClassStudentRepository.GetStudentByClassId(classId);

            return data.Select(c => new StudentDto
            {
                StudentID = c.StudentID,
                StudentCode = c.Student.StudentCode,
                DateOfBirth = c.Student.DateOfBirth,
                FirstName = c.Student.FirstName,
                LastName = c.Student.LastName,
                Phone = c.Student.PhoneNumber,
            }).ToList();
        }
    }
}
