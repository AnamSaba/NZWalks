using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class Seedingdatasforwalkdifficultyandregiontables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Area", "Code", "Lat", "Long", "Name", "Population", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("68c2ab66-c5eb-40b6-81e0-954456d06bba"), 12230.0, "BAYP", -37.532825899999999, 175.7642701, "Bay Of Plenty", 345400L, null },
                    { new Guid("7391432f-c164-4e3e-9873-b2c57389d773"), 4894.0, "AKL", -36.525320700000002, 173.77857040000001, "Auckland", 1718982L, "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("79e9872d-5a2f-413e-ac36-511036ccd3d4"), 8970.0, "WAIK", -37.514458400000002, 174.54051279999999, "Waikato", 496700L, null },
                    { new Guid("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"), 5230.0, "WGN", -37.531625900000002, 178.76423410000001, "Wellington", 134400L, "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" }
                });

            migrationBuilder.InsertData(
                table: "WalkDifficulty",
                columns: new[] { "Id", "Code" },
                values: new object[,]
                {
                    { new Guid("07a52212-552c-4e30-8000-d118a78e5ac7"), "Medium" },
                    { new Guid("a4cfcd9e-ccf8-4f3f-82ea-4f37c2de3368"), "Hard" },
                    { new Guid("fe140afa-4217-46e0-b65f-7e76f64ee2e3"), "Easy" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("68c2ab66-c5eb-40b6-81e0-954456d06bba"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("7391432f-c164-4e3e-9873-b2c57389d773"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("79e9872d-5a2f-413e-ac36-511036ccd3d4"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"));

            migrationBuilder.DeleteData(
                table: "WalkDifficulty",
                keyColumn: "Id",
                keyValue: new Guid("07a52212-552c-4e30-8000-d118a78e5ac7"));

            migrationBuilder.DeleteData(
                table: "WalkDifficulty",
                keyColumn: "Id",
                keyValue: new Guid("a4cfcd9e-ccf8-4f3f-82ea-4f37c2de3368"));

            migrationBuilder.DeleteData(
                table: "WalkDifficulty",
                keyColumn: "Id",
                keyValue: new Guid("fe140afa-4217-46e0-b65f-7e76f64ee2e3"));
        }
    }
}
