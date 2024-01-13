using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagerApp.Shared.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseID { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "Title must not exceed 200 characters.")]
        public string Title { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description must not exceed 500 characters.")]
        public string Description { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Study hours must be a positive number.")]
        public int StudyHours { get; set; }

        // Additional properties based on the Razor component
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

        public int? InstructorID { get; set; }
        public Instructor? Instructor { get; set; }
    }
}