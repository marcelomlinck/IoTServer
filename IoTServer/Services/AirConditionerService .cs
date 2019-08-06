using AutoMapper;
using IoTServer.Data;
using IoTServer.Data.Management;
using IoTServer.Handlers;
using IoTServer.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace IoTServer.Services
{
	public class AirConditionerService : IAirConditionerService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public AirConditionerService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public void RunCommand(int deviceId, AirConditionerStatusViewModel model)
		{
			try
			{
				var acDeviceRep = _unitOfWork.GetRepository<AirConditionerDevice>();

				var sensorUniqueId = acDeviceRep.FindBy(r => r.Id == deviceId).Select(r => r.SensorUniqueId).FirstOrDefault();

				if (!string.IsNullOrEmpty(sensorUniqueId))
				{
					throw new Exception("Device not found");
				}

				var acCommandRep = _unitOfWork.GetRepository<AirConditionerCommand>();

				var acCommand = acCommandRep.FindBy(r =>
					r.Temperature == model.Temperature &&
					r.Mode.Equals(model.Mode) &&
					r.Fan == model.Fan)
					.FirstOrDefault();

				if (acCommand == null)
				{
					throw new Exception("Command not found");
				}

				SendCommand(sensorUniqueId, acCommand);
			}
			catch (Exception e)
			{
				Log.Fatal(e.Message);
				//throw e;
			}
		}

		private void SendCommand(string sensorUniqueId, AirConditionerCommand acCommand)
		{
			try
			{
				var repository = _unitOfWork.GetRepository<AirConditionerDevice>();
				var mqttHandlerInstance = MQTTHandler.Instance;
				mqttHandlerInstance.Publish(sensorUniqueId, acCommand.RawData);
			}
			catch (Exception e)
			{
				Log.Fatal(e, "Error while sending MQTT data");
			}
		}

		public AirConditionerStatusViewModel GetDeviceStatus(int deviceId)
		{
			try
			{
				var acDeviceRep = _unitOfWork.GetRepository<AirConditionerDevice>();

				var acDeviceStatus = acDeviceRep.Get(deviceId);

				if (acDeviceStatus == null)
				{
					throw new Exception("Device not found");
				}

				return _mapper.Map<AirConditionerStatusViewModel>(acDeviceStatus);
			}
			catch (Exception e)
			{
				Log.Fatal(e.Message);
				//throw e;
				return null;
			}
		}
	}
}
