using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseManagerApp.Shared.Models
{
    public class Instructor {
        public int InstructorID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string OfficeNumber { get; set; } = string.Empty;

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
