using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Domain.Entity
{
    public class TestCase
    {
        public int TestCaseID { get; set; }
        public int ExerciseID { get; set; }
        public string InputData { get; set; } = null!;
        public string ExpectedOutput { get; set; } = null!;
        public bool IsHidden { get; set; } = true; // 1: Test case ẩn, 0: Test case hiển thị

        // Navigation Property
        public CodingExercise Exercise { get; set; } = null!;
    }

}
