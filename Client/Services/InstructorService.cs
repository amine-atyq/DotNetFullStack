using CourseManagerApp.Shared.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CourseManagerApp.Client.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly HttpClient httpClient;

        public InstructorService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Instructor>>? GetAll()
        {
            try
            {
                // Replace with actual API endpoint or data access logic
                return await httpClient.GetFromJsonAsync<IEnumerable<Instructor>>("api/instructors");
            }
            catch (HttpRequestException)
            {
                // Handle exception or logging
                return null;
            }
        }

        public async Task<Instructor?> GetInstructor(int id)
        {
            try
            {
                // Replace with actual API endpoint or data access logic
                return await httpClient.GetFromJsonAsync<Instructor>($"api/instructors/{id}");
            }
            catch (HttpRequestException)
            {
                // Handle exception or logging
                return null;
            }
        }

        public async Task<Instructor?> AddInstructor(Instructor instructor)
        {
            try
            {
                // Replace with actual API endpoint or data access logic
                var response = await httpClient.PostAsJsonAsync("api/instructors", instructor);
                return await response.Content.ReadFromJsonAsync<Instructor>();
            }
            catch (HttpRequestException)
            {
                // Handle exception or logging
                return null;
            }
        }

        public async Task<bool> UpdateInstructor(Instructor instructor)
        {
            try
            {
                // Replace with actual API endpoint or data access logic
                var response = await httpClient.PutAsJsonAsync($"api/instructors/{instructor.InstructorID}", instructor);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                // Handle exception or logging
                return false;
            }
        }

        public async Task<bool> DeleteInstructor(int id)
        {
            try
            {
                // Replace with actual API endpoint or data access logic
                var response = await httpClient.DeleteAsync($"api/instructors/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                // Handle exception or logging
                return false;
            }
        }
    }
}
