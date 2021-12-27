using Investment.Portfolio.Domain.InvestmentPortfolio.Configs;
using Investment.Portfolio.Domain.InvestmentPortfolio.Repositories;
using Investment.Portfolio.Domain.InvestmentPortfolio.Service;
using Investment.Portfolio.Domain.InvestmentPortfolio.Services;
using Investment.Portfolio.Domain.YahooFinance.Configs;
using Investment.Portfolio.Domain.YahooFinance.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Investment.Portfolio.Infra.CrossCutting.Modules
{
    public static class DomainModule
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IYahooFinanceService>(provider =>
            {
                var config = provider.GetRequiredService<YahooFinanceConfig>();
                return new YahooFinanceService(config);
            });

            services.AddScoped<IStockService>(provider =>
            {
                return new StockService(provider.GetRequiredService<IStockRepository>(), provider.GetRequiredService<IYahooFinanceService>());
            });

            services.AddScoped<IOperationService>(provider =>
            {
                var config = provider.GetRequiredService<OperationConfig>();
                return new OperationService(provider.GetRequiredService<IOperationRepository>(),
                                            provider.GetRequiredService<IYahooFinanceService>(), 
                                            provider.GetRequiredService<IStockService>(),
                                            config);
            });
        }
    }
}
