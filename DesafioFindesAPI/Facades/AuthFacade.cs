using System;
using DesafioFindesAPI.Facades.Interfaces;
using DesafioFindesAPI.Infra.Repositories;
using DesafioFindesAPI.Infra.Repositories.Interfaces;
using DesafioFindesAPI.Models;

namespace DesafioFindesAPI.Facades
{
	public class AuthFacade : IAuthFacade
    {
        private readonly IAuthRepository _authRepository;

        public AuthFacade()
		{
            _authRepository = new AuthRepository();
        }

        public async Task<User> SignIn(string username, string password)
        {
            return await _authRepository.AuthenticateAsync(username, password);
        }
    }
}

