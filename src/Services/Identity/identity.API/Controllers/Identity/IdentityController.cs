using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using identity.API.Repositories.TenantIdentity;
using identity.API.Entities;
using System.Text.RegularExpressions;

namespace identity.API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly ITenantIdentityRepository _repository;
        private readonly ILogger<IdentityController> _logger;
        public IdentityController(ITenantIdentityRepository repository, ILogger<IdentityController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [AllowAnonymous]
        [HttpGet("GetIdentity")]
        public async Task<ActionResult<Tenant>> GetIdentity()
        {
            string referer = HttpContext.Request.Headers["Referer"];
            string origin = HttpContext.Request.Headers["Origin"];

            string refererDomain = Regex.Replace(referer.Replace("http://", "").Replace("https://", ""), ":\\d+/", "");
            string originDomain = Regex.Replace(origin.Replace("http://", "").Replace("https://", ""), ":\\d+", "");

            if (string.IsNullOrEmpty(referer.Replace("http://", "").Replace("https://", "")) || string.IsNullOrEmpty(origin))
            {
                return StatusCode(500);
            }

            Tenant tenant = await _repository.GetIdentity(refererDomain, originDomain);

            return Ok(tenant);
        }
    }
}
