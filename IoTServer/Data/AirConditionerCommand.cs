using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IoTServer.Data
{
	[Table("air_conditioner_command")]
	public class AirConditionerCommand
	{
		public int Id { get; set; }
		public string Model { get; set; }
		public int Temperature { get; set; }
		public int Fan { get; set; }
		public string Mode { get; set; }
		public string RawData { get; set; }
	}
}
