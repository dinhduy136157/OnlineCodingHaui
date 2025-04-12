using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.DTOs.FunctionTemplate
{
    public class FunctionTemplateDto
    {
        public int TemplateID { get; set; }
        public int ExerciseID { get; set; }
        public string Language { get; set; }
        public string FunctionTemplateContent { get; set; }
    }
}
