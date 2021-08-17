using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace DDDWebApi.Helpers
{
    public interface IJwtHelper
    {
        string IssueJwt(IEnumerable<Claim> claims, DateTime expiry, string key);

        bool ValidateJwt(string token, string key);

        Dictionary<string, string> GetClaims(string token);
    }
}
