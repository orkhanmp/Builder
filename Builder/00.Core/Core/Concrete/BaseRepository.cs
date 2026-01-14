using Core.Abstract;
using Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concrete
{
    public class BaseRepository<T, TContext> : IBaseRepository<T>
        where T : BaseEntity, new()
        where TContext : DbContext, new()
    {
        public void Add(T entity)
        {
            using (TContext context = new())
            {
                var entityAdded = context.Entry(entity);
                entityAdded.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(T entity)
        {
            using (TContext context = new())
            {
                var entityDelete = context.Entry(entity);
                entityDelete.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            using (TContext context = new())
            {
                return context.Set<T>().FirstOrDefault(predicate);
            }
        }

        public List<T> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            using (TContext context = new())
            {
                return predicate is null
                    ? context.Set<T>().ToList()
                    : context.Set<T>().Where(predicate).ToList();
            }
        }

        public void Update(T entity)
        {
            using (TContext context = new())
            {
                var entityUpdate = context.Entry(entity);
                entityUpdate.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
