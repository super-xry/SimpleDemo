using System.Net;

namespace SimpleDemo.Shared.Exception
{
    public class SimpleNotFoundException(string errorCode, string? errorMessage)
        : SimpleBaseException(errorCode, HttpStatusCode.NotFound, errorMessage);
}