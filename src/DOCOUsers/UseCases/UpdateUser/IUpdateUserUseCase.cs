using BuildingBlocks.Abstractions.Primitives;
using DOCOUsers.Application.Dtos;

namespace DOCOUsers.UseCases.UpdateUser
{
    public record UpdateUserRequest(UserDto User);
    public record UpdateUserResponse(UserDto User);
    public interface IUpdateUserUseCase
    {
        Task<Result<UserDto>> UpdateUserAsync(UpdateUserRequest request, CancellationToken cancellationToken);
    }
}
