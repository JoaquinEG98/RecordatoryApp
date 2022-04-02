using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public interface IUnitOfWork
    {
        public IRepository<User> Users { get; }
        public IRepository<Note> Notes { get; }
        public IRepository<List> Lists { get; }

        public void Save();
        public Task SaveAsync();    
    }
}
