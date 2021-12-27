using System;

namespace Investment.Portfolio.Domain.Abstractions.Exceptions
{
    public sealed class StockServiceException : DomainException
    {
        private StockServiceException() : base() { }
        public StockServiceException(string message) : base(message) { }
        public StockServiceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
