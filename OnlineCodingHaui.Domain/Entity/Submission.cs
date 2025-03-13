using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Domain.Entity
{
    public class Submission
    {
        public int SubmissionID { get; set; }

        public int StudentID { get; set; }
        public int ExerciseID { get; set; }
        public string Code { get; set; }
        public DateTime SubmittedAt { get; set; }

        public string Status { get; set; }

        public string Result { get; set; }
        public int? Score { get; set; }
        public int? ExecutionTime { get; set; }
        public float? MemoryUsage { get; set; }
        public int? TestCasesPassed { get; set; }
        public int? TotalTestCases { get; set; }

        public Student Student { get; set; }
        public CodingExercise CodingExercise { get; set; }
    }

}
