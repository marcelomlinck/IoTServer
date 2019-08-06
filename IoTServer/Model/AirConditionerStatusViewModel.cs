using System.ComponentModel.DataAnnotations;

namespace IoTServer.Model
{
	public class AirConditionerStatusViewModel
	{
		public int DeviceId { get; set; }
		[Required, Range(18,30)]
		public int Temperature { get; set; }
		[Required, Range(1, 4)]
		public int Fan { get; set; }
		[Required]
		public string Mode { get; set; }
	}
}