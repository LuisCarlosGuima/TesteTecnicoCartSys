using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Domain.Contracts.BaseRepository;
public interface IBaseRepository<TEntity> : IDisposable where TEntity : BaseEntity
{
    TEntity Add(TEntity entity);
    IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
    Task<TEntity> AddAsync(TEntity entity);
    TEntity GetById(int Id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
    Task<TEntity> GetByIdAsync(int Id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
    IQueryable<TEntity> GetAll();
    Task<IEnumerable<TEntity>> GetAllAsync();
    TEntity Update(TEntity entity);
    int SaveChanges();
    Task<int> SaveChangesAsync();
    IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> activitiesToFinish);
    Task<TEntity> UpdateAsync(TEntity entity);
    bool Remove(TEntity entity);
    Task<bool> RemoveAsync(TEntity entity);
    Task<bool> RemoveRangeAsync(IEnumerable<TEntity> entities);
    bool RemoveRange(IEnumerable<TEntity> entities);
}