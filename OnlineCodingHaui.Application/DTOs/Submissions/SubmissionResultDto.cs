using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.DTOs.Submissions
{
    public class SubmissionResultDto
    {
        public string Input { get; set; }
        public string Expected { get; set; }
        public string Output { get; set; }
        public string Status { get; set; }
    }
}
