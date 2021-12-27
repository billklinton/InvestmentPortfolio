using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment.Portfolio.Domain.Abstractions.Exceptions
{
    public sealed class OperationServiceException : DomainException
    {
        private OperationServiceException() : base() { }
        public OperationServiceException(string message) : base(message) { }
        public OperationServiceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
