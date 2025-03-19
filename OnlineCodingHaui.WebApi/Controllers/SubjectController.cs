using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCodingHaui.Application.DTOs.Authentication;
using OnlineCodingHaui.Application.DTOs.Subjects;
using OnlineCodingHaui.Application.Services.Interfaces;
using OnlineCodingHaui.Domain.Entity;

namespace OnlineCodingHaui.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;

        public SubjectController(ISubjectService subjectService, IMapper mapper)
        {
            _subjectService = subjectService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var subject = await _subjectService.GetAllSubjectAsync();
            var subjectDto = _mapper.Map<IEnumerable<SubjectDto>>(subject);
            return Ok(subjectDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSubjectByIdAsync(int Id)
        {
            var subject = await _subjectService.GetByIdAsync(Id);
            var subjectDto = _mapper.Map<SubjectDto>(subject);
            return Ok(subjectDto);
        }
        [HttpPost]
        public async Task<ActionResult> CreateSubject(SubjectDto subjectDto)
        {
            var subject = _mapper.Map<Subject>(subjectDto);
            await _subjectService.AddSubjectAsync(subject);
            return Ok(subjectDto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSubject(int id, SubjectDto subjectDto)
        {
            var subject = _mapper.Map<Subject>(subjectDto);
            await _subjectService.UpdateSubjectAsync(subject);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSubject(int id)
        {
            await _subjectService.DeleteSubjectAsync(id);
            return Ok();
        }
    }
}
