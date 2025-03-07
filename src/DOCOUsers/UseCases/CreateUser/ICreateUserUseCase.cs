using BuildingBlocks.Abstractions.Primitives;

namespace DOCOUsers.UseCases.CreateUser
{
    public record CreateUserRequest(string FirstName, string LastName, string UserName, string Password, string Email)
    {
        public CreateUserRequest() : this(default!, default!, default!, default!, default!)
        {
            
        }
    }

    public record CreateUserResponse(Guid Id);
    public interface ICreateUserUseCase
    {
        Task<Result<Guid>> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken);
    }
}
