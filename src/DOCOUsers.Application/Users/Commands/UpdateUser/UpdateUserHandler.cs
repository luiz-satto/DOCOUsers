using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using DOCOUsers.Application.Dtos;
using DOCOUsers.Application.Exceptions;
using DOCOUsers.Infrastructure.Models;
using DOCOUsers.Infrastructure.Repositories.User;
using System.Text.RegularExpressions;

namespace DOCOUsers.Application.Users.Commands.UpdateUser
{
    public class UpdateUserHandler(IUserRepository userRepository)
        : ICommandHandler<UpdateUserCommand, UpdateUserResult>
    {
        public async Task<UpdateUserResult> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            UpdateUserCommandValidator(command);

            User? result = await userRepository.GetAsync(command.User.Id, cancellationToken);
            if (result == null)
            {
                throw new UserNotFoundException(command.User.Id);
            }

            User user = new()
            {
                Id = command.User.Id,
                UserName = command.User.UserName,
                FirstName = command.User.FirstName,
                LastName = command.User.LastName,
                Email = command.User.Email,
                Password = command.User.Password
            };

            User updatedUser = await userRepository.UpdateAsync(user, cancellationToken);
            UserDto userDto = new(
                updatedUser.Id,
                updatedUser.FirstName,
                updatedUser.LastName,
                updatedUser.UserName,
                updatedUser.Password,
                updatedUser.Email);

            return new UpdateUserResult(userDto);
        }

        private static void UpdateUserCommandValidator(UpdateUserCommand command)
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
