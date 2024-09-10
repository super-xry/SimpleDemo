using System.Net;

namespace SimpleDemo.Shared.Exception
{
    public class SimpleForbiddenException(string errorCode, string? errorMessage) : SimpleBaseException(errorCode, HttpStatusCode.Forbidden, errorMessage)
    {
    }
}