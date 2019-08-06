using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IoTServer.Data
{
	[Table("air_conditioner_room")]
	public class AirConditionerRoom
	{
		public int Id { get; set; }
		public int AirConditionerDeviceId { get; set; }
		public string Name { get; set; }
	}
}
