using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Namshi.Infrastructure.Context;
using Microsoft.Extensions.Configuration;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using Microsoft.AspNetCore.Identity;
using SmartExam.Application.Interfaces.Repositories;
using SmartExam.Domain.Entities;
using SmartExam.Infrastructure.Repositories;
using FluentValidation.AspNetCore;
using SmartExam.Infrastructure.Validators.ChapterValidator;
using Application.Interfaces.Repositories;
using Infrastructure.Repositories;
using Application.Interfaces.Services;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SmartExam.Infrastructure.Validators.QuestionValidator;
using SmartExam.Infrastructure.Validators.ExamValidator;
using SmartExam.Infrastructure.Validators.ExamQueryValidator;
using SmartExam.Infrastructure.Validators.StudentExamValidator;
using Hangfire;
using SmartExam.Application.Interfaces.Services;
using SmartExam.Infrastructure.Services;
using Newtonsoft.Json;

namespace SmartExam.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);
            services.AddRepositories();
            services.AddFluentValidation();
            services.AddHangfire(configuration);
        }
        private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString,
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services
                // Repositories
                .AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork))
                .AddScoped<IJWTManagerRepository, JWTManagerRepository>()

                // Services
                .AddScoped<IUserService, UserService>()
                .AddTransient<IChangeExamStatus, ChangeExamStatus>()

                .Configure<IdentityOptions>(options =>
                {
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 3;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 8;
                    options.User.RequireUniqueEmail = true;
                })

                // Identities
                .AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
        }

        private static void AddFluentValidation(this IServiceCollection services)
        {
            services
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ChapterValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ModelValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<QuestionValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ExamValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ExamQueryValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<StudentExamValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<SubjectValidator>());
        }

        private static void AddHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddHangfire((sp, config) =>
            {
                //Add the line below
                config.UseSqlServerStorage(connectionString);
                config.UseSerializerSettings(new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            });

            services.AddHangfireServer();
        }

    }
}
