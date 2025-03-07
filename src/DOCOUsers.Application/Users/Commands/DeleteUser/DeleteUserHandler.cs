using BuildingBlocks.CQRS;
using DOCOUsers.Infrastructure.Repositories.User;

namespace DOCOUsers.Application.Users.Commands.DeleteUser
{
    public class DeleteUserHandler(IUserRepository userRepository)
        : ICommandHandler<DeleteUserCommand, DeleteUserResult>
    {
        public async Task<DeleteUserResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var result = await userRepository.DeleteAsync(request.Id, cancellationToken);
            return new DeleteUserResult(result);
        }
    }
}
