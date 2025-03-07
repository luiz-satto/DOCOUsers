using BuildingBlocks.Exceptions;
using DOCOUsers.Application.Dtos;
using DOCOUsers.Application.Exceptions;
using DOCOUsers.Application.Users.Commands.UpdateUser;
using DOCOUsers.Infrastructure.Models;
using DOCOUsers.Infrastructure.Repositories.User;
using FluentAssertions;
using static FluentAssertions.FluentActions;
using Moq;

namespace DOCOUsers.Tests.Commands
{
    public class UpdateUserCommandTests
    {
        public Mock<IUserRepository> _userRepositoryMock { get; set; }

        public UpdateUserCommandTests()
        {
            _userRepositoryMock = new();
        }

        //[Fact]
        //public async void Handle_Should_ReturnUpdatedUser()
        //{
        //    // Arrange
        //    User user = new()
        //    {
        //        Id = Guid.NewGuid(),
        //        FirstName = "Juca",
        //        LastName = "Jarbas",
        //        UserName = "jucajarbas",
        //        Password = "qwerty123456",
        //        Email = "jucajarbas@email.com"
        //    };

        //    _userRepositoryMock
        //        .Setup(x => x.UpdateAsync(It.IsAny<User>(), default))
        //        .ReturnsAsync(user);

        //    var userDto = new UserDto(user.Id, user.FirstName, user.LastName, user.UserName, user.Password, user.Email);
        //    var command = new UpdateUserCommand(userDto);
        //    var handler = new UpdateUserHandler(_userRepositoryMock.Object);

        //    // Act
        //    var result = await handler.Handle(command, default);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.IsType<UpdateUserResult>(result);
        //    _userRepositoryMock.Verify(
        //        x => x.UpdateAsync(It.IsAny<User>(), default),
        //        Times.Once);
        //}

        [Fact]
        public async void Handle_Should_ThrowUserNotFoundException_WhenUserIdDoesNotExists()
        {
            // Arrange
            User user = new()
            {
                FirstName = "Juca",
                LastName = "Jarbas",
                UserName = "jucajarbas",
                Password = "qwerty123456",
                Email = "jucajarbas@email.com"
            };

            _userRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<User>(), default))
                .ReturnsAsync(user);

            var userDto = new UserDto(user.Id, user.FirstName, user.LastName, user.UserName, user.Password, user.Email);
            var command = new UpdateUserCommand(userDto);
            var handler = new UpdateUserHandler(_userRepositoryMock.Object);

            // Act
            var action = handler.Handle(command, default);

            // Assert
            await Invoking(() => action)
                .Should()
                .ThrowAsync<UserNotFoundException>()
                .WithMessage("Entity \"User\" (00000000-0000-0000-0000-000000000000) was not found.");
        }

        [Fact]
        public async void Handle_Should_ThrowBadRequestException_WhenFirstNameIsNullOrEmpty()
        {
            // Arrange
            var userDto = new UserDto(Guid.NewGuid(), "", "Jarbas", "jucajarbas", "qwerty123456", "juca@email.com");
            var command = new UpdateUserCommand(userDto);
            var handler = new UpdateUserHandler(_userRepositoryMock.Object);

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
            var userDto = new UserDto(Guid.NewGuid(), "Juca", "", "jucajarbas", "qwerty123456", "juca@email.com");
            var command = new UpdateUserCommand(userDto);
            var handler = new UpdateUserHandler(_userRepositoryMock.Object);

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
            var userDto = new UserDto(Guid.NewGuid(), "Juca", "Jarbas", "", "qwerty123456", "juca@email.com");
            var command = new UpdateUserCommand(userDto);
            var handler = new UpdateUserHandler(_userRepositoryMock.Object);

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
            var userDto = new UserDto(Guid.NewGuid(), "Juca", "Jarbas", "jucajarbas", "", "juca@email.com");
            var command = new UpdateUserCommand(userDto);
            var handler = new UpdateUserHandler(_userRepositoryMock.Object);

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
            var userDto = new UserDto(Guid.NewGuid(), "Juca", "Jarbas", "jucajarbas", "qwerty123456", "email");
            var command = new UpdateUserCommand(userDto);
            var handler = new UpdateUserHandler(_userRepositoryMock.Object);

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
