using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IoTServer.Data
{
	[Table("sensor")]
	public class Sensor
	{
		public string UniqueId { get; set; }
		public string HardwareModel { get; set; }
		public string Description { get; set; }
	}
}
