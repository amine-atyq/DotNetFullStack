using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CourseManagerApp.Shared.Models;

namespace CourseManagerApp.Client.Services
{
    public class StudentCourseService : IStudentCourseService
    {
        private readonly HttpClient _httpClient;
        private readonly ICourseService _courseService;
        private readonly IStudentService _studentService;

        public StudentCourseService(HttpClient httpClient, ICourseService courseService, IStudentService studentService)
        {
            _httpClient = httpClient;
            _courseService = courseService;
            _studentService = studentService;
        }

        public async Task<StudentCourse?> AddStudentCourse(StudentCourse studentCourse)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(studentCourse), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/StudentCourse", itemJson);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStreamAsync();
                    var result = await JsonSerializer.DeserializeAsync<StudentCourse>(responseBody, new JsonSerializerOptions() { 
                       PropertyNameCaseInsensitive = true 
                      });
                    return result;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public async Task<IEnumerable<StudentCourse>>? GetAll()
        {
            try
            {
                var apiResponse = await _httpClient.GetStreamAsync("api/studentcourse");
                var studentCourses = await JsonSerializer.DeserializeAsync<StudentCourse[]>(apiResponse, new JsonSerializerOptions() { 
                    PropertyNameCaseInsensitive = true 
                  });
                return studentCourses;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteStudentCourse(int studentId, int courseId)
        {
            var response = await _httpClient.DeleteAsync($"api/studentcourse/{studentId}/{courseId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<StudentCourse?> GetStudentCourse(int studentId, int courseId)
        {
            try
            {
                var apiResponse = await _httpClient.GetStreamAsync($"api/studentcourse/{studentId}/{courseId}");
                var studentCourse = await JsonSerializer.DeserializeAsync<StudentCourse>(apiResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return studentCourse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        
        public async Task<List<Course>> GetEnrolledCourses(int studentId)
        {
            try
            {
                var studentCourses = await GetAll();
                if (studentCourses != null && studentCourses.Any())
                {
                    var enrolledCourses = new List<Course>();

                    foreach (var studentCourse in studentCourses.Where(sc => sc.StudentID == studentId))
                    {
                        var course = await _courseService.GetCourse(studentCourse.CourseID);
                        if (course != null)
                        {
                            enrolledCourses.Add(course);
                        }
                    }

                    return enrolledCourses;
                }

                return new List<Course>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Handle the exception as needed
                return new List<Course>();
            }
        }
        
        public async Task<List<Student>> GetEnrolledStudents(int courseId)
        {
            try
            {

                var studentCourses = await GetAll();
                if (studentCourses != null && studentCourses.Any())
                {
                    var enrolledStudents = new List<Student>();

                    foreach (var studentCourse in studentCourses.Where(sc => sc.CourseID == courseId))
                    {
                        var student = await _studentService.GetStudent(studentCourse.StudentID);
                        if (student != null)
                        {
                            enrolledStudents.Add(student);
                        }
                    }

                    Console.WriteLine($"Fetched {enrolledStudents.Count} students for course ID: {courseId}");

                    return enrolledStudents;
                }

                Console.WriteLine($"No student courses found for course ID: {courseId}");
                return new List<Student>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching enrolled students: {ex.Message}");
                // Log or handle the exception as needed
                return new List<Student>();
            }
        }




    }
}
