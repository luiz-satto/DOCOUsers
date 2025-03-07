using BuildingBlocks.Exceptions;

namespace DOCOUsers.Application.Exceptions
{
    public class UserNotFoundException(Guid id) : NotFoundException("User", id);
}
