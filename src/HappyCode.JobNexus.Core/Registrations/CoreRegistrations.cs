using HappyCode.JobNexus.Core.Repositories;
using HappyCode.JobNexus.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCode.JobNexus.Core.Registrations
{
    public static class CoreRegistrations
    {
        public static IServiceCollection AddCoreComponents(this IServiceCollection services)
        {
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ICarService, CarService>();

            return services;
        }
    }
}
