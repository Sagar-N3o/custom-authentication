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
            ResponseDetails responseDetails = new ResponseDetails();

            try
            {
                UserViewModel modelMapping = _authService.Login(model.Email, model.Password);

                if (modelMapping == null)
                    responseDetails = StaticHelpers.SetResponSeDetails(false, null, MessageType.Info, "Invalid login data.");

                responseDetails = StaticHelpers.SetResponSeDetails(true, modelMapping, MessageType.Success, "Successfully logged in.");
            }
            catch(Exception ex)
            {
                responseDetails = StaticHelpers.SetResponSeDetails(false, ex, MessageType.Error);
            }

            return responseDetails;
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