using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using DOCOUsers.Application.Dtos;
using DOCOUsers.Infrastructure.Repositories.User;

namespace DOCOUsers.Application.Users.Queries.GetUserList
{
    public class GetUserListHandler(IUserRepository userRepository)
        : IQueryHandler<GetUserListQuery, GetUserListResult>
    {
        public async Task<GetUserListResult> Handle(GetUserListQuery query, CancellationToken cancellationToken)
        {
            int pageIndex = query.PaginationRequest.PageIndex;
            int pageSize = query.PaginationRequest.PageSize;
            var result = await userRepository.GetAsync(pageIndex, pageSize, cancellationToken);

            List<UserDto> users = [];
            foreach (var u in result.Item1)
            {
                UserDto user = new(u.Id, u.UserName, u.FirstName, u.LastName, u.Email, u.Password);
                users.Add(user);
            }

            var paginatedResult = new PaginatedResult<UserDto>(pageIndex, pageSize, result.Item2, users);
            return new GetUserListResult(paginatedResult);
        }
    }
}
