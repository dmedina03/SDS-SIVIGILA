using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SIVIGILA.Models.Context;
using SIVIGILA.Repository.Interface.BaseInterface;
using System.Linq.Expressions;

namespace SIVIGILA.Repository.BaseRepository
{
    public abstract class BaseRepository<T> : IBaseGenericRepository<T> where T: class
    {
        public context _context { get; set; }
        public DbSet<T> Entity => _context.Set<T>();
        public BaseRepository(context context)
        {
            _context = context;
        }

        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate = null, bool SaveChanges = true,
                                            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var query = Entity.AsQueryable();
            if (include != null)
                query = include(query);
            if (predicate != null)
                query = query.Where(predicate);

            IEnumerable<T> entities = query;
            foreach (var item in entities)
            {
                _context.Entry(item).State = EntityState.Deleted;
                _context.Remove(item);
            }
            if (SaveChanges)
                await SaveChangesAsync();
            return true;
        }


        public async Task<List<T>> GenericGetAllAsync(Expression<Func<T, bool>> predicate = null,
                                                    Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                    Expression<Func<T, T>> selector = null,
                                                    bool distinct = false)
        {
            IQueryable<T> query = Entity.AsNoTracking();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (include != null)
            {
                query = include(query);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (selector != null)
            {
                query = query.Select(selector);
            }
            if (distinct)
            {
                query = query.Distinct();
            }
            return await query.ToListAsync();

        }

        public async Task<U> GenericGetAsync<U>(Expression<Func<T, U>> parameter,
                                                 Expression<Func<T, bool>> searchQuery = null,
                                                 Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var query = Entity.AsNoTracking();
            if (searchQuery != null)
            {
                query = query.Where(searchQuery);
            }
            if (include != null)
                query = include(query);
            return await query.Select(parameter).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateGenericAsync(T value, bool saveChanges = true, params Expression<Func<T, object>>[] propertyExpresion)
        {
            if (propertyExpresion == null || propertyExpresion.Count() == 0)
            {
                var entity = Entity.Update(value);
                entity.State = EntityState.Modified;
            }
            else
            {
                if (_context.Entry(value).State == EntityState.Detached)
                    Entity.Attach(value);

                foreach (var column in propertyExpresion)
                {
                    _context.Entry(value).Property(column).IsModified = true;
                }
            }

            if (saveChanges)
                await SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddAsync(T entity, bool SaveChanges = true)
        {
            await Entity.AddAsync(entity);
            if (SaveChanges)
                await SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddRangeAsync(IEnumerable<T> lista, bool SaveChanges = true)
        {
            await Entity.AddRangeAsync(lista);
            if (SaveChanges)
                await SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateRangeAsync(IEnumerable<T> lista, bool SaveChanges = true)
        {
            foreach (var item in lista)
            {
                await UpdateGenericAsync(item, false);
            }
            if (SaveChanges)
                await SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExistGenericAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            if (predicate == null)
                return await Entity.AnyAsync();
            if (include != null)
            {
                return await include(Entity.Where(predicate)).AnyAsync();
            }
            return await Entity.AnyAsync(predicate);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<U> ApplyOperationAsync<U>(Func<IQueryable<T>, Task<U>> Operation,
                                                    Expression<Func<T, bool>> parameter = null,
                                                    Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var query = Entity.AsQueryable();
            if (include != null)
                query = include(query);
            if (parameter != null)
            {
                query = query.Where(parameter);
            }
            return await Operation(query);
        }

        public async Task<IEnumerable<U>> GenericGetAllValuesAsync<U>(Expression<Func<T, U>> selector,
                                                               Expression<Func<T, bool>> predicate = null,
                                                               Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                                               Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                               bool distinct = false) 
        {
            IQueryable<T> query = Entity.AsNoTracking();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (include != null)
            {
                query = include(query);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (distinct)
            {
                query = query.Distinct();
            }
            return await query.Select(selector).ToListAsync();
        }
    }
}
