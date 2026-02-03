using System.ComponentModel.DataAnnotations;

namespace StudySync.Data;

public class Course
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Course name is required")]
    [StringLength(100, ErrorMessage = "Course name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    [StringLength(50, ErrorMessage = "Course code cannot exceed 50 characters")]
    public string? Code { get; set; }

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }

    public string UserId { get; set; } = string.Empty;

    public ApplicationUser? User { get; set; }

    public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
