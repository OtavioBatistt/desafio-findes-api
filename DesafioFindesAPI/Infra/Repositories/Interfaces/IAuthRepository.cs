using System;
using DesafioFindesAPI.Models;

namespace DesafioFindesAPI.Infra.Repositories.Interfaces
{
	public interface IAuthRepository
	{
        Task<User> AuthenticateAsync(string username, string password);
    }
}

