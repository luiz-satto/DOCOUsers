using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using DOCOUsers.Application.Dtos;

namespace DOCOUsers.Application.Users.Queries.GetUserList
{
    public record GetUserListQuery(PaginationRequest PaginationRequest) : IQuery<GetUserListResult>;
    public record GetUserListResult(PaginatedResult<UserDto> Users);
}
