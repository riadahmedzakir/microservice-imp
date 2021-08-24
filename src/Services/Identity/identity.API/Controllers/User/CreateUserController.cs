using System;

using identity.API.Entities;
using identity.API.Repositories;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace identity.API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class CreateUserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<CreateUserController> _logger;

        public CreateUserController(IUserRepository repository, ILogger<CreateUserController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            return Ok();
        }
    }
}
