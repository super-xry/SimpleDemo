using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SimpleDemo.Infrastructure.Bus;
using SimpleDemo.Infrastructure.Events;

namespace SimpleDemo.Infrastructure.Extension
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseEventHandler<TEvent, TEventHandler>(this IApplicationBuilder appBuilder)
            where TEvent : class, IEvent
            where TEventHandler : IEventHandler<TEvent>
        {
            var inProcessBus = appBuilder.ApplicationServices.GetService<IHandlerRegistrar>();

            inProcessBus?.RegisterHandler<TEvent>(async (message, _) =>
            {
                using var scope = appBuilder.ApplicationServices.CreateScope();
                var handler = scope.ServiceProvider.GetService<TEventHandler>();
                if (handler == null) return;
                await handler.HandleAsync(message);
            });
        }
    }
}