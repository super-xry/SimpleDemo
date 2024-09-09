using System.Net;

namespace SimpleDemo.Shared.Exception
{
    public class SimpleInvalidException(string errorCode, string errorMessage) : SimpleBaseException(errorCode, HttpStatusCode.UnprocessableEntity, errorMessage)
    {
    }
}
