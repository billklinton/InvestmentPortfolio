using Investment.Portfolio.Domain.Abstractions.Exceptions;
using Investment.Portfolio.Domain.YahooFinance.Configs;
using Investment.Portfolio.Domain.YahooFinance.Dtos;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Investment.Portfolio.Domain.YahooFinance.Services
{
    public class YahooFinanceService : IYahooFinanceService
    {
        private readonly YahooFinanceConfig _config;
        public YahooFinanceService(YahooFinanceConfig config)
        {
            _config = config;
        }

        public async Task<QueryQuotesResponse> QueryQuotes(string searchText)
        {
            try
            {
                var result = await HttpGet($"{_config.QueryQuotesURL}q={searchText}");
                return JsonConvert.DeserializeObject<QueryQuotesResponse>(result);
            }
            catch (DomainException e)
            {
                throw new YahooFinanceException("Error on find Quotes in YahooFinance API", e);
            }
        }

        public async Task<QueryQuotePriceResponse> QueryQuotePrice(string symbol)
        {
            try
            {
                var result = await HttpGet($"{_config.QueryQuotePriceURL}symbols={symbol}");
                return JsonConvert.DeserializeObject<QueryQuotePriceResponse>(result);                
            }
            catch (DomainException e)
            {
                throw new YahooFinanceException("Error on find QuotePrice in YahooFinance API", e);
            }
        }

        private async Task<string> HttpGet(string url)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-API-KEY", _config.ApiKey);
            var response = await httpClient.GetAsync($"{url}&region=US&lang=en");
            var result = response.Content.ReadAsStringAsync().Result;
            return result;
        }
    }
}
