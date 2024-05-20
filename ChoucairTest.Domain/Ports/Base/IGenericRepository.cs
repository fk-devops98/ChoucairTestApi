﻿using System.Linq.Expressions;

namespace ChoucairTest.Domain.Ports.Base
{
    public interface IGenericRepository<E> : IDisposable
       where E : Entities.Base.DomainEntity
    {
        Task<IEnumerable<E>> GetAsync(Expression<Func<E, bool>>? filter = null, Func<IQueryable<E>, IOrderedQueryable<E>>? orderBy = null, string includeStringProperties = "", bool isTracking = false);

        Task<E> GetByIdAsync(object id);

        Task<E> AddAsync(E entity);

        Task<E> UpdateAsync(E entity);

        Task DeleteAsync(E entity);
    }
}