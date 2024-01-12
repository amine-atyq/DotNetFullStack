using Microsoft.AspNetCore.Mvc;
using CourseManagerApp.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CourseManagerApp.Server.Data;

namespace CourseManagerApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentCourseController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public StudentCourseController(ApiDbContext context)
        {
            _context = context;
        }

        // POST: api/StudentCourse
        [HttpPost]
        public async Task<ActionResult<StudentCourse>> AddStudentToCourse(StudentCourse studentCourse)
        {
            if (StudentCourseExists(studentCourse.StudentID, studentCourse.CourseID))
            {
                return BadRequest("Student is already enrolled in this course.");
            }

            _context.StudentCourses.Add(studentCourse);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudentCourse), new { studentId = studentCourse.StudentID, courseId = studentCourse.CourseID }, studentCourse);
        }

        // DELETE: api/StudentCourse/5/3
        [HttpDelete("{studentId}/{courseId}")]
        public async Task<IActionResult> RemoveStudentFromCourse(int studentId, int courseId)
        {
            var studentCourse = await _context.StudentCourses.FindAsync(studentId, courseId);
            if (studentCourse == null)
            {
                return NotFound();
            }

            _context.StudentCourses.Remove(studentCourse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/StudentCourse/5/Courses
        [HttpGet("{studentId}/Courses")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesByStudent(int studentId)
        {
            var courseIds = await _context.StudentCourses
                .Where(sc => sc.StudentID == studentId)
                .Select(sc => sc.CourseID)
                .ToListAsync();

            var courses = await _context.Courses
                .Where(c => courseIds.Contains(c.CourseID))
                .ToListAsync();

            return Ok(courses);
        }

        // GET: api/StudentCourse/3/Students
        [HttpGet("{courseId}/Students")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsByCourse(int courseId)
        {
            var studentIds = await _context.StudentCourses
                .Where(sc => sc.CourseID == courseId)
                .Select(sc => sc.StudentID)
                .ToListAsync();

            var students = await _context.Students
                .Where(s => studentIds.Contains(s.StudentID))
                .ToListAsync();

            return Ok(students);
        }

        // Helper method to check if a specific StudentCourse exists
        private bool StudentCourseExists(int studentId, int courseId)
        {
            return _context.StudentCourses.Any(sc => sc.StudentID == studentId && sc.CourseID == courseId);
        }

        // Helper method to get a specific StudentCourse
        [HttpGet("{studentId}/{courseId}")]
        public async Task<ActionResult<StudentCourse>> GetStudentCourse(int studentId, int courseId)
        {
            var studentCourse = await _context.StudentCourses.FindAsync(studentId, courseId);
            if (studentCourse == null)
            {
                return NotFound();
            }
            return studentCourse;
        }
    }
}
