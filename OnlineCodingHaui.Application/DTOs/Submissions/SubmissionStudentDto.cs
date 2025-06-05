using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.DTOs.Submissions
{
    public class SubmissionStudentDto
    {
        public int SubmissionID { get; set; }
        public int StudentID { get; set; }
        public int ExerciseID { get; set; }
        public string ProgrammingLanguage { get; set; } = null!; 
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = null!;
        public string Result { get; set; } = "Chưa có kq";
        public int? Score { get; set; }
        public double? MemoryUsage { get; set; }
        public int? TestCasesPassed { get; set; }
        public int? TotalTestCases { get; set; }
        public string StudentCode {  get; set; }
        public string StudentName { get; set; }
    }
}
