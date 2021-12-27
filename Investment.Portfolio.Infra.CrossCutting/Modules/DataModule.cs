
using Investment.Portfolio.Domain.InvestmentPortfolio.Repositories;
using Investment.Portfolio.Infra.Data.Repositories;
using Investment.Portfolio.Infra.Data.Repositories.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Investment.Portfolio.Infra.CrossCutting.Modules
{
    public static class DataModule
    {
        private static readonly string ConnectionString = "mssql_connection_string";

        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBaseRepository>(_ => new BaseRepository(configuration.GetConnectionString(ConnectionString)));

            services.AddScoped<IStockRepository>(provider =>
            {
                return new StockRepository(provider.GetRequiredService<IBaseRepository>());
            });

            services.AddScoped<IOperationRepository>(provider =>
            {
                return new OperationRepository(provider.GetRequiredService<IBaseRepository>());
            });
        }
    }
}
