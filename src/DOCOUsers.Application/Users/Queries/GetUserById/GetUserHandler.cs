using BuildingBlocks.CQRS;
using DOCOUsers.Application.Dtos;
using DOCOUsers.Application.Exceptions;
using DOCOUsers.Infrastructure.Models;
using DOCOUsers.Infrastructure.Repositories.User;

namespace DOCOUsers.Application.Users.Queries.GetUser
{
    public class GetUserHandler(IUserRepository userRepository)
        : IQueryHandler<GetUserQuery, GetUserResult>
    {
        public async Task<GetUserResult> Handle(GetUserQuery query, CancellationToken cancellationToken)
        {
            User? result = await userRepository.GetAsync(query.Id, cancellationToken);
            if (result == null)
            {
                throw new UserNotFoundException(query.Id);
            }

            UserDto user = new(
                result.Id,
                result.UserName,
                result.FirstName,
                result.LastName,
                result.Email,
                result.Password);

            return new GetUserResult(user);
        }
    }
}
