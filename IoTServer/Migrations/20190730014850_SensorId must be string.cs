using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IoTServer.Migrations
{
    public partial class SensorIdmustbestring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_sensor",
                table: "sensor");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "sensor");

            migrationBuilder.DropColumn(
                name: "SensorId",
                table: "air_conditioner_device");

            migrationBuilder.AddColumn<string>(
                name: "UniqueId",
                table: "sensor",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SensorUniqueId",
                table: "air_conditioner_device",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sensor",
                table: "sensor",
                column: "UniqueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_sensor",
                table: "sensor");

            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "sensor");

            migrationBuilder.DropColumn(
                name: "SensorUniqueId",
                table: "air_conditioner_device");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "sensor",
                maxLength: 10,
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "SensorId",
                table: "air_conditioner_device",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_sensor",
                table: "sensor",
                column: "Id");
        }
    }
}
