
using Incident_Reporting.Data;
using Incident_Reporting.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Incident_Reporting.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // per request- scoped

            services.AddScoped<ITCPL_Keystone_XL_Safety_ReportsContext, TCPL_Keystone_XL_Safety_ReportsContext>();
            services.AddScoped<IDataService<IncidentDataVM>, IncidentDataService>();
            // transient

            return services;
        }
    }
}
