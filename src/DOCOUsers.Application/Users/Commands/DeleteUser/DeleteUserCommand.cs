using BuildingBlocks.CQRS;

namespace DOCOUsers.Application.Users.Commands.DeleteUser
{
    public record DeleteUserCommand(Guid Id) : ICommand<DeleteUserResult>;
    public record DeleteUserResult(bool IsDeleted);
}
