using BuildingBlocks.Abstractions.Primitives;

namespace BuildingBlocks.Abstractions
{
    public interface IEntityRepository<TEntity>
        where TEntity : Entity
    {
        Guid Insert(TEntity entity);
        Task<Guid> InsertAsync(TEntity entity, CancellationToken cancellationToken);
        TEntity Update(TEntity entity);
        bool Delete(Guid id);
    }
}
