using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Claims;
using BusinessApp.Core.Entity.Users;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using BusinessApp.Core.ApplicationService.IService;
using BusinessApp.Core.ApplicationService.ServiceSetting;
using Microsoft.Extensions.Options;

namespace BusinessApp.Core.ApplicationService.Service
{
    public class TokenManagerService : ITokenManagerService
    {
        private readonly TokenProviderOptions _tokenProviderOptions;
        public TokenManagerService(IOptions<TokenProviderOptions> optionsAccessor)
        {
            _tokenProviderOptions = optionsAccessor.Value;
        }

        public string GenerateJwtToken(object user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenProviderOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(120);
            var token = new JwtSecurityToken(_tokenProviderOptions.Key,_tokenProviderOptions.Issuer,expires: expires,signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
