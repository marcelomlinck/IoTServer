using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace IoTServer.Handlers
{
	public class MQTTHandler : IMQTTHandler
	{
		private MqttClient _client;
		public MQTTHandler()
		{
			if (_client == null)
			{
				try
				{
					Log.Information("MQTT: initializing handler");
					_client = new MqttClient("localhost");
					Connect();
					Log.Information($"MQTT: connection stablished : {_client.ClientId}");
				}
				catch (Exception e)
				{
					Log.Fatal(e.Message);
				}
			}
		}

		private void Connect()
		{
			try
			{
				if (_client == null)
				{
					throw new Exception("MQTT: handler not initialized for connection");
				}
				int i = 0;
				do
				{
					if (!_client.IsConnected)
					{
						Debug.WriteLine("MQTT: attempting connection...");
						_client.Connect("IoTServer");
					}
					else
					{
						break;
					}
					i++;
				} while (i < 3);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public void Publish(string topic, string message)
		{
			try
			{
				if (!_client.IsConnected)
				{
					Connect();
				}
				_client.Publish(topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
				Debug.WriteLine($"MQTT: {_client.ClientId} : {message}");
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		//private static MQTTHandler instance = new MQTTHandler();

		//public static MQTTHandler Instance => instance;
	}
}

//public static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
//{
//	string ReceivedMessage = Encoding.UTF8.GetString(e.Message);

//	Console.WriteLine(ReceivedMessage);
//}