using System;

namespace Investment.Portfolio.Domain.Abstractions.Exceptions
{
    public sealed class YahooFinanceException : DomainException
    {
        private YahooFinanceException() : base() { }
        public YahooFinanceException(string message) : base(message) { }
        public YahooFinanceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
