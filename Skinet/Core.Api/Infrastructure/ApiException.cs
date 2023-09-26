using System;
using System.Net;

namespace Core.Api.Infrastructure
{
    public class ApiException : Exception
    {
        public bool IsSafeMessage { get; private set; }

        public HttpStatusCode Status { get; private set; }

        public ApiException(HttpStatusCode statusCode, string message)
          : base(message)
        {
            Status = statusCode;
        }

        public ApiException(HttpStatusCode statusCode, string message, bool isSafeMessage)
          : base(message)
        {
            IsSafeMessage = isSafeMessage;
            Status = statusCode;
        }

        public ApiException(HttpStatusCode statusCode, string message, bool isSafeMessage, Exception innerException)
          : base(message, innerException)
        {
            IsSafeMessage = isSafeMessage;
            Status = statusCode;
        }
    }
}
