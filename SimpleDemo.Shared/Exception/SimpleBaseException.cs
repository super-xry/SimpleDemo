using System.Net;

namespace SimpleDemo.Shared.Exception
{
    public abstract class SimpleBaseException(string errorCode, HttpStatusCode statusCode, string? errorMessage) : System.Exception
    {
        protected SimpleBaseException(string errorCode, HttpStatusCode statusCode) : this(errorCode, statusCode, null)
        {
        }

        public string ErrorCode { get; } = errorCode;

        public HttpStatusCode StatusCode { get; } = statusCode;

        public string? ErrorMessage { get; } = errorMessage;
    }
}
