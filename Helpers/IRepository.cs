using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        TEntity Get(int id);
        Task<TEntity> GetAsync(int id);
        TEntity Add(TEntity data);
        Task<TEntity> AddAsync(TEntity data);
        TEntity Update(TEntity data);
        TEntity Delete(int id);
        void Save();
    }
}
