using CourseManagerApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseManagerApp.Client.Services
{
    public interface IInstructorService
    {
        Task<IEnumerable<Instructor>>? GetAll();
        Task<Instructor?> GetInstructor(int id);
        Task<Instructor?> AddInstructor(Instructor instructor);
        Task<bool> UpdateInstructor(Instructor instructor);
        Task<bool> DeleteInstructor(int id);
    }
}
