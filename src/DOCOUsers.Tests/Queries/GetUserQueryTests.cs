using DOCOUsers.Application.Exceptions;
using DOCOUsers.Application.Users.Queries.GetUser;
using DOCOUsers.Infrastructure.Models;
using DOCOUsers.Infrastructure.Repositories.User;
using FluentAssertions;
using Moq;
using static FluentAssertions.FluentActions;

namespace DOCOUsers.Tests.Queries
{
    public class GetUserQueryTests
    {
        public Mock<IUserRepository> _userRepositoryMock { get; set; }
        public GetUserQueryTests()
        {
            _userRepositoryMock = new();
        }

        [Fact]
        public async void Handle_Should_GetUserById()
        {
            // Arrange
            User user = new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Juca",
                LastName = "Jarbas",
                UserName = "jucajarbas",
                Password = "qwerty123456",
                Email = "jucajarbas@email.com"
            };

            _userRepositoryMock
                .Setup(x => x.GetAsync(It.IsAny<Guid>(), default))
                .ReturnsAsync(user);

            var command = new GetUserQuery(user.Id);
            var handler = new GetUserHandler(_userRepositoryMock.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<GetUserResult>(result);
            _userRepositoryMock.Verify(
                x => x.GetAsync(It.IsAny<Guid>(), default),
                Times.Once);
        }

        //[Fact]
        //public async void Handle_Should_ThrowUserNotFoundException_WhenUserIdDoesNotExists()
        //{
        //    // Arrange
        //    User user = new()
        //    {
        //        FirstName = "Juca",
        //        LastName = "Jarbas",
        //        UserName = "jucajarbas",
        //        Password = "qwerty123456",
        //        Email = "jucajarbas@email.com"
        //    };

        //    _userRepositoryMock
        //        .Setup(x => x.GetAsync(It.IsAny<Guid>(), default))
        //        .ReturnsAsync(user);

        //    var command = new GetUserQuery(default);
        //    var handler = new GetUserHandler(_userRepositoryMock.Object);

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
