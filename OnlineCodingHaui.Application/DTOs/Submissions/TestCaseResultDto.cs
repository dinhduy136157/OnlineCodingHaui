using System;

namespace OnlineCodingHaui.Application.DTOs.Submissions
{
    public class TestCaseResultDto
    {
        public string Input { get; set; }
        public string Expected { get; set; }
        public string Output { get; set; }
        public int? ExecutionTime { get; set; }
        public int? CpuTime { get; set; }
        public string Status { get; set; }
    }
} 