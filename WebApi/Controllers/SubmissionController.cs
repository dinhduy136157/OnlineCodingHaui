using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCodingHaui.Application.DTOs.Lessons;
using OnlineCodingHaui.Application.DTOs.Submissions;
using OnlineCodingHaui.Application.Services.Interfaces;
using OnlineCodingHaui.Domain.Entity;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private readonly ISubmissionService _submissionService;
        private readonly IMapper _mapper;

        public SubmissionController(ISubmissionService submissionService, IMapper mapper)
        {
            _submissionService = submissionService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var submission = await _submissionService.GetAllSubmissionAsync();
            var submissionDto = _mapper.Map<IEnumerable<SubmissionDto>>(submission);
            return Ok(submissionDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSubmissionByIdAsync(int Id)
        {
            var submission = await _submissionService.GetByIdAsync(Id);
            var submissionDto = _mapper.Map<SubmissionDto>(submission);
            return Ok(submissionDto);
        }
        [HttpPost]
        public async Task<ActionResult> CreateSubmission(SubmissionDto submissionDto)
        {
            var submission = _mapper.Map<Submission>(submissionDto);
            await _submissionService.AddSubmissionAsync(submission);
            return Ok(submissionDto);
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
