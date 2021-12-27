using Investment.Portfolio.Domain.InvestmentPortfolio.Entities;
using Investment.Portfolio.Domain.InvestmentPortfolio.Enums;
using Investment.Portfolio.Domain.InvestmentPortfolio.Repositories;
using Investment.Portfolio.Domain.InvestmentPortfolio.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Investment.Portfolio.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperationController : ControllerBase
    {
        private IOperationService _operationService;
        public OperationController(IOperationService operationService)
        {
            _operationService = operationService;
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Post(string ticker, int quantity, OperationType operationType)
        {
            try
            {
                var operation = await _operationService.SaveOperation(ticker, quantity, operationType);
                return Ok(operation);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { ErrorMessage = e.Message });
            }
        }

        [HttpGet]
        [Route("operations")]
        public async Task<IActionResult> GetOperations(string ticker)
        {
            try
            {
                var operations = await _operationService.GetOperations(ticker);
                return Ok(operations);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { ErrorMessage = e.Message });
            }
        }
    }
}
