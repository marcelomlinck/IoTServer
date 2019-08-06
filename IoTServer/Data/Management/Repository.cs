﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IoTServer.Data.Management
{
	public class Repository<T> : IRepository<T>
			where T : class
	{
		/// <summary>
		/// EF data base context
		/// </summary>
		private readonly IDbContext _context;

		/// <summary>
		/// Used to query and save instances of
		/// </summary>
		private readonly DbSet<T> dbSet;

		/// <summary>
		/// Initializes a new instance of the <see cref="Repository{T}"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public Repository(IDbContext context)
		{
			_context = context;

			dbSet = context.Set<T>();
		}

		/// <inheritdoc />
		public virtual EntityState Add(T entity)
		{
			return dbSet.Add(entity).State;
		}

		/// <inheritdoc />
		public T Get<TKey>(TKey id)
		{
			return dbSet.Find(id);
		}

		/// <inheritdoc />
		public async Task<T> GetAsync<TKey>(TKey id)
		{
			return await dbSet.FindAsync(id);
		}

		/// <inheritdoc />
		public T Get(params object[] keyValues)
		{
			return dbSet.Find(keyValues);
		}

		/// <inheritdoc />
		public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
		{
			return dbSet.Where(predicate);
		}

		/// <inheritdoc />
		public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, string include)
		{
			return this.FindBy(predicate).Include(include);
		}

		/// <inheritdoc />
		public IQueryable<T> GetAll()
		{
			return dbSet;
		}

		/// <inheritdoc />
		public IQueryable<T> GetAll(int page, int pageCount)
		{
			var pageSize = (page - 1) * pageCount;

			return dbSet.Skip(pageSize).Take(pageCount);
		}

		/// <inheritdoc />
		public IQueryable<T> GetAll(string include)
		{
			return dbSet.Include(include);
		}

		/// <inheritdoc />
		public IQueryable<T> FromSql(string query, params object[] parameters)
		{
			return dbSet.FromSql(query, parameters);
		}

		/// <inheritdoc />
		public IQueryable<T> GetAll(string include, string include2)
		{
			return dbSet.Include(include).Include(include2);
		}

		/// <inheritdoc />
		public bool Exists(Expression<Func<T, bool>> predicate)
		{
			return dbSet.Any(predicate);
		}

		/// <inheritdoc />
		public IQueryable<T> FromSql(string sqlQuery)
		{
			return dbSet.FromSql(sqlQuery);
		}

		/// <inheritdoc />
		public EntityState Delete(T entity)
		{
			return dbSet.Remove(entity).State;
		}

		/// <inheritdoc />
		public virtual EntityState Update(T entity)
		{
			return dbSet.Update(entity).State;
		}
	}
}