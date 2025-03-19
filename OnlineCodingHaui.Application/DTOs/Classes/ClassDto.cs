using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.DTOs.Classes
{
    public class ClassDto
    {
        public int ClassID { get; set; }
        public string SubjectID { get; set; } = null!;
        public string ClassName { get; set; } = null!;
        public int TeacherID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
