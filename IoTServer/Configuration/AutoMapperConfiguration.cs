using AutoMapper;
using IoTServer.Data;
using IoTServer.Model;

namespace IoTServer.Configuration
{
	public static class AutoMapperConfiguration
	{
		public static void Configure(IMapperConfigurationExpression config)
		{
			config.CreateMap<AirConditionerDevice, AirConditionerStatusViewModel>()
				.ForMember(d => d.DeviceId, opt => opt.MapFrom(m => m.Id));
		}
	}
}