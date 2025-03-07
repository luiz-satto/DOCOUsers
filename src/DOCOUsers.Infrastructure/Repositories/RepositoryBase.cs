using BuildingBlocks.Abstractions;
using BuildingBlocks.Abstractions.Primitives;
using DOCOUsers.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DOCOUsers.Infrastructure.Repositories
{
    public abstract class RepositoryBase<TEntity>(AppDbContext dbContext) : IEntityRepository<TEntity>
        where TEntity : Entity
    {
        private readonly AppDbContext _dbContext = dbContext;

        public Guid Insert(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            var result = _dbContext.Set<TEntity>().Add(entity);
            return result.Entity.Id;
        }

        public async Task<Guid> InsertAsync(TEntity entity, CancellationToken cancellationToken)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            var result = await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
            return result.Entity.Id;
        }

        public TEntity Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return _dbContext.Update(entity).Entity;
        }

        public bool Delete(Guid entityId)
        {
            TEntity? entity = _dbContext.Find<TEntity>(entityId);
            if (entity is null)
            {
                return false;
            }

            _dbContext.Entry(entity).State = EntityState.Deleted;
            _dbContext.Remove(entity);
            return true;
        }
    }
}
