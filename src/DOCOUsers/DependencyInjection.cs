using DOCOUsers.UseCases.CreateUser;
using DOCOUsers.UseCases.DeleteUser;
using DOCOUsers.UseCases.GetUser;
using DOCOUsers.UseCases.UpdateUser;

namespace DOCOUsers
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddScoped<IGetUserUseCase, GetUserUseCase>();
            services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
            services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
            return services;
        }
    }
}
