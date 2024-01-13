using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagerApp.Shared.Models
{
    public class StudentCourse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentCourseID { get; set; }

        public int StudentID { get; set; }

        public int CourseID { get; set; }
        
        public Student Student { get; set; }
        
        public Course Course { get; set; }
    }

}