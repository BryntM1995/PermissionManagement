using Microsoft.EntityFrameworkCore;
using PermissionManagement.Model.Entities;
using System;
using System.Linq;

namespace PermissionManagement.Repository
{
    public interface IBaseRepository<Entity>
    {
        void Add(Entity entity);
        void Update(Entity entity);
        bool Remove(int key);
        Entity GetById(int key);
        IQueryable<Entity> GetAll();
        public bool Commit();
    }

    public class BaseRepository<Entity> : IBaseRepository<Entity> where Entity : class, IBaseEntity
    {
        private readonly DbContext _dbContext;
        protected readonly DbSet<Entity> _dbSet;
        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Entity>();
        }
        public void Add(Entity entity)
        {
            _dbSet.Add(entity);
            Commit();
        }

        public void Update(Entity entity)
        {
            _dbSet.Update(entity);
            _dbContext.SaveChanges();
        }

        public bool Remove(int key)
        {
            var item = GetById(key);
            _dbSet.Remove(item);    
            return Commit();
        }

        public Entity GetById(int key)
        {
            return GetAll().Where(x => x.Id == key).FirstOrDefault();
        }

        public IQueryable<Entity> GetAll()
        {
            return _dbSet;
        }

        public bool Commit()
        {
            var result = _dbContext.SaveChanges() == 1;
            return result;
        }
    }
}
