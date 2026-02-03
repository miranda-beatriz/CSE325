using System.ComponentModel.DataAnnotations;

namespace StudySync.Data;

public class Assignment
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Assignment title is required")]
    [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Due date is required")]
    public DateTime DueDate { get; set; }

    [Required]
    public AssignmentStatus Status { get; set; } = AssignmentStatus.ToDo;

    public AssignmentPriority Priority { get; set; } = AssignmentPriority.Medium;

    [Required]
    public int CourseId { get; set; }

    public Course? Course { get; set; }

    public string UserId { get; set; } = string.Empty;

    public ApplicationUser? User { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }
}

public enum AssignmentStatus
{
    ToDo,
    InProgress,
    Done
}

public enum AssignmentPriority
{
    Low,
    Medium,
    High
}
