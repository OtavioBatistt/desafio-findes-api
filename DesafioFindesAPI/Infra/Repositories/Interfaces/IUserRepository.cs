using System;
using DesafioFindesAPI.Models;

namespace DesafioFindesAPI.Infra.Repositories.Interfaces
{
	public interface IUserRepository
	{
        Task<User> GetUserByIdAsync(int id);
        Task<List<User>> LoadUsersAsync();
        Task<List<User>> GetUserByRoleAsync(string role);
    }
}

