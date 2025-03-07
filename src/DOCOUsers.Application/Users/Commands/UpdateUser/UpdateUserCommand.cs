using BuildingBlocks.CQRS;
using DOCOUsers.Application.Dtos;

namespace DOCOUsers.Application.Users.Commands.UpdateUser
{
    public record UpdateUserCommand(UserDto User) : ICommand<UpdateUserResult>;
    public record UpdateUserResult(UserDto User);
}
