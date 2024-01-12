using CourseManagerApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseManagerApp.Client.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>>? GetAll();
        Task<Course?> GetCourse(int id);
        Task<Course?> AddCourse(Course course);
        Task<bool> UpdateCourse(Course course);
        Task<bool> DeleteCourse(int id);
    }
}
