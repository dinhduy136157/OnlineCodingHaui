using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCodingHaui.Application.DTOs.Authentication;
using OnlineCodingHaui.Application.DTOs.Classes;
using OnlineCodingHaui.Application.Services.Interfaces;
using OnlineCodingHaui.Domain.Entity;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassStudentController : ControllerBase
    {
        private readonly IClassStudentService _classStudentService;
        private readonly IMapper _mapper;

        public ClassStudentController(IClassStudentService classStudentService, IMapper mapper)
        {
            _classStudentService = classStudentService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var classStudent = await _classStudentService.GetAllClassStudentAsync();
            var classStudentDto = _mapper.Map<IEnumerable<ClassStudentDto>>(classStudent);
            return Ok(classStudentDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetClassStudentByIdAsync(int id)
        {
            var classStudent = await _classStudentService.GetByIdAsync(id);
            var classStudentDto = _mapper.Map<ClassStudentDto>(classStudent);
            return Ok(classStudentDto);
        }
        [HttpPost]
        public async Task<ActionResult> CreateClassStudent(ClassStudentDto classStudentDto)
        {
            var classStudent = _mapper.Map<ClassStudent>(classStudentDto);
            await _classStudentService.AddClassStudentAsync(classStudent);
            return Ok(classStudentDto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateClassStudent(int id, ClassStudentDto classStudentDto)
        {
            if (id != classStudentDto.ClassID)
                return BadRequest();

            var classStudent = _mapper.Map<ClassStudent>(classStudentDto);
            await _classStudentService.UpdateClassStudentAsync(classStudent);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClassStudent(int id)
        {
            await _classStudentService.DeleteClassStudentAsync(id);
            return Ok();
        }

        [HttpGet("getStudentByClassId")]
        public async Task<IActionResult> GetStudentByClassId(int classId)
        {
            var classStudent = await _classStudentService.GetStudentByClass(classId);
            var classStudentDto = _mapper.Map<IEnumerable<StudentDto>>(classStudent);
            return Ok(classStudentDto);
        }
    }
}
