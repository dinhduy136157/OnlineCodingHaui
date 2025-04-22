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
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ExampleInput { get; set; } = null!;
        public string ExampleOutput { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string FunctionName { get; set; } = "Solve";
        public string ReturnType { get; set; } = null!;
        public string ParametersJson { get; set; } = null!;
        public Lesson Lesson { get; set; } = null!;
        public ICollection<TestCase> TestCases { get; set; } = new List<TestCase>();
        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
        public ICollection<FunctionTemplate> FunctionTemplates { get; set; } = new List<FunctionTemplate>();
    }

}
