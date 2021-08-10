using identity.API.Entities;
using identity.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace identity.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("v1/[controller]")]
    public class UserController: ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository repository, ILogger<UserController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("GetUser")]
        [ProducesResponseType(typeof(IEnumerable<User>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            var users = await _repository.GetUser();
            return Ok(users);
        }

        [HttpGet("GetUser/{UserId:length(36)}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<User>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<User>> GetUserById(string UserId)
        {
            var user = await _repository.GetUser(UserId);
            if(user == null)
            {
                _logger.LogError($"User with id : {UserId} not found.");
                return NotFound();
            }
            return Ok(user);
        }
    }    
}
