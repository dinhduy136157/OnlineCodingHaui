using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCodingHaui.Application.DTOs.Authentication;
using OnlineCodingHaui.Application.DTOs.Classes;
using OnlineCodingHaui.Application.Services.Implementations;
using OnlineCodingHaui.Application.Services.Interfaces;
using OnlineCodingHaui.Domain.Entity;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classesService;
        private readonly IMapper _mapper;

        public ClassController(IClassService classesService, IMapper mapper)
        {
            _classesService = classesService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var classes = await _classesService.GetAllClassAsync();
            var classesDto = _mapper.Map<IEnumerable<ClassDto>>(classes);
            return Ok(classesDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetClassByIdAsync(int id)
        {
            var classes = await _classesService.GetByIdAsync(id);
            var classesDto = _mapper.Map<ClassDto>(classes);
            return Ok(classesDto);
        }
        [HttpPost]
        public async Task<ActionResult> CreateClass(ClassDto classesDto)
        {
            var classes = _mapper.Map<Class>(classesDto);
            var createdClass = await _classesService.AddClassAsync(classes);
            var response = new
            {
                    ClassID = createdClass.ClassID, // Lấy ClassID từ entity đã lưu
                    ClassName = createdClass.ClassName,
                    SubjectID = createdClass.SubjectID
                    // Các trường khác nếu cần
            };

            return Ok(response);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateClass(int id, ClassDto classesDto)
        {
            if (id != classesDto.ClassID)
                return BadRequest();

            var classes = _mapper.Map<Class>(classesDto);
            await _classesService.UpdateClassAsync(classes);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClass(int id)
        {
            await _classesService.DeleteClassAsync(id);
            return Ok();
        }
        [HttpGet("getClassByTeacherId")]
        public async Task<IActionResult> GetClassByTeacherId(int teacherId)
        {
            teacherId = 1;
            var classes = await _classesService.GetClassByTeacherId(teacherId);
            var classesDto = _mapper.Map<IEnumerable<ClassDto>>(classes);
            return Ok(classesDto);
        }
    }
}
