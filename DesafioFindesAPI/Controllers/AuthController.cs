using System;
using DesafioFindesAPI.Facades;
using DesafioFindesAPI.Facades.Interfaces;
using DesafioFindesAPI.Infra.Repositories;
using DesafioFindesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesafioFindesAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private IAuthFacade _authFacade;

        public AuthController()
        {
            _authFacade = new AuthFacade();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _authFacade.SignIn(request.Username, request.Password);

            if (user == null)
            {
                return Unauthorized("Credenciais inválidas!");
            }
            
            return Ok(new { user.Id, user.Username, user.Role });
        }
    }
}

