using BuildingBlocks.Abstractions.Primitives;
using DOCOUsers.Application.Dtos;
using DOCOUsers.Application.Users.Commands.UpdateUser;
using MediatR;

namespace DOCOUsers.UseCases.UpdateUser
{
    public class UpdateUserUseCase(
        ISender sender,
        ILogger<UpdateUserUseCase> logger
    ) : IUpdateUserUseCase
    {
        public async Task<Result<UserDto>> UpdateUserAsync(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            UpdateUserCommand command = new(request.User);
            UpdateUserResult result = await sender.Send(command, cancellationToken);
            logger.LogInformation($"User : [{result.User.UserName}] Updated Successfuly!");
            return Result.Success(result.User);
        }
    }
}
