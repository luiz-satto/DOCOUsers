namespace DOCOUsers.Infrastructure.Repositories.User
{
    public interface IUserRepository
    {
        Task<Tuple<List<Models.User>, long>> GetAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
        Task<Models.User?> GetAsync(Guid id, CancellationToken cancellationToken);
        Task<Guid> CreateAsync(Models.User user, CancellationToken cancellationToken);
        Task<Models.User> UpdateAsync(Models.User user, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Guid userId, CancellationToken cancellationToken);
    }
}
