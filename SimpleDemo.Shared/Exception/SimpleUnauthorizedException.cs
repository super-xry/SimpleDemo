using System.Net;

namespace SimpleDemo.Shared.Exception
{
    public class SimpleUnauthorizedException(string errorCode, string? errorMessage) : SimpleBaseException(errorCode, HttpStatusCode.Unauthorized, errorMessage)
    {
    }
}