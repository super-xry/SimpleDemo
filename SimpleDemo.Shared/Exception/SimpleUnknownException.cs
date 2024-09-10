using System.Net;

namespace SimpleDemo.Shared.Exception
{
    public class SimpleUnknownException(string errorCode, string? errorMessage)
        : SimpleBaseException(errorCode, HttpStatusCode.InternalServerError, errorMessage);
}