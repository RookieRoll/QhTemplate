using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationCore
{
    public abstract class BaseManager<T> where T :class
    {
        protected readonly EmsDBContext _db;
        private readonly DbSet<T> _dbSet;
        protected BaseManager(EmsDBContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public IEnumerable<T> Finds()
        {
            return _dbSet;
        }

        public IEnumerable<T> Finds(Func<T, bool> func)
        {
            return Finds().Where(func);
        }

        public T First(Func<T, bool> func)
        {
            return Finds().First();
        }

        public T FirstOrDefault(Func<T, bool> func)
        {
            return Finds().FirstOrDefault(func);
        }
        protected void Save()
        {
            _db.SaveChanges();
        }

    }
}