namespace CourseManagerApp.Shared.Models
{
    public class StudentCourse
    {
        public int StudentID { get; set; }
        public Student Student { get; set; } = null!;

        public int CourseID { get; set; }
        public Course Course { get; set; } = null!;
    }
}
