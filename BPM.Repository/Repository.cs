using BPM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BPM.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DataContext _dbContext;

        public Repository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual DataContext Query()
        {
            return _dbContext;
        }


        public virtual T GetById(Guid id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public virtual IEnumerable<T> List()
        {
            return _dbContext.Set<T>().AsEnumerable();
        }

        public virtual IEnumerable<T> List(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>()
                   .Where(predicate)
                   .AsEnumerable();
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
        }

        public void Edit(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public IEnumerable<T> GetPageList( int pageSize, int pageIndex,out int records)
        {
            var q = _dbContext.Set<T>();
            records = q.Count();

            return q.Skip(pageSize * (pageIndex - 1))
                            .Take(pageSize).AsEnumerable();
        }

        public IEnumerable<T> GetPageList(System.Linq.Expressions.Expression<Func<T, bool>> predicate,int pageSize,int pageIndex,out int records)
        {
            var q = _dbContext.Set<T>().Where(predicate);
            records = q.Count();

            return q.Skip(pageSize * (pageIndex - 1))
                            .Take(pageSize).AsEnumerable();
        }
    }
}
