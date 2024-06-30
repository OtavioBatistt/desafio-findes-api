using System;
using System.Text.Json;
using DesafioFindesAPI.Infra.Repositories.Interfaces;
using DesafioFindesAPI.Models;

namespace DesafioFindesAPI.Infra.Repositories
{
	public class UserRepository : IUserRepository
	{
        private readonly FileStorageService<User> _fileStorageService;
        private string _filePath;

        public UserRepository()
		{
             _filePath = Path.Combine(Directory.GetCurrentDirectory(), "LocalStorage/users.json");
            _fileStorageService = new FileStorageService<User>(_filePath);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var users = await LoadUsersAsync();
            return users.FirstOrDefault(u => u.Id == id);
        }

        public async Task<List<User>> GetUserByRoleAsync(string role)
        {
            var users = await LoadUsersAsync();
            return users.Where(u => u.Role.Equals(role, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public async Task<List<User>> LoadUsersAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new List<User>();
            }

            var jsonData = await File.ReadAllTextAsync(_filePath);
            return string.IsNullOrEmpty(jsonData) ? new List<User>() : JsonSerializer.Deserialize<List<User>>(jsonData);
        }
    }
}

