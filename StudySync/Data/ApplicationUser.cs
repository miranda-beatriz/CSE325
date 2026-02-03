using Microsoft.AspNetCore.Identity;

namespace StudySync.Data;

public class ApplicationUser : IdentityUser
{
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}
