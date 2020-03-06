using System;
using System.Threading.Tasks;

using WebApiEF.Db.Model;

namespace WebApiEF.Db.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class, IPOCOClass;

        int Save();
        Task<int> SaveAsync();
    }
}
