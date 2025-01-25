using Avance2Progreso.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Avance2Progreso.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:5206/api/Users";

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // CREATE
        public async Task<User> CreateUserAsync(User user)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/create", user);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<User>();
        }

        // READ ALL
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<User>>($"{BaseUrl}/all");
        }

        // READ ONE
        public async Task<User> GetUserByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/get/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<User>();
            }
            else
            {
                throw new Exception($"User with ID {id} not found.");
            }
        }

        // UPDATE
        public async Task UpdateUserAsync(int id, User user)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{BaseUrl}/update/{id}", jsonContent);
            response.EnsureSuccessStatusCode();
        }

        // DELETE
        public async Task DeleteUserAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/delete/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
