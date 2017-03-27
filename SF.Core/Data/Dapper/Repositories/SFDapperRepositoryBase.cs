﻿using SF.Entitys.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SF.Core.Data.Dapper.Repositories
{
    /// <summary>
    ///     Base class to implement <see cref="IDapperRepository{TEntity,TPrimaryKey}" />.
    ///     It implements some methods in most simple way.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TPrimaryKey">The type of the primary key.</typeparam>
    /// <seealso cref="IDapperRepository{TEntity,TPrimaryKey}" />
    public abstract class SFDapperRepositoryBase<TEntity, TPrimaryKey> : IDapperRepository<TEntity, TPrimaryKey> where TEntity : BaseEntity<TPrimaryKey>
    {
        public abstract TEntity Get(TPrimaryKey id);

        public abstract IEnumerable<TEntity> GetList();

        public abstract IEnumerable<TEntity> GetList(object predicate);

        public virtual Task<TEntity> GetAsync(TPrimaryKey id)
        {
            return Task.FromResult(Get(id));
        }

        public virtual Task<IEnumerable<TEntity>> GetListAsync()
        {
            return Task.FromResult(GetList());
        }

        public virtual Task<IEnumerable<TEntity>> GetListAsync(object predicate)
        {
            return Task.FromResult(GetList(predicate));
        }

        public abstract int Count(object predicate);

        public virtual Task<int> CountAsync(object predicate)
        {
            return Task.FromResult(Count(predicate));
        }

        public abstract IEnumerable<TEntity> Query(string query, object parameters);

        public virtual Task<IEnumerable<TEntity>> QueryAsync(string query, object parameters)
        {
            return Task.FromResult(Query(query, parameters));
        }

        public abstract IEnumerable<TAny> Query<TAny>(string query, object parameters) where TAny : class;

        public virtual Task<IEnumerable<TAny>> QueryAsync<TAny>(string query, object parameters) where TAny : class
        {
            return Task.FromResult(Query<TAny>(query, parameters));
        }

        public abstract IEnumerable<TAny> Query<TAny>(string query) where TAny : class;

        public virtual Task<IEnumerable<TAny>> QueryAsync<TAny>(string query) where TAny : class
        {
            return Task.FromResult(Query<TAny>(query));
        }

        public abstract IEnumerable<TEntity> GetSet(object predicate, int firstResult, int maxResults, string sortingProperty, bool ascending = true);

        public virtual Task<IEnumerable<TEntity>> GetSetAsync(object predicate, int firstResult, int maxResults, string sortingProperty, bool ascending = true)
        {
            return Task.FromResult(GetSet(predicate, firstResult, maxResults, sortingProperty, ascending));
        }

        public virtual Task<IEnumerable<TEntity>> GetListPagedAsync(object predicate, int pageNumber, int itemsPerPage, string sortingProperty, bool ascending = true)
        {
            return Task.FromResult(GetListPaged(predicate, pageNumber, itemsPerPage, sortingProperty, ascending));
        }

        public abstract IEnumerable<TEntity> GetListPaged(object predicate, int pageNumber, int itemsPerPage, string sortingProperty, bool ascending = true);

        public abstract IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);

        public virtual Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(GetList(predicate));
        }

        public virtual Task<IEnumerable<TEntity>> GetListPagedAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, string sortingProperty, bool ascending = true)
        {
            return Task.FromResult(GetListPaged(predicate, pageNumber, itemsPerPage, sortingProperty, ascending));
        }

        public abstract IEnumerable<TEntity> GetListPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, string sortingProperty, bool ascending = true);

        public abstract int Count(Expression<Func<TEntity, bool>> predicate);

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Count(predicate));
        }

        public abstract IEnumerable<TEntity> GetSet(Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, string sortingProperty, bool ascending = true);

        public virtual Task<IEnumerable<TEntity>> GetSetAsync(Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, string sortingProperty, bool ascending = true)
        {
            return Task.FromResult(GetSet(predicate, firstResult, maxResults, sortingProperty, ascending));
        }

        public virtual Task<IEnumerable<TEntity>> GetListPagedAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, bool ascending = true, params Expression<Func<TEntity, object>>[] sortingExpression)
        {
            return Task.FromResult(GetListPaged(predicate, pageNumber, itemsPerPage, ascending, sortingExpression));
        }

        public abstract IEnumerable<TEntity> GetListPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, bool ascending = true, params Expression<Func<TEntity, object>>[] sortingExpression);

        public abstract IEnumerable<TEntity> GetSet(Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, bool ascending = true, params Expression<Func<TEntity, object>>[] sortingExpression);

        public virtual Task<IEnumerable<TEntity>> GetSetAsync(Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, bool ascending = true, params Expression<Func<TEntity, object>>[] sortingExpression)
        {
            return Task.FromResult(GetSet(predicate, firstResult, maxResults, ascending, sortingExpression));
        }

        public abstract void Insert(TEntity entity);

        public virtual Task InsertAsync(TEntity entity)
        {
            Insert(entity);
            return Task.FromResult(0);
        }

        public abstract void Update(TEntity entity);

        public virtual Task UpdateAsync(TEntity entity)
        {
            Update(entity);
            return Task.FromResult(0);
        }

        public abstract void Delete(TEntity entity);

        public virtual Task DeleteAsync(TEntity entity)
        {
            Delete(entity);
            return Task.FromResult(0);
        }

        public abstract void Delete(Expression<Func<TEntity, bool>> predicate);

        public virtual Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            Delete(predicate);
            return Task.FromResult(0);
        }

        public abstract TPrimaryKey InsertAndGetId(TEntity entity);

        public virtual Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            return Task.FromResult(InsertAndGetId(entity));
        }
    }
}
