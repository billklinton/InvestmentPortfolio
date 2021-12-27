using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment.Portfolio.Domain.InvestmentPortfolio.Entities
{
    public class Stock : Base
    {
        public string Ticker { get; set; }
        public string CorporateName { get; set; }
    }
}
