using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Domain.Entity
{
    public class Lesson
    {
        public int LessonID { get; set; }

        public string SubjectID { get; set; }

        public string LessonTitle { get; set; }

        public string LessonContent { get; set; }

        public DateTime CreatedAt { get; set; }

        // Quan hệ với Subject
        public Subject Subject { get; set; }

        // Quan hệ với CodingExercise
        public ICollection<CodingExercise> CodingExercises { get; set; }
    }

}
