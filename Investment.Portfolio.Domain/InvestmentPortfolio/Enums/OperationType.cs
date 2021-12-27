using System.ComponentModel.DataAnnotations;

namespace Investment.Portfolio.Domain.InvestmentPortfolio.Enums
{
    public enum OperationType
    {
        [Display(Name = "Compra")]
        Buy,
        [Display(Name = "Venda")]
        Sell
    }
}
