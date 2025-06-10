﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Persons.API.Database;
using Persons.API.Database.Entities;
using Persons.API.Helpers;
using System.Text;

namespace Persons.API.Extensions
{
    public static class AuthenticationEstension
    {
        public static void AddAuthenticationConfig(
            this IServiceCollection services, 
            IConfiguration configuration) 
        {
            services.AddIdentity<UserEntity, RoleEntity>(options => {
                options.SignIn.RequireConfirmedAccount = false;
                options.Tokens.EmailConfirmationTokenProvider = "EmailConfirmation";            
            }).AddEntityFrameworkStores<PersonsDbContext>()
              .AddDefaultTokenProviders()
              .AddErrorDescriber<ErrorMessagesIdentity>();

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))

                };

            });
        }
    }
}
