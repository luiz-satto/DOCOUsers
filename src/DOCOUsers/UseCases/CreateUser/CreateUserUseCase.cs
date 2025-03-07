using BuildingBlocks.Abstractions.Primitives;
using DOCOUsers.Application.Dtos;
using DOCOUsers.Application.Users.Commands.CreateUser;
using MediatR;

namespace DOCOUsers.UseCases.CreateUser
{
    public class CreateUserUseCase(
        ISender sender,
        ILogger<CreateUserUseCase> logger
    ) : ICreateUserUseCase
    {
        public async Task<Result<Guid>> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken)
        {
            UserDto user = new(
                Guid.NewGuid(),
                request.FirstName,
                request.LastName,
                request.UserName,
                request.Password,
                request.Email);

            CreateUserCommand command = new(user);
            CreateUserResult result = await sender.Send(command, cancellationToken);
            logger.LogInformation($"New User : [{result.Id}] Created Successfuly!");
            return Result.Success(result.Id);
        }
    }
}
