using System;
using DesafioFindesAPI.Infra.Repositories.Interfaces;
using DesafioFindesAPI.Models;

namespace DesafioFindesAPI.Infra.Repositories
{
	public class AuthRepository : IAuthRepository
    {
        private readonly FileStorageService<User> _fileStorageService;

        public AuthRepository()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "LocalStorage/users.json");
            _fileStorageService = new FileStorageService<User>(filePath);
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var users = await _fileStorageService.LoadDataAsync();
            return users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}

