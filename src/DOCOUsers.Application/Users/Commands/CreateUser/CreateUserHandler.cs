using BuildingBlocks.Abstractions.Primitives;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using DOCOUsers.Infrastructure.Models;
using DOCOUsers.Infrastructure.Repositories.User;
using System.Text.RegularExpressions;

namespace DOCOUsers.Application.Users.Commands.CreateUser
{
    public class CreateUserHandler(IUserRepository userRepository)
        : ICommandHandler<CreateUserCommand, CreateUserResult>
    {
        public async Task<CreateUserResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            CreateUserCommandValidator(command);

            User user = new()
            {
                Id = Guid.NewGuid(),
                UserName = command.User.UserName,
                FirstName = command.User.FirstName,
                LastName = command.User.LastName,
                Email = command.User.Email,
                Password = command.User.Password
            };

            Result<Guid> result = await userRepository.CreateAsync(user, cancellationToken);
            return new CreateUserResult(result.Value);
        }

        private static void CreateUserCommandValidator(CreateUserCommand command)
        {
            if (string.IsNullOrEmpty(command.User.FirstName))
            {
                throw new BadRequestException("First Name is required.");
            }

            if (command.User.FirstName.Length > 50)
            {
                throw new BadRequestException("First Name maximum length is 50.");
            }

            if (string.IsNullOrEmpty(command.User.LastName))
            {
                throw new BadRequestException("Last Name is required.");
            }

            if (command.User.LastName.Length > 50)
            {
                throw new BadRequestException("Last Name maximum length is 50.");
            }

            if (string.IsNullOrEmpty(command.User.UserName))
            {
                throw new BadRequestException("User Name is required.");
            }

            if (command.User.UserName.Length > 20)
            {
                throw new BadRequestException("User Name maximum length is 20.");
            }

            if (string.IsNullOrEmpty(command.User.Password))
            {
                throw new BadRequestException("Password is required.");
            }

            if (command.User.Password.Length > 12)
            {
                throw new BadRequestException("Password maximum length is 12.");
            }

            Regex regex = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(command.User.Email);
            if (!match.Success)
            {
                throw new BadRequestException("Email is with incorrect format.");
            }

            if (command.User.Email.Length > 255)
            {
                throw new BadRequestException("Email maximum length is 255.");
            }
        }
    }
}
