using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.DTOs.PistonAPI
{
    public class PistonResultDto
    {
        public bool IsSuccess => Run?.Code == 0;
        public string Output => Run?.Output;
        public string Error => Run?.Stderr;

        public class RunResult
        {
            public string Output { get; set; }
            public string Stderr { get; set; }
            public int Code { get; set; }
        }

        public RunResult Run { get; set; }
    }
}