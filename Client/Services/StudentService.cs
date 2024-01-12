using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CourseManagerApp.Shared.Models;

namespace CourseManagerApp.Client.Services
{
    public class StudentService : IStudentService
    {
        private readonly HttpClient _httpClient;

        public StudentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Student?> AddStudent(Student student)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(student), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/students", itemJson);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStreamAsync();
                    var result = await JsonSerializer.DeserializeAsync<Student>(responseBody, new JsonSerializerOptions() { 
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

        public async Task<IEnumerable<Student>>? GetAll()
        {
            try
            {
                var apiResponse = await _httpClient.GetStreamAsync("api/students");
                var students = await JsonSerializer.DeserializeAsync<Student[]>(apiResponse, new JsonSerializerOptions() { 
                    PropertyNameCaseInsensitive = true 
                  });
                return students;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteStudent(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/students/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<Student?> GetStudent(int id)
        {
            try
            {
                var apiResponse = await _httpClient.GetStreamAsync($"api/students/{id}");
                var student = await JsonSerializer.DeserializeAsync<Student>(apiResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return student;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public async Task<bool> UpdateStudent(Student student)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(student), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/students/{student.StudentID}", itemJson);
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
