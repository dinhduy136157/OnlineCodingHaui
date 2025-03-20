using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.DTOs.Lessons
{
    public class LessonDto
    {
        public int LessonID { get; set; }
        public int ClassID { get; set; }
        public string LessonTitle { get; set; } = null!;
        public int TeacherID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
