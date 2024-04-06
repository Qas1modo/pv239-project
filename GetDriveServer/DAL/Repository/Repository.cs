using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        internal GetDriveDbContext context;

        internal DbSet<TEntity> dbSet;

        public Repository(GetDriveDbContext dbcontext)
        {
            this.context = dbcontext;
            this.dbSet = context.Set<TEntity>();
        }

        public async virtual Task<TEntity?> GetByID(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual void Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new Exception("Arugment entity is null");
            }
            dbSet.Add(entity);
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new Exception("Arugment entity is null");
            }
            await dbSet.AddAsync(entity);
        }

        public virtual void Delete(int id)
        {
            TEntity? entityToDelete = dbSet.Find(id) ?? throw new Exception("Entity with given Id does not exist.");
            dbSet.Remove(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            if (entityToUpdate == null)
            {
                throw new Exception("Argument entityToUpdate is null.");
            }

            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual IQueryable<TEntity> GetQueryable()
        {
            return dbSet.AsQueryable();
        }

        public async virtual Task<IEnumerable<TEntity>> GetAll()
        {
            return await dbSet.ToListAsync();
        }
    }
}
