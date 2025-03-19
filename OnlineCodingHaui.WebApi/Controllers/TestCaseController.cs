using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCodingHaui.Application.DTOs.Lessons;
using OnlineCodingHaui.Application.DTOs.TestCases;
using OnlineCodingHaui.Application.Services.Interfaces;
using OnlineCodingHaui.Domain.Entity;

namespace OnlineCodingHaui.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestCaseController : ControllerBase
    {
        private readonly ITestCaseService _testCaseService;
        private readonly IMapper _mapper;

        public TestCaseController(ITestCaseService testCaseService, IMapper mapper)
        {
            _testCaseService = testCaseService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var testCase = await _testCaseService.GetAllTestCaseAsync();
            var testCaseDto = _mapper.Map<IEnumerable<TestCaseDto>>(testCase);
            return Ok(testCaseDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTestCaseByIdAsync(int Id)
        {
            var testCase = await _testCaseService.GetByIdAsync(Id);
            var testCaseDto = _mapper.Map<TestCaseDto>(testCase);
            return Ok(testCaseDto);
        }
        [HttpPost]
        public async Task<ActionResult> CreateTestCase(TestCaseDto testCaseDto)
        {
            var testCase = _mapper.Map<TestCase>(testCaseDto);
            await _testCaseService.AddTestCaseAsync(testCase);
            return Ok(testCaseDto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTestCase(int id, TestCaseDto testCaseDto)
        {
            if (id != testCaseDto.TestCaseID)
                return BadRequest();

            var testCase = _mapper.Map<TestCase>(testCaseDto);
            await _testCaseService.UpdateTestCaseAsync(testCase);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTestCase(int id)
        {
            await _testCaseService.DeleteTestCaseAsync(id);
            return Ok();
        }
    }
}
