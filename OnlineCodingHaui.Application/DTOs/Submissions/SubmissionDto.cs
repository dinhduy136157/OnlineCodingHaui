using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.DTOs.Submissions
{
    public class SubmissionDto
    {
        public int SubmissionID { get; set; }
        public int StudentID { get; set; }
        public int ExerciseID { get; set; }
        public string Code { get; set; } = null!;
        public string ProgrammingLanguage { get; set; } = null!; // 🔥 Đã chuyển vào Submission
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = null!; // "Accepted", "Wrong Answer", "Pending"...
        public string Result { get; set; } = null!;
        public int? Score { get; set; }
        public int? ExecutionTime { get; set; } // (ms)
        public double? MemoryUsage { get; set; }
        public int? TestCasesPassed { get; set; }
        public int? TotalTestCases { get; set; }
    }
}
