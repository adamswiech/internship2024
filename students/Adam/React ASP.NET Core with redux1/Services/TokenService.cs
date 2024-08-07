﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace React_ASP.NET_Core_with_redux1.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(string email);
    }
    public class TokenService : ITokenService
    {
        private readonly SigningConfigurations _signingConfigurations;
        private readonly TokenConfigurations _tokenConfigurations;

        public TokenService([FromServices] SigningConfigurations signingConfigurations, [FromServices] TokenConfigurations tokenConfigurations)
        {
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }
        public string GenerateAccessToken(string email)
        {
            var claims = GetClaims(email);
            var identity = new ClaimsIdentity(claims);
            var handler = new JwtSecurityTokenHandler();

            var date = DateTime.Now;
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = date,
                Expires = date + TimeSpan.FromSeconds(_tokenConfigurations.Seconds)
            });

            return handler.WriteToken(securityToken);
        }

        private static List<Claim> GetClaims(string email) => new List<Claim>(){
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, email)};
    }
}
