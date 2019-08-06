using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTServer.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "air_conditioner_command",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 20, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Model = table.Column<int>(maxLength: 50, nullable: false),
                    Temperature = table.Column<int>(nullable: false),
                    Fan = table.Column<int>(nullable: false),
                    Mode = table.Column<int>(nullable: false),
                    RawData = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_air_conditioner_command", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "air_conditioner_device",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 10, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SensorId = table.Column<int>(maxLength: 10, nullable: false),
                    Model = table.Column<int>(maxLength: 50, nullable: false),
                    Temperature = table.Column<int>(nullable: false),
                    Fan = table.Column<int>(nullable: false),
                    Mode = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_air_conditioner_device", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "air_conditioner_room",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 10, nullable: false),
                    AirConditionerDeviceId = table.Column<int>(maxLength: 10, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_air_conditioner_room", x => new { x.Id, x.AirConditionerDeviceId });
                });

            migrationBuilder.CreateTable(
                name: "sensor",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 10, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    HardwareModel = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sensor", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_air_conditioner_command_Model_Temperature_Fan_Mode",
                table: "air_conditioner_command",
                columns: new[] { "Model", "Temperature", "Fan", "Mode" });

            migrationBuilder.CreateIndex(
                name: "IX_air_conditioner_room_Name",
                table: "air_conditioner_room",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "air_conditioner_command");

            migrationBuilder.DropTable(
                name: "air_conditioner_device");

            migrationBuilder.DropTable(
                name: "air_conditioner_room");

            migrationBuilder.DropTable(
                name: "sensor");
        }
    }
}
