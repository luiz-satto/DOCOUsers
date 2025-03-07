using BuildingBlocks.Abstractions.Primitives;
using DOCOUsers.Application.Users.Commands.DeleteUser;
using DOCOUsers.UseCases.CreateUser;
using MediatR;

namespace DOCOUsers.UseCases.DeleteUser
{
    public class DeleteUserUseCase(
        ISender sender,
        ILogger<CreateUserUseCase> logger
    ) : IDeleteUserUseCase
    {
        public async Task<Result<bool>> DeleteUserAsync(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            DeleteUserCommand command = new(request.Id);
            DeleteUserResult result = await sender.Send(command);
            logger.LogInformation($"User : [{request.Id}] Deleted Successfuly!");
            return Result.Success(result.IsDeleted);
        }
    }
}
