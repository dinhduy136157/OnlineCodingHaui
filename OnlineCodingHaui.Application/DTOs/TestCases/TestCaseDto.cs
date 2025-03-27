using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.DTOs.TestCases
{
    public class TestCaseDto
    {
        public int TestCaseID { get; set; }
        public int ExerciseID { get; set; }
        public string InputData { get; set; } = null!;
        public string ExpectedOutput { get; set; } = null!;
        public bool IsHidden { get; set; } = false;
    }
}
