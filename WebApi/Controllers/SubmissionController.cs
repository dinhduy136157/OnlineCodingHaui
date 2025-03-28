using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCodingHaui.Application.DTOs.Lessons;
using OnlineCodingHaui.Application.DTOs.PistonAPI;
using OnlineCodingHaui.Application.DTOs.Submissions;
using OnlineCodingHaui.Application.DTOs.TestCases;
using OnlineCodingHaui.Application.Services.Interfaces;
using OnlineCodingHaui.Domain.Entity;
using Newtonsoft.Json;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private readonly ISubmissionService _submissionService;
        private readonly IMapper _mapper;
        private readonly IPistonApiService _pistonService;
        private readonly ITestCaseService _testCaseService;


        public SubmissionController(ISubmissionService submissionService, IMapper mapper, IPistonApiService pistonService, ITestCaseService testCaseService)
        {
            _submissionService = submissionService;
            _mapper = mapper;
            _pistonService = pistonService;
            _testCaseService = testCaseService;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var submission = await _submissionService.GetAllSubmissionAsync();
            var submissionDto = _mapper.Map<IEnumerable<SubmissionDto>>(submission);
            return Ok(submissionDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSubmissionByIdAsync(int id)
        {
            var submission = await _submissionService.GetByIdAsync(id);
            var submissionDto = _mapper.Map<SubmissionDto>(submission);
            return Ok(submissionDto);
        }
        [HttpPost("submissions")]
        public async Task<ActionResult> CreateSubmission(SubmissionDto submissionDto)
        {
            // 1️⃣ Map submission và lưu vào DB
            var submission = _mapper.Map<Submission>(submissionDto);
            submission.Status = "Processing";
            await _submissionService.AddSubmissionAsync(submission);

            // 2️⃣ Lấy danh sách test case từ DB
            var testCases = await _testCaseService.GetTestCaseByExerciseId(submissionDto.ExerciseID);
            if (testCases == null || !testCases.Any())
            {
                return BadRequest(new { Error = "No test cases found for this exercise." });
            }

            // 3️⃣ Xác định phiên bản ngôn ngữ
            var languageVersions = new Dictionary<string, string>
            {
                { "python", "3.12.0" },
                { "csharp", "10.0.0" },
                { "cpp", "10.2.0" }
            };

            // 4️⃣ Chạy từng test case riêng lẻ để kiểm tra output
            int passedCount = 0;
            List<object> resultDetails = new List<object>();

            foreach (var testCase in testCases)
            {
                var pistonResult = await _pistonService.ExecuteAsync(new PistonExecutionDto
                {
                    Code = submissionDto.Code,
                    Language = submissionDto.ProgrammingLanguage,
                    Version = languageVersions.GetValueOrDefault(submissionDto.ProgrammingLanguage, "latest"),
                    Stdin = testCase.InputData
                });

                if (!pistonResult.IsSuccess)
                {
                    submission.Status = "Failed";
                    submission.Result = JsonConvert.SerializeObject(new { Error = pistonResult.Error });
                    await _submissionService.UpdateSubmissionAsync(submission);
                    return BadRequest(new { Error = pistonResult.Error });
                }

                // 5️⃣ So sánh output với expected output
                string actualOutput = pistonResult.Output.Trim();
                string expectedOutput = testCase.ExpectedOutput.Trim();
                bool isPassed = actualOutput == expectedOutput;

                if (isPassed) passedCount++;

                // 🔹 Lưu thông tin từng test case vào JSON
                resultDetails.Add(new
                {
                    input = testCase.InputData,
                    expected = expectedOutput,
                    output = actualOutput,
                    status = isPassed ? "✅ Pass" : "❌ Fail"
                });
            }

            // 6️⃣ Cập nhật submission với JSON result
            submission.Result = JsonConvert.SerializeObject(resultDetails);
            submission.TestCasesPassed = passedCount;
            submission.TotalTestCases = testCases.Count;
            submission.Status = (passedCount == testCases.Count) ? "Accepted" : "Failed";

            await _submissionService.UpdateSubmissionAsync(submission);

            return Ok(new
            {
                SubmissionID = submission.SubmissionID,
                PassedTestCases = passedCount,
                TotalTestCases = testCases.Count,
                Status = submission.Status,
                Details = resultDetails
            });
        }




        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSubmission(int id, SubmissionDto submissionDto)
        {
            if (id != submissionDto.SubmissionID)
                return BadRequest();

            var submission = _mapper.Map<Submission>(submissionDto);
            await _submissionService.UpdateSubmissionAsync(submission);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSubmission(int id)
        {
            await _submissionService.DeleteSubmissionAsync(id);
            return Ok();
        }
    }
}
