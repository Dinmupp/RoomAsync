using Domain.Notification.Driver;
using Domain.Notification.UseCase;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CompositionRoot.Notification
{
    public static class NotificationDependency
    {
        public static void AddNotificationDriver(IServiceCollection services)
        {
            services.TryAddTransient<INotificationDriverPort, NotificationDriverImplementation>();
            services.TryAddTransient<SendSmsUseCase>();
            services.TryAddTransient<SendEmailUseCase>();
        }
    }
}
