using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Domain.Entity
{
    public class FunctionTemplate
    {
        public int TemplateID { get; set; }
        public int ExerciseID { get; set; }
        public string Language { get; set; }
        public string FunctionTemplateContent { get; set; } // Nội dung template hàm

        // Định nghĩa khóa ngoại
        public CodingExercise Exercise { get; set; } = null!;
    }
}
