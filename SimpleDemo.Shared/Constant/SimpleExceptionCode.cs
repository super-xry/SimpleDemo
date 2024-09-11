namespace SimpleDemo.Shared.Constant
{
    public static class SimpleExceptionCode
    {
        public struct Application
        {
            public const string QueueMessageIsRequired = "QueueMessageIsRequired";
            public const string QueryHandlerNotFound = "QueryHandlerNotFound";

            public const string UserNotFound = "UserNotFound";
            public const string UserRoleNotFound = "UserRoleNotFound";

            public const string UnknownException = "UnknownException";
        }

        public struct Security
        {
            public const string InvalidToken = "InvalidToken";
            public const string AccessDenied = "AccessDenied";
            public const string Unauthorized = "Unauthorized";
        }
    }
}