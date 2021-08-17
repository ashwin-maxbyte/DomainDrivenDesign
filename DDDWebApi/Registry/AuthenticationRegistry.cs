﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace DDDWebApi.Registry
{
    public class AuthenticationRegistry : IRegistry
    {
        private readonly IServiceCollection _services;
        private readonly string _siginingCredential;

        public AuthenticationRegistry(IServiceCollection services, string siginingCredential)
        {
            _services = services;
            _siginingCredential = siginingCredential;
        }

        public void Register()
        {
            _services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).
            AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_siginingCredential))
                };
            });
        }
    }
}
