using System;
using Investment.Portfolio.Domain.Abstractions.Helpers;
using Investment.Portfolio.Domain.InvestmentPortfolio.Enums;

namespace Investment.Portfolio.Domain.InvestmentPortfolio.Entities
{
    public class Operation : Base
    {
        public Guid StockId { get; set; }
        public Stock Stock { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public DateTime OperationDate => DateTime.Now;
        public double TotalAmount { get; set; }
        public OperationType OperationType { get; set; }
        public string OperationTypeDescription => OperationType.GetDescriptionName();
    }
}
