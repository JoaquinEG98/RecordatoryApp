using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private RecordatorioAppContext _context;
        private DbSet<TEntity> _dbSet;

        public Repository(RecordatorioAppContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public TEntity Add(TEntity data)
        {
            if (data == null) throw new Exception("El objeto recibido es nulo.");

            _dbSet.Add(data);
            return data;
        }

        public async Task<TEntity> AddAsync(TEntity data)
        {
            if (data == null) throw new Exception("El objeto recibido es nulo.");

            await _dbSet.AddAsync(data);
            return data;
        }

        public TEntity Delete(int id)
        {
            TEntity data = _dbSet.Find(id)!;
            _dbSet.Remove(data!);
            return data;
        }

        public TEntity Get(int id)
        {
            return _dbSet.Find(id)!;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public TEntity Update(TEntity data)
        {
            if (data == null) throw new Exception("El objeto recibido es nulo.");

            _dbSet.Attach(data);
            _context.Entry(data).State = EntityState.Modified;
            return data;
        }
    }
}
