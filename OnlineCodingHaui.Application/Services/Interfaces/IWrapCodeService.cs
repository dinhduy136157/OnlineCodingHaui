using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.Services.Interfaces
{
    public interface IWrapCodeService
    {
        public string Wrap(string studentCode, string functionName, string returnType, string parametersJson, string exampleInput, string language);
    }
}
