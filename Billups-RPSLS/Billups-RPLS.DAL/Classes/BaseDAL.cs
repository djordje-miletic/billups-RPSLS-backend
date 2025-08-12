using Billups.RPLS.DAL.Interfaces;
using Billups.RPSLS.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Billups.RPLS.DAL.Classes;

public class BaseDAL<T> : IBaseDAL<T> where T : class
{
    #region Fields

    protected readonly RPSLSDbContext _context;

    #endregion Fields

    #region Properties

    public RPSLSDbContext Context
    {
        get
        {
            return _context;
        }
    }

    #endregion

    #region Constructors

    public BaseDAL(RPSLSDbContext context)
    {
        _context = context;
    }

    #endregion Constructors

    #region Public methods

    public T GetById(int id)
    {
        return _context.Set<T>().Find(id);
    }

    List<T> IBaseDAL<T>.GetAll()
    {
        return _context.Set<T>().AsNoTracking().ToList();
    }

    public IQueryable<T> GetFor(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _context.Set<T>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includes != null)
        {
            query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        return query;
    }

    void IBaseDAL<T>.Insert(T entity)
    {

        EntityEntry<T> savedEntity;
        savedEntity = _context.Set<T>().Add(entity);
    }

    void IBaseDAL<T>.Insert(List<T> entities)
    {
        _context.Set<T>().AddRange(entities);
    }

    void IBaseDAL<T>.Update(T entity)
    {
        EntityEntry<T> savedEntity;
        savedEntity = _context.Set<T>().Update(entity);
    }

    void IBaseDAL<T>.Update(List<T> entities)
    {
        _context.Set<T>().UpdateRange(entities);
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    #endregion Public methods
}

