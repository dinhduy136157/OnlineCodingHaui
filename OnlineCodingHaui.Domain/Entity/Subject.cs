using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Domain.Entity
{
    public class Subject
    {
        public string SubjectID { get; set; }
        public string SubjectName { get; set; }

        // Quan hệ với Lesson
        public ICollection<Lesson> Lessons { get; set; }
    }

}
