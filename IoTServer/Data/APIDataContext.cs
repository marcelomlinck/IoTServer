using IoTServer.Data.Management;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace IoTServer.Data
{
	public class ApiDataContext : DbContext, IDbContext
	{
		public ApiDataContext() { }

		public ApiDataContext(DbContextOptions<ApiDataContext> options)
		: base(options) { }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseMySql("server=localhost;Port=3306;uid=valet;pwd=vpass;database=IoTServer;SslMode=none");
			}
		}

		public DbSet<AirConditionerDevice> AirConditionerDevices { get; set; }
		public DbSet<AirConditionerRoom> AirConditionerRooms { get; set; }
		public DbSet<AirConditionerCommand> AirConditionerCommands { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			#region AirConditionerDevice

			modelBuilder.Entity<AirConditionerDevice>(e =>
			{
				e.ToTable("air_conditioner_device");
				e.HasKey(i => i.Id);

				e.Property(t => t.Id)
					.HasMaxLength(10)
					.IsRequired();

				e.Property(t => t.SensorUniqueId)
					.HasMaxLength(10)
					.IsRequired();

				e.Property(t => t.Model)
					.HasMaxLength(50)
					//.HasColumnName("model")
					.IsRequired();

				e.Property(t => t.Temperature)
					.IsRequired();

				e.Property(t => t.Fan)
					.IsRequired();

				e.Property(t => t.Mode)
					.IsRequired();

				e.Property(t => t.LastUpdated)
					.IsRequired();
			});

			#endregion

			#region AirConditionerRoom

			modelBuilder.Entity<AirConditionerRoom>(e =>
			{
				e.ToTable("air_conditioner_room");
				e.HasKey(t => new { t.Id, t.AirConditionerDeviceId });
				e.HasIndex(t => t.Name );

				e.Property(t => t.Id)
					.HasMaxLength(10)
					.IsRequired();

				e.Property(t => t.AirConditionerDeviceId)
					.HasMaxLength(10)
					.IsRequired();

				e.Property(t => t.Name)
					.HasMaxLength(50)
					.IsRequired();
			});

			#endregion

			#region AirConditionerCommand

			modelBuilder.Entity<AirConditionerCommand>(e =>
			{
				e.ToTable("air_conditioner_command");
				e.HasKey(t => t.Id);
				e.HasIndex(t => new { t.Model, t.Temperature, t.Fan, t.Mode });

				e.Property(t => t.Id)
					.IsRequired()
					.HasMaxLength(20);

				e.Property(t => t.Model)
					.IsRequired()
					.HasMaxLength(50);

				e.Property(t => t.Temperature)
					.IsRequired();

				e.Property(t => t.Fan)
					.IsRequired();

				e.Property(t => t.Mode)
					.IsRequired();

				e.Property(t => t.RawData)
					.IsRequired();
			});

			#endregion

			#region Sensor

			modelBuilder.Entity<Sensor>(e =>
			{
				e.ToTable("sensor");
				e.HasKey(t => t.UniqueId);

				e.Property(t => t.UniqueId)
					.HasMaxLength(10)
					.IsRequired();

				e.Property(t => t.HardwareModel)
					.IsRequired();

				e.Property(t => t.Description);
			});

			#endregion
		}

		public void Migrate()
		{
			this.Database.Migrate();
		}

		public IEnumerable<string> GetAppliedMigrations()
		{
			return this.Database.GetAppliedMigrations();
		}
	}
}