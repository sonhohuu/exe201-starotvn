﻿using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Application.User;
using Exe.Starot.Application.User.Authenticate;
using Exe.Starot.Application.User.Register;
using Exe.Starot.Domain.Common.Exceptions;
using Exe.Starot.Domain.Entities.Base;
using Exe.Starot.Domain.Entities.Repositories;
using IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Exe.Starot.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IMediator _mediator;
        public AuthController(IUserRepository userRepository, IJwtService jwtService, IMediator mediator)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _mediator = mediator;
        }

        // Refresh token API to issue a new access token using the refresh token
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            var result = await _jwtService.RefreshTokenAsync(tokenRequest);

            if (result == null)
            {
                return Unauthorized("Invalid refresh token");
            }

            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginQuery loginQuery, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(loginQuery, cancellationToken);
            return Ok(new JsonResponse<UserLoginDTO>(StatusCodes.Status200OK, "Login Success", result));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(registerCommand, cancellationToken);
                return Ok(new JsonResponse<UserLoginDTO>(StatusCodes.Status200OK, "Register Success", result)); ;
            }
            catch (DuplicationException ex)
            {
                return BadRequest(new JsonResponse<string>(StatusCodes.Status400BadRequest, ex.Message, ""));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new JsonResponse<string>(StatusCodes.Status400BadRequest, ex.Message, ""));
            }
        }
    }

}
