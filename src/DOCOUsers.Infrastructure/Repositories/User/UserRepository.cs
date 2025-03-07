using DOCOUsers.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DOCOUsers.Infrastructure.Repositories.User
{
    public sealed class UserRepository
        : RepositoryBase<Models.User>, IUserRepository
    {
        private AppDbContext _dbContext { get; init; }
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Tuple<List<Models.User>, long>> GetAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            var totalCount = await _dbContext.Users.LongCountAsync(cancellationToken);
            var result = await _dbContext.Users
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return Tuple.Create(result, totalCount);
        }

        public async Task<Models.User?> GetAsync(Guid id, CancellationToken cancellationToken)
            => await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<Guid> CreateAsync(Models.User user, CancellationToken cancellationToken)
        {
            var result = await InsertAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }

        public async Task<Models.User> UpdateAsync(Models.User user, CancellationToken cancellationToken)
        {
            var result = Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }

        public async Task<bool> DeleteAsync(Guid userId, CancellationToken cancellationToken)
        {
            var result = Delete(userId);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
