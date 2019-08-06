using System;
using System.Collections.Generic;

namespace IoTServer.Data.Management
{
	public sealed class UnitOfWork : IUnitOfWork
	{
		private readonly IContextFactory _contextFactory;

		private readonly IDbContext _dbContext;

		/// <summary>
		/// The repositories
		/// </summary>
		private Dictionary<Type, object> _repositories;

		/// <summary>
		/// Initializes a new instance of the <see cref="UnitOfWork" /> class.
		/// </summary>
		/// <param name="contextFactory">The context factory.</param>
		public UnitOfWork(IContextFactory contextFactory)
		{
			_contextFactory = contextFactory;
			_dbContext = contextFactory.DbContext;
		}

		/// <summary>
		/// Gets the repository.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <returns></returns>
		public IRepository<TEntity> GetRepository<TEntity>()
			where TEntity : class
		{
			if (_repositories == null)
			{
				_repositories = new Dictionary<Type, object>();
			}

			var type = typeof(TEntity);
			if (!_repositories.ContainsKey(type))
			{
				_repositories[type] = new Repository<TEntity>(_dbContext);
			}

			return (IRepository<TEntity>)_repositories[type];
		}

		/// <summary>
		/// Saves all pending changes
		/// </summary>
		/// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
		public int Commit()
		{
			// Save changes with the default options
			return _dbContext.SaveChanges();
		}

		/// <summary>
		/// Disposes the current object
		/// </summary>
		public void Dispose()
		{
			this.Dispose(true);

			// ReSharper disable once GCSuppressFinalizeForTypeWithoutDestructor
			GC.SuppressFinalize(obj: this);
		}

		/// <summary>
		/// Disposes all external resources.
		/// </summary>
		/// <param name="disposing">The dispose indicator.</param>
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				_dbContext?.Dispose();
			}
		}
	}
}
