using Microsoft.EntityFrameworkCore;
using OnlineCodingHaui.Application.DTOs.Classes;
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
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddStudentAsync(Student student)
        {
            await _unitOfWork.StudentRepository.AddAsync(student);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(id);
            if (student != null)
            {
                await _unitOfWork.StudentRepository.DeleteAsync(student);
                await _unitOfWork.SaveChangeAsync();
            }
        }

        public async Task<IEnumerable<Student>> GetAllStudentAsync()
        {
            return await _unitOfWork.StudentRepository.GetAllAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _unitOfWork.StudentRepository.GetByIdAsync(id);
        }

        //public async Task<Student?> GetStudentByIdAsync(int id) => await _unitOfWork.StudentRepository.GetByIdAsync(id);


        public async Task UpdateStudentAsync(Student student)
        {
            await _unitOfWork.StudentRepository.UpdateAsync(student);
            await _unitOfWork.SaveChangeAsync();
        }
        public async Task<Student?> AuthenticateStudentAsync(string studentCode, string password)
        {
            var student = (await _unitOfWork.StudentRepository.GetAllAsync())
                          .FirstOrDefault(s => s.StudentCode == studentCode);

            if (student == null || student.Password != password)
            {
                return null;
            }
            return student;
        }
        public async Task<Student?> GetCurrentStudentAsync(string studentIdString)
        {
            if (!int.TryParse(studentIdString, out int studentId)) return null;

            return await _unitOfWork.StudentRepository.GetByIdAsync(studentId);
        }

        //Lấy ra thông tin sinh viên đang học học phần 
        public async Task<List<ClassDto>> GetStudentClassesAsync(int studentId)
        {
            var classes = await _unitOfWork.StudentRepository.GetStudentClassesAsync(studentId);

            return classes.Select(c => new ClassDto
            {
                ClassID = c.ClassID,
                ClassName = c.ClassName,
                SubjectName = c.Subject.SubjectName, // Lấy tên môn học
            }).ToList();
        }


    }
}