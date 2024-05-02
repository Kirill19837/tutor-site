﻿using Microsoft.AspNetCore.Identity.UI.Services;
using TutorPro.Application.Interfaces;
using TutorPro.Application.Services;
using TutorPro.Middlewares;

namespace TutorPro.Configuration
{
    public static class DependeciesConfiguration
    {
        public static IServiceCollection AddServ(this IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddScoped<IEmailSenderService, EmailSenderService>();
            services.AddScoped<IMaterialsService, MaterialsService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IWaitlistUserService, WaitlistUserService>();

            services.AddTransient<ExceptionMiddleware>();

            return services;
        }
    }
}
