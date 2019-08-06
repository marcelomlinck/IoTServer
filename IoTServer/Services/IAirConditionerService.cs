using IoTServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTServer.Services
{
	public interface IAirConditionerService
	{
		void RunCommand(int deviceId, AirConditionerStatusViewModel model);
		AirConditionerStatusViewModel GetDeviceStatus(int deviceId);
	}
}
