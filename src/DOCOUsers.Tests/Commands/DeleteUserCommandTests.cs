using DOCOUsers.Application.Exceptions;
using DOCOUsers.Application.Users.Commands.DeleteUser;
using DOCOUsers.Infrastructure.Repositories.User;
using FluentAssertions;
using Moq;
using static FluentAssertions.FluentActions;

namespace DOCOUsers.Tests.Commands
{
    public class DeleteUserCommandTests
    {
        public Mock<IUserRepository> _userRepositoryMock { get; set; }

        public DeleteUserCommandTests()
        {
            _userRepositoryMock = new();
        }

        [Fact]
        public async void Handle_Should_DeleteUserById()
        {
            // Arrange
            bool isDeleted = false;
            _userRepositoryMock
                .Setup(x => x.DeleteAsync(It.IsAny<Guid>(), default))
                .ReturnsAsync(isDeleted);

            var command = new DeleteUserCommand(Guid.NewGuid());
            var handler = new DeleteUserHandler(_userRepositoryMock.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DeleteUserResult>(result);
            _userRepositoryMock.Verify(
                x => x.DeleteAsync(It.IsAny<Guid>(), default),
                Times.Once);
        }

        //[Fact]
        //public async void Handle_Should_ThrowUserNotFoundException_WhenUserIdDoesNotExists()
        //{
        //    // Arrange
        //    bool isDeleted = false;
        //    _userRepositoryMock
        //        .Setup(x => x.DeleteAsync(It.IsAny<Guid>(), default))
        //        .ReturnsAsync(isDeleted);

        //    var command = new DeleteUserCommand(default);
        //    var handler = new DeleteUserHandler(_userRepositoryMock.Object);

        //    // Act
        //    var action = handler.Handle(command, default);

        //    // Assert
        //    await Invoking(() => action)
        //        .Should()
        //        .ThrowAsync<UserNotFoundException>()
        //        .WithMessage("Entity \"User\" (00000000-0000-0000-0000-000000000000) was not found.");
        //}
    }
}
