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

        [HttpGet("login")]
        public string abc()
        {
            return "Pikina";
        }

        [HttpPost("Register")]
        public ResponseDetails Register(UserViewModel model)
        {
            ResponseDetails responseDetails = new ResponseDetails();

            try
            {
                _authService.Register(model, model.Password);
                responseDetails = StaticHelpers.SetResponSeDetails(true, null, MessageType.Success, "User Created Successfully.");
            }
            catch(Exception ex)
            {
                responseDetails = StaticHelpers.SetResponSeDetails(false, ex, MessageType.Error);
            }

            return responseDetails;
        }
    }
}