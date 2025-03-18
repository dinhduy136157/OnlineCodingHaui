using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Domain.Entity
{
    public class Class
    {
        public int ClassID { get; set; }
        public string SubjectID { get; set; } = null!;
        public string ClassName { get; set; } = null!;
        public int TeacherID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public Subject Subject { get; set; } = null!;
        public Teacher Teacher { get; set; } = null!;
        public ICollection<ClassStudent> ClassStudents { get; set; } = new List<ClassStudent>();
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    }

}
