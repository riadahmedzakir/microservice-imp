using System.Collections.Generic;
using System.Security.Claims;

namespace identity.API.Entities
{
    public interface IJWTModel
    {
        string SecretKey { get; set; }
        string SecurityAlgorithm { get; set; }
        int ExpireMinutes { get; set; }
        List<Claim> Claims { get; set; }
    }
}
