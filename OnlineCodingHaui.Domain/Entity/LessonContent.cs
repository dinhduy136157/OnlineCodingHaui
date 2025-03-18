using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Domain.Entity
{
    public class LessonContent
    {
        public int ContentID { get; set; }
        public int LessonID { get; set; }
        public string Title { get; set; } = null!;
        public string ContentType { get; set; } = null!; // "PDF", "Video", "Quiz", "Text"
        public string? FileUrl { get; set; }
        public string Category { get; set; } = null!; // "Pre-Class", "In-Class", "Post-Class"

        // Navigation Properties
        public Lesson Lesson { get; set; } = null!;
    }

}
