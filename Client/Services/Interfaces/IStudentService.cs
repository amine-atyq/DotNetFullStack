using CourseManagerApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseManagerApp.Client.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>>? GetAll();
        Task<Student?> GetStudent(int id);
        Task<Student?> AddStudent(Student student);
        Task<bool> UpdateStudent(Student student);
        Task<bool> DeleteStudent(int id);
    }
}
