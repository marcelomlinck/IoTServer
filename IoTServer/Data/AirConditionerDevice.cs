using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

//dotnet ef migrations add 

namespace IoTServer.Data
{
	[Table("air_conditioner_device")]
	public class AirConditionerDevice
	{
		public int Id { get; set; }
		public string SensorUniqueId { get; set; }
		public string Model { get; set; }
		public int Temperature { get; set; }
		public int Fan { get; set; }
		public string Mode { get; set; }
		public string Description { get; set; }
		public DateTime LastUpdated { get; set; }
	}
}
