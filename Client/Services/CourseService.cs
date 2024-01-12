using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CourseManagerApp.Shared.Models;

namespace CourseManagerApp.Client.Services
{
    public class CourseService : ICourseService
    {
        private readonly HttpClient _httpClient;

        public CourseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Course?> AddCourse(Course course)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(course), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/courses", itemJson);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStreamAsync();
                    var result = await JsonSerializer.DeserializeAsync<Course>(responseBody, new JsonSerializerOptions()
                    {
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

        public async Task<bool> DeleteCourse(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/courses/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Course>>? GetAll()
        {
            try
            {
                var apiResponse = await _httpClient.GetStreamAsync("api/courses");
                var courses = await JsonSerializer.DeserializeAsync<Course[]>(apiResponse, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });
                return courses;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Course?> GetCourse(int id)
        {
            try
            {
                var apiResponse = await _httpClient.GetStreamAsync($"api/courses/{id}");
                var course = await JsonSerializer.DeserializeAsync<Course>(apiResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return course;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public async Task<bool> UpdateCourse(Course course)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(course), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/courses/{course.CourseID}", itemJson);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
