using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Domain.Entity
{
    public class CodingExercise
    {
        public int ExerciseID { get; set; }

        public int LessonID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        public string ProgrammingLanguage { get; set; }

        public string ExampleInput { get; set; }
        public string ExampleOutput { get; set; }
        public string TestCaseInput { get; set; }
        public string TestCaseOutput { get; set; }

        public DateTime CreatedAt { get; set; }

        // Quan hệ với Lesson
        public Lesson Lesson { get; set; }

        // Quan hệ với Submission
        public ICollection<Submission> Submissions { get; set; }
    }

}
