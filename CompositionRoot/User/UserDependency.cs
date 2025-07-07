using Application.User;
using Domain.Infrastructure.Users;
using Domain.User.Driven;
using Domain.User.Driver;
using Domain.User.UseCases;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CompositionRoot.User
{
    public static class UserDependency
    {
        public static void AddUserDriver(IServiceCollection services)
        {
            services.TryAddTransient<IUserRepository, UserRepository>();
        }

        public static void AddUserDriven(IServiceCollection services)
        {
            services.TryAddTransient<IUserDriverPort, UserDriverImplementation>();
            services.TryAddTransient<IUserRepository, UserRepository>();
            services.TryAddTransient<CreateUserUseCase>();
        }
    }
}
