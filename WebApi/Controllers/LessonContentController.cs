using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCodingHaui.Application.DTOs.Authentication;
using OnlineCodingHaui.Application.DTOs.Lessons;
using OnlineCodingHaui.Application.Services.Implementations;
using OnlineCodingHaui.Application.Services.Interfaces;
using OnlineCodingHaui.Domain.Entity;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonContentController : ControllerBase
    {
        private readonly ILessonContentService _lessonContentService;
        private readonly IMapper _mapper;

        public LessonContentController(ILessonContentService lessonContentService, IMapper mapper)
        {
            _lessonContentService = lessonContentService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var lessonContent = await _lessonContentService.GetAllLessonContentAsync();
            var lessonContentDto = _mapper.Map<IEnumerable<LessonContentDto>>(lessonContent);
            return Ok(lessonContentDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetLessonContentByIdAsync(int id)
        {
            var lessonContent = await _lessonContentService.GetByIdAsync(id);
            var lessonContentDto = _mapper.Map<LessonContentDto>(lessonContent);
            return Ok(lessonContentDto);
        }
        [HttpPost]
        public async Task<ActionResult> CreateLessonContent(LessonContentDto lessonContentDto)
        {
            var lessonContent = _mapper.Map<LessonContent>(lessonContentDto);
            await _lessonContentService.AddLessonContentAsync(lessonContent);
            return Ok(lessonContentDto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLessonContent(int id, LessonContentDto lessonContentDto)
        {
            if (id != lessonContentDto.ContentID)
                return BadRequest();

            var lessonContent = _mapper.Map<LessonContent>(lessonContentDto);
            await _lessonContentService.UpdateLessonContentAsync(lessonContent);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLessonContent(int id)
        {
            await _lessonContentService.DeleteLessonContentAsync(id);
            return Ok();
        }

        //Lấy ra chi tiết bài học theo lesson

        [HttpGet("lesson-detail")]
        public async Task<IActionResult> GetClassLessons(int lessonId)
        {
            var lessons = await _lessonContentService.GetLessonsContentAsync(lessonId);
            return Ok(lessons);
        }
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]

        public async Task<IActionResult> UploadLessonContentFile()
        {
            try
            {
                var form = Request.Form;

                if (!form.Files.Any())
                    return BadRequest("Không có file được tải lên.");

                var file = form.Files[0];

                // Validate file type
                var fileExtension = Path.GetExtension(file.FileName).ToLower();
                if (fileExtension != ".pdf")
                    return BadRequest("Chỉ chấp nhận file PDF");

                // Parse form data
                if (!int.TryParse(form["lessonId"], out int lessonId))
                    return BadRequest("LessonID không hợp lệ");

                var title = form["title"];
                var contentType = form["contentType"];
                var category = form["category"];

                // Tạo thư mục nếu chưa tồn tại
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "pdfs");
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                // Tạo tên file unique
                var fileName = $"{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine(uploadPath, fileName);

                // Lưu file vật lý
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Lưu metadata vào database
                var lessonContent = new LessonContent
                {
                    LessonID = lessonId,
                    Title = string.IsNullOrEmpty(title) ? Path.GetFileNameWithoutExtension(file.FileName) : title,
                    ContentType = "PDF", // Luôn set là PDF vì chỉ chấp nhận PDF
                    Category = category,
                    FileUrl = fileName
                };

                await _lessonContentService.AddLessonContentAsync(lessonContent);

                return Ok(new
                {
                    ContentID = lessonContent.ContentID,
                    LessonID = lessonContent.LessonID,
                    Title = lessonContent.Title,
                    ContentType = lessonContent.ContentType,
                    FileUrl = lessonContent.FileUrl,
                    Category = lessonContent.Category
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

    }
}
