using System;
using CustomTokenBasedAuthentication.Business.Contracts.Services.Auth;
using CustomTokenBasedAuthentication.Business.Enums;
using CustomTokenBasedAuthentication.Business.Helpers;
using CustomTokenBasedAuthentication.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomTokenBasedAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ResponseDetails Login(LoginDTO model)
        {
            try
            {
                string token = _authService.Login(model.Email, model.Password);

                if (token == null)
                    return  StaticHelpers.SetResponSeDetails(false, null, MessageType.Info, "Invalid login data.");

                return StaticHelpers.SetResponSeDetails(true, token, MessageType.Success, "Successfully logged in.");
            }
            catch(Exception ex)
            {
                return StaticHelpers.SetResponSeDetails(false, ex, MessageType.Error);
            }
        }

        [HttpPost("Register")]
        public ResponseDetails Register(UserViewModel model)
        {
            var request = Request;

            var token = request.Headers["Authorize"];
            try
            {
                UserViewModel modelMapping = _authService.Register(model, model.Password);

                if (modelMapping == null)
                    return StaticHelpers.SetResponSeDetails(false, null, MessageType.Info, "Email already exists.");
                return StaticHelpers.SetResponSeDetails(true, null, MessageType.Success, "User Created Successfully.");
            }
            catch(Exception ex)
            {
                return StaticHelpers.SetResponSeDetails(false, ex, MessageType.Error);
            }
        }
    }
}