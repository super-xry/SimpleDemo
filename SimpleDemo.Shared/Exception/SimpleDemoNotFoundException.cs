using System.Net;

namespace SimpleDemo.Shared.Exception
{
    public class SimpleDemoNotFoundException(string errorCode, string? errorMessage)
        : SimpleBaseException(errorCode, HttpStatusCode.NotFound, errorMessage);
}
