using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCodingHaui.Application.DTOs.Authentication;
using OnlineCodingHaui.Application.DTOs.CodingExercises;
using OnlineCodingHaui.Application.DTOs.Submissions;
using OnlineCodingHaui.Application.DTOs.TestCases;
using OnlineCodingHaui.Application.Services.Implementations;
using OnlineCodingHaui.Application.Services.Interfaces;
using OnlineCodingHaui.Domain.Entity;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodingExerciseController : ControllerBase
    {
        private readonly ICodingExerciseService _codingExerciseService;
        private readonly ITestCaseService _testCaseService;
        private readonly ISubmissionService _submissionService;
        private readonly IMapper _mapper;

        public CodingExerciseController(ICodingExerciseService codingExerciseService, ITestCaseService testCaseService, ISubmissionService submissionService, IMapper mapper)
        {
            _codingExerciseService = codingExerciseService;
            _testCaseService = testCaseService;
            _submissionService = submissionService;
            _mapper = mapper;
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
            return Ok(codingExerciseDto);
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
