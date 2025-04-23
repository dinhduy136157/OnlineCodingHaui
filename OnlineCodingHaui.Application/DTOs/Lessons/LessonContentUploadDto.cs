using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.DTOs.Lessons
{
    public class LessonContentUploadDto
    {
        public int LessonID { get; set; }
        public string Title { get; set; }
        public string ContentType { get; set; }
        public string Category { get; set; }
    }
}
