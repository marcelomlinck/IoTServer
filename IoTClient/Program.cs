using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace IoTClient
{
	class Program
	{
		public static void ReceiveTest()
		{
			// create the client
			var client = new MqttClient("localhost");

			// register a callback-function (we have to implement, see below) which is called by the library when a message was received
			client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

			client.Connect(Guid.NewGuid().ToString());

			if (!client.IsConnected)
			{
				return;
			}

			// add a subscription
			client.Subscribe(new string[] { "TestChannel" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

			while (true)
			{
				Thread.Sleep(60000);
			}
		}

		public static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
		{
			string ReceivedMessage = Encoding.UTF8.GetString(e.Message);

			Console.WriteLine(ReceivedMessage);
		}

		static void Main(string[] args)
		{
			Console.WriteLine("Starting client!");
			ReceiveTest();
		}
	}
}