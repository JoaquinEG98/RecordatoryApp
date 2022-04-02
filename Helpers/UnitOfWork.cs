using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class UnitOfWork : IUnitOfWork
    {
        private RecordatorioAppContext _context;
        private IRepository<User>? _users;
        private IRepository<Note>? _notes;
        private IRepository<List>? _lists;

        public UnitOfWork(RecordatorioAppContext context)
        {
            _context = context;
        }


        public IRepository<User> Users
        {
            get
            {
                return _users == null ?
                    _users = new Repository<User>(_context) :
                    _users;
            }
        }
        public IRepository<Note> Notes
        {
            get
            {
                return _notes == null ?
                    _notes = new Repository<Note>(_context) :
                    _notes;
            }
        }
        public IRepository<List> Lists
        {
            get
            {
                return _lists == null ?
                    _lists = new Repository<List>(_context) :
                    _lists;
            }
        }


        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
