using IoTServer.Model;
using IoTServer.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IoTServer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AirConditionerController : ControllerBase
	{
		private readonly IAirConditionerService _airConditionerService;

		public AirConditionerController(IAirConditionerService airConditionerService)
		{
			_airConditionerService = airConditionerService;
		}

		// GET deviceId/status
		[HttpGet]
		[Route("device/{deviceId}")]
		public ActionResult Get(int deviceId)
		{
			if (deviceId <= 0)
			{
				return this.BadRequest();
			}

			var res = _airConditionerService.GetDeviceStatus(deviceId);
			return this.Ok(res);
		}

		// GET roomName/status
		[HttpGet]
		[Route("room/{roomName}")]
		public ActionResult Get(string roomName)
		{
			return this.NotFound();
		}

		// POST api/values
		[HttpPut]
		[Route("device/{deviceId}")]
		public ActionResult Put([FromRoute] int deviceId, [FromBody] AirConditionerStatusViewModel model)
		{
			if (deviceId <= 0)
			{
				return this.BadRequest();
			}

			_airConditionerService.RunCommand(deviceId, model);
			return this.Ok();
		}

		// POST api/values
		[HttpPut]
		[Route("room/{roomName}")]
		public ActionResult Put([FromRoute] string roomName, [FromBody] AirConditionerStatusViewModel model)
		{
			return this.NotFound();
		}
	}
}
