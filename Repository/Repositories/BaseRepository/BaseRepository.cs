using Domain.Contracts.BaseRepository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Repository.Repositories.BaseRepository;

public abstract class BaseRepository<TEntity, Context> : IBaseRepository<TEntity>, IDisposable where TEntity : BaseEntity where Context : DbContext
{
    protected readonly Context _context;

    public BaseRepository(Context context)
    {
        _context = context;
    }

    public virtual TEntity Add(TEntity entity)
    {
        entity = AddAsync(entity).Result;
        return entity;
    }

    public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().AddRange(entities);
        _context.SaveChanges();
        return entities;
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
        _context.Entry(entity).State = EntityState.Detached;
        await _context.Entry(entity).ReloadAsync();
        return entity;
    }

    public virtual IQueryable<TEntity> GetAll()
    {
        return _context.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public virtual TEntity GetById(int Id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
    {
        return GetByIdAsync(Id, include).Result;
    }

    public virtual async Task<TEntity> GetByIdAsync(int Id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
    {
        IQueryable<TEntity> queryable = from t in _context.Set<TEntity>()
                                        where t.Id.Equals(Id)
                                        select t;
        if (include != null)
        {
            queryable = include(queryable);
        }

        return await queryable.FirstOrDefaultAsync((TEntity t) => t.Id == Id);
    }

    public virtual bool Remove(TEntity entity)
    {
        _context.Remove(entity);
        return _context.SaveChanges() > 0;
    }

    public virtual async Task<bool> RemoveAsync(TEntity entity)
    {
        _context.Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public virtual bool RemoveRange(IEnumerable<TEntity> entities) 
        => RemoveRangeAsync(entities).Result;

    public virtual async Task<bool> RemoveRangeAsync(IEnumerable<TEntity> entities)
    {
        _context.RemoveRange(entities);
        return await _context.SaveChangesAsync() > 0;
    }

    public virtual TEntity Update(TEntity entity)
    {
        entity = UpdateAsync(entity).Result;
        return GetById(entity.Id);
    }

    public int SaveChanges()
        => _context.SaveChanges();

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public virtual IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().UpdateRange(entities);
        _context.SaveChanges();
        return entities;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
        _context.Entry(entity).State = EntityState.Detached;
        await _context.Entry(entity).ReloadAsync();
        return entity;
    }

    public void Dispose()
        => _context.Dispose();
}