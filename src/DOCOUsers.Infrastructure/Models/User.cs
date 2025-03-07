using BuildingBlocks.Abstractions.Primitives;

namespace DOCOUsers.Infrastructure.Models
{
    public class User() : Entity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public string Email { get; set; } = default!;
    }
}
