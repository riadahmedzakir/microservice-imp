using System;
using System.Collections.Generic;
using System.Security.Claims;

using Microsoft.IdentityModel.Tokens;

namespace identity.API.Entities
{
    public class JWTContainer : IJWTModel
    {
        public int ExpireMinutes { get; set; } = 1;
        public string SecretKey { get; set; } = "SUPERDUPERSECRETKEY==";
        public string SecurityAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;
        public List<Claim> Claims { get; set; }
    }
}
