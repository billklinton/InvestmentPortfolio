using Investment.Portfolio.Domain.Abstractions.Exceptions;
using Investment.Portfolio.Domain.InvestmentPortfolio.Entities;
using Investment.Portfolio.Domain.InvestmentPortfolio.Repositories;
using Investment.Portfolio.Domain.InvestmentPortfolio.Service;
using Investment.Portfolio.Domain.YahooFinance.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Investment.Portfolio.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        private IStockService _stockService;
        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Post(Stock stock)
        {
            try
            {
                await _stockService.SaveStock(stock);
                return Ok(stock);
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new { ErrorMessage = e.Message });
            }
        }

        [HttpGet]
        [Route("getPrice")]
        public async Task<IActionResult> GetPrice(string ticker)
        {
            try
            {
                var response = await _stockService.SearchStockQuotesPrice(ticker);
                return Ok(response);
            }
            catch (DomainException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { ErrorMessage = e.Message });
            }
        }
    }
}
