using System;
using DesafioFindesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesafioFindesAPI.Facades.Interfaces
{
	public interface IAuthFacade
	{
        Task<User> SignIn(string username, string password);
    }
}

