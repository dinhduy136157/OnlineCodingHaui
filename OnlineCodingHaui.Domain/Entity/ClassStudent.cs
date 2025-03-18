using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Domain.Entity
{
    public class ClassStudent
    {
        public int ClassID { get; set; }
        public int StudentID { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public Class Class { get; set; } = null!;
        public Student Student { get; set; } = null!;
    }


}
