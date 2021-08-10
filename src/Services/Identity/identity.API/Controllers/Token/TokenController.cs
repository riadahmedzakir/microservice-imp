using System;
using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using identity.API.Repositories;
using identity.API.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using identity.API.Data.Token;

namespace identity.API.Controllers.Token
{
    [ApiController]
    [Route("v1/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IJWTService _jwtService;
        private readonly ILogger<TokenController> _logger;

        public TokenController(IJWTService jwtService, ILogger<TokenController> logger)
        {
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public IActionResult GetToken ()
        {
            var token = _jwtService.GenerateToken("riad.zakir@yahoo.com", "dfdfdfd");
            return Ok(token);
        }

        [HttpPost("Validate")]
        public IActionResult ValidateToken()
        {
            var token = Request.Headers["Authorization"];
            if (token.Count == 1)
            {
                var isValid = _jwtService.IsTokenValid(token);
                return Ok(isValid);
            }                
            return Ok(false);
        }
    }
}
