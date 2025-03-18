using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Domain.Entity
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string? PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public ICollection<Class> Classes { get; set; } = new List<Class>();
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
        public ICollection<CodingExercise> CodingExercises { get; set; } = new List<CodingExercise>();


        //public int TeacherID { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Email { get; set; }
        //public string Password {  get; set; }
        //public DateTime CreatedAt { get; set; }

        //public ICollection<Lesson> Lessons { get; set; }
        //public ICollection<CodingExercise> CodingExercises { get; set; } = new List<CodingExercise>();

    }

}
