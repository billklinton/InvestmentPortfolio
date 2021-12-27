using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment.Portfolio.Domain.InvestmentPortfolio.Dtos
{
    public class StockQuote
    {
        public Stock Stock { get; set; }
        public Quote Quote { get; set; }
    }

    public class Quote
    {
        public double Price { get; set; }
        public DateTime ConsultingDate { get; set; }
    }

    public class Stock
    {
        public string Ticker { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
    }
}
