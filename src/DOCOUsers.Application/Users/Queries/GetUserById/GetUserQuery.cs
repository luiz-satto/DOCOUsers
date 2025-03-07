using BuildingBlocks.CQRS;
using DOCOUsers.Application.Dtos;

namespace DOCOUsers.Application.Users.Queries.GetUser
{
    public record GetUserQuery(Guid Id) : IQuery<GetUserResult>;
    public record GetUserResult(UserDto User);
}
