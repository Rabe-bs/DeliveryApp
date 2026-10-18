using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DeliveryApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            // Register MediatR Handlers
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            // Register FluentValidation Validators
            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}