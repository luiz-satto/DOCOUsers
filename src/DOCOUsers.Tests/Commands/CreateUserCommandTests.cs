using BuildingBlocks.Exceptions;
using DOCOUsers.Application.Dtos;
using DOCOUsers.Application.Users.Commands.CreateUser;
using DOCOUsers.Infrastructure.Repositories.User;
using FluentAssertions;
using static FluentAssertions.FluentActions;
using Moq;
using DOCOUsers.Infrastructure.Models;

namespace DOCOUsers.Tests.Commands
{
    public class CreateUserCommandTests
    {
        public Mock<IUserRepository> _userRepositoryMock { get; set; }

        public CreateUserCommandTests()
        {
            _userRepositoryMock = new();
        }

        [Fact]
        public async void Handle_Should_ReturnNewUser()
        {
            // Arrange
            Guid userId = default;
            _userRepositoryMock
                .Setup(x => x.CreateAsync(It.IsAny<User>(), default))
                .ReturnsAsync(userId);

            var userDto = new UserDto(userId, "Juca", "Jarbas", "jucajarbas", "qwerty123456", "juca@email.com");
            var command = new CreateUserCommand(userDto);
            var handler = new CreateUserHandler(_userRepositoryMock.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CreateUserResult>(result);
            _userRepositoryMock.Verify(
                x => x.CreateAsync(It.IsAny<User>(), default),
                Times.Once);
        }

        [Fact]
        public async void Handle_Should_ThrowBadRequestException_WhenFirstNameIsNullOrEmpty()
        {
            // Arrange
            var user = new UserDto(default, "", "Jarbas", "jucajarbas", "qwerty123456", "juca@email.com");
            var command = new CreateUserCommand(user);
            var handler = new CreateUserHandler(_userRepositoryMock.Object);

            // Act
            var action = handler.Handle(command, default);

            // Assert
            await Invoking(() => action)
                .Should()
                .ThrowAsync<BadRequestException>()
                .WithMessage("First Name is required.");
        }

        [Fact]
        public async void Handle_Should_ThrowBadRequestException_WhenLastNameIsNullOrEmpty()
        {
            // Arrange
            var user = new UserDto(default, "Juca", "", "jucajarbas", "qwerty123456", "juca@email.com");
            var command = new CreateUserCommand(user);
            var handler = new CreateUserHandler(_userRepositoryMock.Object);

            // Act
            var action = handler.Handle(command, default);

            // Assert
            await Invoking(() => action)
                .Should()
                .ThrowAsync<BadRequestException>()
                .WithMessage("Last Name is required.");
        }

        [Fact]
        public async void Handle_Should_ThrowBadRequestException_WhenUserNameIsNullOrEmpty()
        {
            // Arrange
            var user = new UserDto(default, "Juca", "Jarbas", "", "qwerty123456", "juca@email.com");
            var command = new CreateUserCommand(user);
            var handler = new CreateUserHandler(_userRepositoryMock.Object);

            // Act
            var action = handler.Handle(command, default);

            // Assert
            await Invoking(() => action)
                .Should()
                .ThrowAsync<BadRequestException>()
                .WithMessage("User Name is required.");
        }

        [Fact]
        public async void Handle_Should_ThrowBadRequestException_WhenPasswordIsNullOrEmpty()
        {
            // Arrange
            var user = new UserDto(default, "Juca", "Jarbas", "jucajarbas", "", "juca@email.com");
            var command = new CreateUserCommand(user);
            var handler = new CreateUserHandler(_userRepositoryMock.Object);

            // Act
            var action = handler.Handle(command, default);

            // Assert
            await Invoking(() => action)
                .Should()
                .ThrowAsync<BadRequestException>()
                .WithMessage("Password is required.");
        }

        [Fact]
        public async void Handle_Should_ThrowBadRequestException_WhenEmailIsWithIncorrectFormat()
        {
            // Arrange
            var user = new UserDto(default, "Juca", "Jarbas", "jucajarbas", "qwerty123456", "email");
            var command = new CreateUserCommand(user);
            var handler = new CreateUserHandler(_userRepositoryMock.Object);

            // Act
            var action = handler.Handle(command, default);

            // Assert
            await Invoking(() => action)
                .Should()
                .ThrowAsync<BadRequestException>()
                .WithMessage("Email is with incorrect format.");
        }
    }
}