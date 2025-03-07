using BuildingBlocks.Abstractions.Primitives;

namespace DOCOUsers.UseCases.DeleteUser
{
    public record DeleteUserRequest(Guid Id);
    public record DeleteUserResponse(bool IsDeleted);
    public interface IDeleteUserUseCase
    {
        Task<Result<bool>> DeleteUserAsync(DeleteUserRequest request, CancellationToken cancellationToken);
    }
}
