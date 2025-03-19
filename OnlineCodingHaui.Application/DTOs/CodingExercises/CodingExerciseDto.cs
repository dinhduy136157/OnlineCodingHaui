using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
