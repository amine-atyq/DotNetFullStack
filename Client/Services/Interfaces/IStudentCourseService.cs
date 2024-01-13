using CourseManagerApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseManagerApp.Client.Services
{
    public interface IStudentCourseService
    {
        Task<IEnumerable<StudentCourse>>? GetAll();
        Task<StudentCourse?> GetStudentCourse(int studentId, int courseId);
        Task<StudentCourse?> AddStudentCourse(StudentCourse studentCourse);
        Task<bool> DeleteStudentCourse(int studentId, int courseId);
        Task<List<Course>> GetEnrolledCourses(int studentId);
    }
}
