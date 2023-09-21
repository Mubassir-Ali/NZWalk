using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class seedmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0fbd1bc7-f3ab-4914-a92e-b0fff04a5c24"), "Hard" },
                    { new Guid("dda1daea-734c-4dd1-ac3c-1c9e4d168dac"), "Easy" },
                    { new Guid("e4549b08-11e3-4385-8163-0fccd6459bcc"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Region",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("024d447e-06bc-4ec2-a383-497288dc3ee3"), "NSL", "Nelson", "nsl-image.jpg" },
                    { new Guid("5ebfec4a-a5c5-4264-b910-b21f82ab3ac9"), "STL", "Southland", "stl-image.jpg" },
                    { new Guid("965ff222-0543-41a9-883f-c79ed940191a"), "BOP", "Bay of Planty", "bop-image.jpg" },
                    { new Guid("c7c9a318-1ab0-42f3-bd28-52187f9b5be7"), "AKL", "Auckland", "ak-image.jpg" },
                    { new Guid("d40fa755-7d03-45b4-a516-2b869b7ec4f9"), "WGN", "Welligton", "wgn-image.jpg" },
                    { new Guid("e281df03-351d-470e-8304-1c7d2026fa50"), "NTL", "Northland", "ntl-image.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("0fbd1bc7-f3ab-4914-a92e-b0fff04a5c24"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("dda1daea-734c-4dd1-ac3c-1c9e4d168dac"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("e4549b08-11e3-4385-8163-0fccd6459bcc"));

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: new Guid("024d447e-06bc-4ec2-a383-497288dc3ee3"));

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: new Guid("5ebfec4a-a5c5-4264-b910-b21f82ab3ac9"));

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: new Guid("965ff222-0543-41a9-883f-c79ed940191a"));

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: new Guid("c7c9a318-1ab0-42f3-bd28-52187f9b5be7"));

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: new Guid("d40fa755-7d03-45b4-a516-2b869b7ec4f9"));

            migrationBuilder.DeleteData(
                table: "Region",
                keyColumn: "Id",
                keyValue: new Guid("e281df03-351d-470e-8304-1c7d2026fa50"));
        }
    }
}
