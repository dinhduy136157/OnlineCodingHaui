using Microsoft.EntityFrameworkCore;
using OnlineCodingHaui.Domain.Entity;
using OnlineCodingHaui.Infrastructure.Context;
using OnlineCodingHaui.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Infrastructure.Repositories.Implementations
{
    public class LessonRepository : GenericRepository<Lesson>, ILessonRepository
    {
        public LessonRepository(OnlineCodingHauiContext context) : base(context)
        {

        }
        public async Task<List<Lesson>> GetLessonsByClassIdAsync(int classId)
        {
            return await _context.Lessons
                .Where(l => l.ClassID == classId)
                .ToListAsync();
        }
        public async Task CopyLessonsAndContentsFromSampleClassAsync(int targetClassId, string subjectId)
        {
            // 1. Tìm lớp mẫu đầu tiên cùng subject nhưng khác class đích
            var sourceClass = await _context.Classes
                .Where(c => c.SubjectID == subjectId && c.ClassID != targetClassId)
                .OrderBy(c => c.ClassID)
                .FirstOrDefaultAsync();

            if (sourceClass == null)
                throw new Exception("Không tìm thấy lớp mẫu phù hợp.");

            // 2. Lấy danh sách Lesson từ lớp mẫu
            var oldLessons = await _context.Lessons
                .Where(l => l.ClassID == sourceClass.ClassID)
                .ToListAsync();

            // 3. Tạo ánh xạ OldLessonID -> NewLessonID
            var lessonMap = new Dictionary<int, int>();

            foreach (var oldLesson in oldLessons)
            {
                var newLesson = new Lesson
                {
                    LessonTitle = oldLesson.LessonTitle,
                    ClassID = targetClassId
                };

                _context.Lessons.Add(newLesson);
                await _context.SaveChangesAsync(); // Save ngay để lấy được LessonID mới

                lessonMap[oldLesson.LessonID] = newLesson.LessonID;

                // 4. Copy LessonContents tương ứng
                var contents = await _context.LessonContents
                    .Where(c => c.LessonID == oldLesson.LessonID)
                    .ToListAsync();

                foreach (var content in contents)
                {
                    var newContent = new LessonContent
                    {
                        LessonID = newLesson.LessonID,
                        Title = content.Title,
                        ContentType = content.ContentType,
                        FileUrl = content.FileUrl,
                        Category = content.Category
                    };

                    _context.LessonContents.Add(newContent);
                }

                await _context.SaveChangesAsync();
            }
        }

    }
}
