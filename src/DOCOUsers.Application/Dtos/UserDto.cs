namespace DOCOUsers.Application.Dtos
{
    public record UserDto(
        Guid Id,
        string FirstName,
        string LastName,
        string UserName,
        string Password,
        string Email);
}
