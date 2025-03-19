using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.DTOs.Lessons
{
    public class LessonContentDto
    {
        public int ContentID { get; set; }
        public int LessonID { get; set; }
        public string Title { get; set; } = null!;
        public string ContentType { get; set; } = null!; // "PDF", "Video", "Quiz", "Text"
        public string? FileUrl { get; set; }
        public string Category { get; set; } = null!;
    }
}
