using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseManagerApp.Shared.Models
{
    public class Student
    {
        public int StudentID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}
