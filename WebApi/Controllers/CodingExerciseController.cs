using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCodingHaui.Application.DTOs.Authentication;
using OnlineCodingHaui.Application.DTOs.CodingExercises;
using OnlineCodingHaui.Application.DTOs.Submissions;
using OnlineCodingHaui.Application.DTOs.TestCases;
using OnlineCodingHaui.Application.Services.Implementations;
using OnlineCodingHaui.Application.Services.Interfaces;
using OnlineCodingHaui.Domain.Entity;
using System.Reflection.Metadata;
using System.Text.Json;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodingExerciseController : ControllerBase
    {
        private readonly ICodingExerciseService _codingExerciseService;
        private readonly IFunctionTemplateService _functionTemplateService;

        private readonly ITestCaseService _testCaseService;
        private readonly ISubmissionService _submissionService;
        private readonly IMapper _mapper;

        public CodingExerciseController(ICodingExerciseService codingExerciseService, 
            ITestCaseService testCaseService, ISubmissionService submissionService, IMapper mapper,
            IFunctionTemplateService functionTemplateService)
        {
            _codingExerciseService = codingExerciseService;
            _testCaseService = testCaseService;
            _submissionService = submissionService;
            _mapper = mapper;
            _functionTemplateService = functionTemplateService;
        }
        public class Parameter
        {
            public string Name { get; set; }
            public string Type { get; set; }
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var codingExercise = await _codingExerciseService.GetAllCodingExerciseAsync();
            var codingExerciseDto = _mapper.Map<IEnumerable<CodingExerciseDto>>(codingExercise);
            return Ok(codingExerciseDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCodingExerciseByIdAsync(int id)
        {
            var codingExercise = await _codingExerciseService.GetByIdAsync(id);
            var codingExerciseDto = _mapper.Map<CodingExerciseDto>(codingExercise);
            return Ok(codingExerciseDto);
        }
        [HttpPost]
        public async Task<ActionResult> CreateCodingExercise(CodingExerciseDto codingExerciseDto)
        {
            var codingExercise = _mapper.Map<CodingExercise>(codingExerciseDto);
            await _codingExerciseService.AddCodingExerciseAsync(codingExercise);
            var templates = new List<FunctionTemplate>
            {
                new FunctionTemplate {
                    ExerciseID = codingExercise.ExerciseID,
                    Language = "java",
                    FunctionTemplateContent = GenerateJavaTemplate(codingExercise)
                },
                new FunctionTemplate {
                    ExerciseID = codingExercise.ExerciseID,
                    Language = "python",
                    FunctionTemplateContent = GeneratePythonTemplate(codingExercise)
                },
                new FunctionTemplate {
                    ExerciseID = codingExercise.ExerciseID,
                    Language = "php",
                    FunctionTemplateContent = GeneratePHPTemplate(codingExercise)
                }
            };

            foreach (var template in templates)
            {
                await _functionTemplateService.AddFunctionTemplateAsync(template);
            }
            return Ok(codingExerciseDto);
        }

        private string GenerateJavaTemplate(CodingExercise exercise)
        {
            // Deserialize JSON thành danh sách các tham số
            var parameters = JsonSerializer.Deserialize<List<Parameter>>(exercise.ParametersJson ?? "[]") ?? new List<Parameter>();

            // Tạo chuỗi tham số cho hàm (với kiểu và tên)
            var paramString = parameters.Any()
                ? string.Join(", ", parameters.Select(p => $"{ConvertType(p.Type ?? "int", "java")} {p.Name ?? "param"}"))
                : "";

            // Kiểu trả về và tên hàm
            var returnType = ConvertType(exercise.ReturnType ?? "void", "java");
            var functionName = exercise.FunctionName ?? "function";

            // Trả về template với các tham số được thêm vào hàm
            return $"public static {returnType} {functionName}({paramString}) {{\n" +
                   "    // Write your code here\n" +
                   (returnType != "void" ? $"    return {paramString};\n" : "") + // Nếu không phải void, trả về arr
                   "}";
        }

        private string GeneratePythonTemplate(CodingExercise exercise)
        {
            // Deserialize JSON thành danh sách các tham số
            var parameters = JsonSerializer.Deserialize<List<Parameter>>(exercise.ParametersJson ?? "[]") ?? new List<Parameter>();

            // Tạo chuỗi tham số cho hàm (với tên tham số)
            var paramString = string.Join(", ", parameters.Select(p => p.Name ?? "param"));

            // Tên hàm
            var functionName = exercise.FunctionName ?? "function";

            // Trả về function template với các tham số
            return $"def {functionName}({paramString}):\n" +
                   "    # Write your code here\n" +
                   $"    return {paramString}";
        }

        private string GeneratePHPTemplate(CodingExercise exercise)
        {
            // Deserialize JSON thành danh sách các tham số
            var parameters = JsonSerializer.Deserialize<List<Parameter>>(exercise.ParametersJson ?? "[]") ?? new List<Parameter>();

            // Tạo chuỗi tham số cho hàm (với kiểu và tên)
            var paramString = parameters.Any()
                ? string.Join(", ", parameters.Select(p => $"${p.Name ?? "param"}"))
                : "";

            // Tên hàm
            var functionName = exercise.FunctionName ?? "function";

            // Trả về template với các tham số được thêm vào hàm
            return $"function {functionName}({paramString}) {{\n" +
                   "    // Write your code here\n" +
                   $"    return ${paramString};\n" +
                   "}";
        }

        private string ConvertType(string type, string language)
        {
            if (language == "python")
            {
                return type switch
                {
                    "int[]" => "list",
                    "string[]" => "list",
                    _ => type
                };
            }
            return type; // Giữ nguyên kiểu cho Java
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCodingExercise(int id, CodingExerciseDto codingExerciseDto)
        {
            if (id != codingExerciseDto.ExerciseID)
                return BadRequest();

            var codingExercise = _mapper.Map<CodingExercise>(codingExerciseDto);
            await _codingExerciseService.UpdateCodingExerciseAsync(codingExercise);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCodingExercise(int id)
        {
            await _codingExerciseService.DeleteCodingExerciseAsync(id);
            return Ok();
        }

        //Lấy ra chi CodingExercise

        [HttpGet("coding-exercise")]
        public async Task<IActionResult> GetCodingExercises(int lessonId)
        {
            var lessons = await _codingExerciseService.GetCodingExerciseAsync(lessonId);
            return Ok(lessons);
        }
        [HttpGet("coding-exercise-detail/{exerciseId}")]
        public async Task<IActionResult> GetCodingExerciseDetail(int exerciseId)
        {
            var exercise = await _codingExerciseService.GetExerciseDetail(exerciseId);
            if (exercise == null)
                return NotFound(new { message = "Bài tập không tồn tại" });

            return Ok(exercise);
        }
        [HttpGet("coding-exercise-classid/{classId}")]
        public async Task<IActionResult> GetAllCodingExerciseByClasID(int classId)
        {
            var exercise = await _codingExerciseService.GetAllCodingExerciseByClassID(classId);
            if (exercise == null)
                return NotFound(new { message = "Bài tập không tồn tại" });

            return Ok(exercise);
        }

        //Lấy ra tất cả thông tin cho site CodingExercise
        //[HttpGet("{id}/full-details")]
        //public async Task<ActionResult> GetFullDetails(int id)
        //{
        //    var exercise = await _codingExerciseService.GetByIdAsync(id);
        //    var testCases = await _testCaseService.GetByExerciseIdAsync(id);
        //    var submissions = await _submissionService.GetByExerciseIdAsync(id);

        //    return Ok(new
        //    {
        //        Exercise = _mapper.Map<CodingExerciseDto>(exercise),
        //        TestCases = _mapper.Map<List<TestCaseDto>>(testCases),
        //        Submissions = _mapper.Map<List<SubmissionDto>>(submissions)
        //    });
        //}
    }
}
