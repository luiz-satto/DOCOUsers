using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.Pagination;
using DOCOUsers.Application.Dtos;

namespace DOCOUsers.UseCases.GetUser
{
    public record GetUsersRequest();
    public record GetUserRequest(Guid Id);
    public record GetUserResponse(UserDto User);    
    public record GetUsersResponse(PaginatedResult<UserDto> Users);

    public interface IGetUserUseCase
    {
        Task<Result<UserDto>> GetUserAsync(GetUserRequest request, CancellationToken cancellationToken);
        Task<Result<PaginatedResult<UserDto>>> GetUserListAsync(PaginationRequest request, CancellationToken cancellationToken);
    }
}
