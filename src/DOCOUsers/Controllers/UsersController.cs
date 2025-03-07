using BuildingBlocks.Pagination;
using DOCOUsers.Application.Dtos;
using DOCOUsers.UseCases.CreateUser;
using DOCOUsers.UseCases.DeleteUser;
using DOCOUsers.UseCases.GetUser;
using DOCOUsers.UseCases.UpdateUser;
using Microsoft.AspNetCore.Mvc;

namespace DOCOUsers.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public sealed class UsersController(
        IGetUserUseCase getUserUseCase,
        ICreateUserUseCase createUserUseCase,
        IUpdateUserUseCase updateUserUseCase,
        IDeleteUserUseCase deleteUserUseCase
    ) : ControllerBase
    {
        [HttpGet(Name = "GetUser")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<UserDto> GetUserAsync([FromQuery] GetUserRequest request, CancellationToken cancellation)
        {
            var result = await getUserUseCase.GetUserAsync(request, cancellation);
            return result.IsSuccess ? result.Value : default!;
        }

        [HttpGet(Name = "GetUserList")]
        [ProducesResponseType(typeof(PaginatedResult<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<PaginatedResult<UserDto>> GetUserListAsync([FromQuery] PaginationRequest request, CancellationToken cancellation)
        {
            var result = await getUserUseCase.GetUserListAsync(request, cancellation);
            return result.IsSuccess ? result.Value : default!;
        }

        [HttpPost(Name = "CreateUser")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<Guid> CreateUserAsync(CreateUserRequest request, CancellationToken cancellation)
        {
            var result = await createUserUseCase.CreateUserAsync(request, cancellation);
            return result.IsSuccess ? result.Value : default!;
        }

        [HttpPatch(Name = "UpdateUser")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<UserDto> UpdateUserAsync(UpdateUserRequest request, CancellationToken cancellation)
        {
            var result = await updateUserUseCase.UpdateUserAsync(request, cancellation);
            return result.IsSuccess ? result.Value : default!;
        }

        [HttpDelete(Name = "DeleteUser")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<bool> DeleteUserAsync(DeleteUserRequest request, CancellationToken cancellation)
        {
            var result = await deleteUserUseCase.DeleteUserAsync(request, cancellation);
            return result.IsSuccess ? result.Value : default!;
        }
    }
}
