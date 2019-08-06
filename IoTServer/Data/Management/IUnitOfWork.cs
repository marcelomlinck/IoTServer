using System;

namespace IoTServer.Data.Management
{
	public interface IUnitOfWork : IDisposable
	{
		IRepository<TEntity> GetRepository<TEntity>()
			where TEntity : class;

		int Commit();
	}
}