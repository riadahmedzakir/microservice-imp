using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation.Results;
using identity.API.Entities;
using identity.API.Repositories;
using identity.API.Repositories.TenantIdentity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace identity.API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class CreateUserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITenantIdentityRepository _tenantRepository;
        private readonly ILogger<CreateUserController> _logger;

        public CreateUserController(IUserRepository userRepository, ITenantIdentityRepository tenantRepository, ILogger<CreateUserController> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _tenantRepository = tenantRepository ?? throw new ArgumentNullException(nameof(tenantRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public async Task<ActionResult<ValidationResult>> Create(User user)
        {
            string referer = HttpContext.Request.Headers["Referer"];
            string origin = HttpContext.Request.Headers["Origin"];

            string refererDomain = Regex.Replace(referer.Replace("http://", "").Replace("https://", ""), ":\\d+/", "");
            string originDomain = Regex.Replace(origin.Replace("http://", "").Replace("https://", ""), ":\\d+", "");

            if (string.IsNullOrEmpty(referer.Replace("http://", "").Replace("https://", "")) || string.IsNullOrEmpty(origin))
            {
                return StatusCode(500);
            }

            Tenant tenant = await _tenantRepository.GetIdentity(refererDomain, originDomain);
            ValidationResult validationResult = await _userRepository.CrateUser(user, tenant.TenantId);
            return Ok(validationResult);
        }
    }
}
