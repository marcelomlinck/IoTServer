namespace IoTServer.Data.Management
{
	public interface IContextFactory
	{
		IDbContext DbContext { get; }
	}
}