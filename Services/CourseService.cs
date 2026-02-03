using Microsoft.EntityFrameworkCore;
using StudySync.Data;

namespace StudySync.Services;

public class CourseService
{
    private readonly ApplicationDbContext _context;

    public CourseService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Course>> GetUserCoursesAsync(string userId)
    {
        return await _context.Courses
            .Where(c => c.UserId == userId)
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<Course?> GetCourseByIdAsync(int id, string userId)
    {
        return await _context.Courses
            .Include(c => c.Assignments)
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
    }

    public async Task<Course> CreateCourseAsync(Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
        return course;
    }

    public async Task<bool> UpdateCourseAsync(Course course, string userId)
    {
        var existingCourse = await _context.Courses
            .FirstOrDefaultAsync(c => c.Id == course.Id && c.UserId == userId);

        if (existingCourse == null)
            return false;

        existingCourse.Name = course.Name;
        existingCourse.Code = course.Code;
        existingCourse.Description = course.Description;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteCourseAsync(int id, string userId)
    {
        var course = await _context.Courses
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

        if (course == null)
            return false;

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
        return true;
    }
}
