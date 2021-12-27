
using Investment.Portfolio.Infra.CrossCutting.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Investment.Portfolio.Infra.CrossCutting
{
    public static class IoC
    {
        public static IServiceCollection Configure(this IServiceCollection services, IConfiguration configuration)
        {
            DataModule.Register(services, configuration);
            DomainModule.Register(services);
            return services;
        }
    }
}
