using BuildingBlocks.CQRS;
using DOCOUsers.Application.Dtos;

namespace DOCOUsers.Application.Users.Commands.CreateUser
{
    public record CreateUserCommand(UserDto User) : ICommand<CreateUserResult>;
    public record CreateUserResult(Guid Id);
}
