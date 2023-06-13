using System.Linq.Expressions;

namespace Infraestructura.Contratos
{
    public interface IRepositoryAsync<T> : IDisposable where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetByID(decimal? id);
        Task<T> Insert(T entity);
        Task<T> Delete(decimal id);
        Task<T> Update(T entity);
        Task<T> Find(Expression<Func<T, bool>> expr);
    }
}
