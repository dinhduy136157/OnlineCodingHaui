using AutoMapper;
using OnlineCodingHaui.Application.DTOs.Authentication;
using OnlineCodingHaui.Application.DTOs.Classes;
using OnlineCodingHaui.Application.DTOs.CodingExercises;
using OnlineCodingHaui.Application.DTOs.FunctionTemplate;
using OnlineCodingHaui.Application.DTOs.Lessons;
using OnlineCodingHaui.Application.DTOs.Subjects;
using OnlineCodingHaui.Application.DTOs.Submissions;
using OnlineCodingHaui.Application.DTOs.TestCases;
using OnlineCodingHaui.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Teacher, TeacherDto>().ReverseMap();
            CreateMap<Class, ClassDto>().ReverseMap();
            CreateMap<ClassStudent, ClassStudentDto>().ReverseMap();
            CreateMap<CodingExercise, CodingExerciseDto>().ReverseMap();
            CreateMap<LessonContent, LessonContentDto>().ReverseMap();
            CreateMap<Lesson, LessonDto>().ReverseMap();
            CreateMap<Subject, SubjectDto>().ReverseMap();
            CreateMap<Submission, SubmissionDto>().ReverseMap();
            CreateMap<TestCase, TestCaseDto>().ReverseMap();
            CreateMap<FunctionTemplate, FunctionTemplateDto>().ReverseMap();
        }
    }
}
