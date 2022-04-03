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
        private ScarletContext _context;
        private DbSet<TEntity> _dbSet;

        public Repository(ScarletContext context)
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
            TEntity data = _dbSet.Find(id)!;

            if (data == null) throw new Exception("No se pudo encontrar nada con ese Id.");
            else return data;
        }

        public async Task<TEntity> GetAsync(int id)
        {
            TEntity data = await _dbSet.FindAsync(id)!;

            if (data == null) throw new Exception("No se pudo encontrar nada con ese Id.");
            else return data;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
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
