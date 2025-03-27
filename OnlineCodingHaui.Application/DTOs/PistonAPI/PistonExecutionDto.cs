using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.DTOs.PistonAPI
{
    public class PistonExecutionDto
    {
        public string Language { get; set; }    // "python", "javascript"
        public string Version { get; set; }     // "3.10.0", "16.13.0"
        public string Code { get; set; }        // Nội dung code
        public string Stdin { get; set; }       // Dữ liệu nhập (nếu có)

    }
}
