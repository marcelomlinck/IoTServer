namespace IoTServer.Handlers
{
	public interface IMQTTHandler
	{
		void Publish(string topic, string message);
	}
}