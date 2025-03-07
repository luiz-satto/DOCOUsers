using DOCOUsers.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace DOCOUsers.Infrastructure.Data
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
