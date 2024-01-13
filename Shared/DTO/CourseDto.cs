namespace CourseManagerApp.Shared.DTO;

public class CourseDto
{
    public int CourseID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int StudyHours { get; set; }
    public string InstructorName { get; set; }
    public int? InstructorID { get; set; }
}