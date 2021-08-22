using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using identity.API.Entities;
using identity.API.Repositories;
using identity.API.Repositories.IdentityService;

namespace identity.API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository repository, ILogger<UserController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [CustomAuthorizeAttribute]
        [HttpGet("GetUser")]
        [ProducesResponseType(typeof(IEnumerable<User>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            var users = await _repository.GetUser();
            return Ok(users);
        }

        //[Authorize(Roles = "test")]
        [CustomAuthorizeAttribute]
        [HttpGet("GetUser/{UserId:length(36)}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<User>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<User>> GetUserById(string UserId)
        {
            var user = await _repository.GetUser(UserId);
            if (user == null)
            {
                _logger.LogError($"User with id : {UserId} not found.");
                return NotFound();
            }
            return Ok(user);
        }
    }
}
