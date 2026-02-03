using Microsoft.EntityFrameworkCore;
using StudySync.Data;

namespace StudySync.Services;

public class AssignmentService
{
    private readonly ApplicationDbContext _context;

    public AssignmentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Assignment>> GetUserAssignmentsAsync(string userId)
    {
        return await _context.Assignments
            .Include(a => a.Course)
            .Where(a => a.UserId == userId)
            .OrderBy(a => a.DueDate)
            .ToListAsync();
    }

    public async Task<List<Assignment>> GetUpcomingAssignmentsAsync(string userId, int days = 7)
    {
        var today = DateTime.UtcNow.Date;
        var endDate = today.AddDays(days);

        return await _context.Assignments
            .Include(a => a.Course)
            .Where(a => a.UserId == userId &&
                       a.Status != AssignmentStatus.Done &&
                       a.DueDate >= today &&
                       a.DueDate <= endDate)
            .OrderBy(a => a.DueDate)
            .ThenByDescending(a => a.Priority)
            .ToListAsync();
    }

    public async Task<List<Assignment>> GetDueSoonAssignmentsAsync(string userId, int days = 3)
    {
        var today = DateTime.UtcNow.Date;
        var endDate = today.AddDays(days);

        return await _context.Assignments
            .Include(a => a.Course)
            .Where(a => a.UserId == userId &&
                       a.Status != AssignmentStatus.Done &&
                       a.DueDate >= today &&
                       a.DueDate <= endDate)
            .OrderBy(a => a.DueDate)
            .ThenByDescending(a => a.Priority)
            .ToListAsync();
    }

    public async Task<List<Assignment>> GetOverdueAssignmentsAsync(string userId)
    {
        var today = DateTime.UtcNow.Date;

        return await _context.Assignments
            .Include(a => a.Course)
            .Where(a => a.UserId == userId &&
                       a.Status != AssignmentStatus.Done &&
                       a.DueDate < today)
            .OrderBy(a => a.DueDate)
            .ToListAsync();
    }

    public async Task<List<Assignment>> GetAssignmentsByCourseAsync(int courseId, string userId)
    {
        return await _context.Assignments
            .Include(a => a.Course)
            .Where(a => a.CourseId == courseId && a.UserId == userId)
            .OrderBy(a => a.DueDate)
            .ToListAsync();
    }

    public async Task<Assignment?> GetAssignmentByIdAsync(int id, string userId)
    {
        return await _context.Assignments
            .Include(a => a.Course)
            .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
    }

    public async Task<Assignment> CreateAssignmentAsync(Assignment assignment)
    {
        _context.Assignments.Add(assignment);
        await _context.SaveChangesAsync();
        return assignment;
    }

    public async Task<bool> UpdateAssignmentAsync(Assignment assignment, string userId)
    {
        var existingAssignment = await _context.Assignments
            .FirstOrDefaultAsync(a => a.Id == assignment.Id && a.UserId == userId);

        if (existingAssignment == null)
            return false;

        existingAssignment.Title = assignment.Title;
        existingAssignment.Description = assignment.Description;
        existingAssignment.DueDate = assignment.DueDate;
        existingAssignment.Status = assignment.Status;
        existingAssignment.Priority = assignment.Priority;
        existingAssignment.CourseId = assignment.CourseId;

        if (assignment.Status == AssignmentStatus.Done && existingAssignment.CompletedAt == null)
        {
            existingAssignment.CompletedAt = DateTime.UtcNow;
        }
        else if (assignment.Status != AssignmentStatus.Done)
        {
            existingAssignment.CompletedAt = null;
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAssignmentAsync(int id, string userId)
    {
        var assignment = await _context.Assignments
            .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);

        if (assignment == null)
            return false;

        _context.Assignments.Remove(assignment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> MarkAsCompleteAsync(int id, string userId)
    {
        var assignment = await _context.Assignments
            .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);

        if (assignment == null)
            return false;

        assignment.Status = AssignmentStatus.Done;
        assignment.CompletedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }
}
