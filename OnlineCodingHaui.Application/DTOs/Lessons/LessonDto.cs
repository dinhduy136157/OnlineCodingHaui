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
        public string LessonTitle { get; set; } = null!;
        public string Progess { get; set; }
        public string Label {  get; set; }
        public string Files {  get; set; }
    }
}
