namespace SimpleDemo.Infrastructure.Events
{
    public static class EventSuppressor
    {
        private class DisposableActionGuard(Action action) : IDisposable
        {
            public void Dispose()
            {
                Dispose(true);
            }

            private void Dispose(bool disposing)
            {
                if (disposing)
                {
                    action();
                }
            }
        }

        private static readonly AsyncLocal<bool> EventsSuppressedStorage = new();

        public static bool EventsSuppressed => EventsSuppressedStorage.Value;

        public static IDisposable SuppressEvents()
        {
            EventsSuppressedStorage.Value = true;
            return new DisposableActionGuard(() => { EventsSuppressedStorage.Value = false; });
        }
    }
}