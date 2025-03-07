using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.Pagination;
using DOCOUsers.Application.Dtos;
using DOCOUsers.Application.Users.Queries.GetUser;
using DOCOUsers.Application.Users.Queries.GetUserList;
using MediatR;

namespace DOCOUsers.UseCases.GetUser
{
    public class GetUserUseCase(
        ISender sender,
        ILogger<GetUserUseCase> logger
    ) : IGetUserUseCase
    {
        public async Task<Result<UserDto>> GetUserAsync(GetUserRequest request, CancellationToken cancellationToken)
        {
            GetUserQuery query = new(request.Id);
            GetUserResult result = await sender.Send(query, cancellationToken);
            logger.LogInformation($"User : [{result.User.UserName}] Retrived Successfuly!");
            return Result.Success(result.User);
        }

        public async Task<Result<PaginatedResult<UserDto>>> GetUserListAsync(PaginationRequest request, CancellationToken cancellationToken)
        {
            GetUserListResult result = await sender.Send(new GetUserListQuery(request), cancellationToken);
            logger.LogInformation($"Users Retrived Successfuly!");
            return Result.Success(result.Users);
        }
    }
}
