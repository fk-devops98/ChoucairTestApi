using ChoucairTest.Domain.Entities.Base;
using ChoucairTest.Domain.Ports.Base;
using ChoucairTest.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ChoucairTest.Infrastructure.Adapters.Base
{
    public class GenericRepository<E> : IGenericRepository<E> where E : DomainEntity
    {
        private readonly PersistenceContext _context;

        public GenericRepository(PersistenceContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<E> AddAsync(E entity)
        {
            _ = entity ?? throw new ArgumentNullException(nameof(entity), "Entity can not be null");
            _context.Set<E>().Add(entity);
            await CommitAsync();
            return entity;
        }

        public async Task<E> GetByIdAsync(object id)
        {
            return await _context.Set<E>().FindAsync(id);
        }

        public async Task<E> UpdateAsync(E entity)
        {
            _context.Set<E>().Update(entity);
            await this.CommitAsync();
            return entity;
        }

        public async Task DeleteAsync(E entity)
        {
            if (entity != null)
            {
                _context.Set<E>().Remove(entity);
                await CommitAsync();
            }
        }

        public async Task<IEnumerable<E>> GetAsync(
            Expression<Func<E, bool>>? filter = null,
            Func<IQueryable<E>, IOrderedQueryable<E>>? orderBy = null,
            string includeStringProperties = "",
            bool isTracking = false)
        {
            IQueryable<E> query = _context.Set<E>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeStringProperties))
            {
                foreach (var includeProperty in includeStringProperties.Split
                    (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync().ConfigureAwait(false);
            }

            return !isTracking ? await query.AsNoTracking().ToListAsync() : await query.ToListAsync();
        }

        public async Task CommitAsync()
        {
            _context.ChangeTracker.DetectChanges();
            await _context.CommitAsync().ConfigureAwait(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}