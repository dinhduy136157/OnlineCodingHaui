using OnlineCodingHaui.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.DTOs.Authentication
{
    public class StudentDto
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string Password { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string FullName => $"{FirstName} {LastName}";

    }
}
