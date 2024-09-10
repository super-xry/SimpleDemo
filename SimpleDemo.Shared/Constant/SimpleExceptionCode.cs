namespace SimpleDemo.Shared.Constant
{
    public static class SimpleExceptionCode
    {
        public struct Application
        {
            public const string QueueMessageIsRequired = "QueueMessageIsRequired";
            public const string QueryHandlerNotFound = "QueryHandlerNotFound";

            public const string UserNotFound = "UserNotFound";

            public const string UnknownException = "UnknownException";
        }
    }
}