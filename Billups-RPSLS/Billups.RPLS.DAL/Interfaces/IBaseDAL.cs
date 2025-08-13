using System.Linq.Expressions;

namespace Billups.RPSLS.DAL.Interfaces;

public interface IBaseDAL<T> where T : class
{
    T GetById(int id);
    List<T> GetAll();
    IQueryable<T> GetFor(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
    void Insert(T entity);
    void Insert(List<T> entities);
    void Update(T entity);
    void Update(List<T> entities);
    Task<T> GetByIdAsync(int id);
}
