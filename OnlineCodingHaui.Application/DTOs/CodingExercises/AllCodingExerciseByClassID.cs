using OnlineCodingHaui.Application.DTOs.TestCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.DTOs.CodingExercises
{
    public class AllCodingExerciseByClassID
    {
        public int ExerciseID { get; set; }
        public int LessonID { get; set; }
        public string Title { get; set; } = null!;
        public string LessonTitle { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    }
}
