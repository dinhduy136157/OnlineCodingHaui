using OnlineCodingHaui.Application.DTOs.TestCases;
using System;
using System.Collections.Generic;

namespace OnlineCodingHaui.Application.DTOs.CodingExercises
{
    public class CodingExerciseDto
    {
        public int ExerciseID { get; set; }
        public int LessonID { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ExampleInput { get; set; } = null!;
        public string ExampleOutput { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // 🆕 Thêm 3 thuộc tính phục vụ wrap code tự động
        public string FunctionName { get; set; } = "Solve";
        public string ReturnType { get; set; } = null!;
        public string ParametersJson { get; set; } = null!;

        public List<TestCaseDto> TestCases { get; set; } = new();
    }
}
