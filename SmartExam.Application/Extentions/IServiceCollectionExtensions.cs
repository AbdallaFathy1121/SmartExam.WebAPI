using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SmartExam.Application.AutoMapper;
using SmartExam.Application.Interfaces.Repositories;
using SmartExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddJWTToken(configuration);
            services.AddAutoMappers();
        }

        private static void AddJWTToken(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var Key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            });
        }

        private static void AddAutoMappers(this IServiceCollection services)
        {
            services
                // AutoMapper
               .AddAutoMapper(typeof(ChapterProfile).Assembly)
               .AddAutoMapper(typeof(SubjectProfile).Assembly)
               .AddAutoMapper(typeof(QuestionProfile).Assembly)
               .AddAutoMapper(typeof(ExamProfile).Assembly)
               .AddAutoMapper(typeof(ModelProfile).Assembly);
        }
    }
}
