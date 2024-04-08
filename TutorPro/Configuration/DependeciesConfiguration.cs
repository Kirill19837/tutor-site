using Microsoft.AspNetCore.Identity.UI.Services;
using TutorPro.Application.Interfaces;
using TutorPro.Application.Services;

namespace TutorPro.Configuration
{
    public static class DependeciesConfiguration
    {
        public static IServiceCollection AddServ(this IServiceCollection services)
        {
            services.AddScoped<IEmailSenderService, EmailSenderService>();

            return services;
        }
    }
}
