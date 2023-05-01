using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ocs.Database.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Line",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Line", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2023, 5, 1, 20, 16, 4, 294, DateTimeKind.Utc).AddTicks(1137)),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderLines",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    LineId = table.Column<Guid>(type: "uuid", nullable: false),
                    Qty = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLines", x => new { x.LineId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_OrderLines_Line_LineId",
                        column: x => x.LineId,
                        principalTable: "Line",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderLines_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Line",
                column: "Id",
                values: new object[]
                {
                    new Guid("009b05ff-bd04-4168-ac70-17432900316d"),
                    new Guid("0ce5af31-68a7-4c29-92f5-9546c944144f"),
                    new Guid("0f2c07fb-29ed-4c66-a5c3-a9d4256002d3"),
                    new Guid("106ab175-294f-4364-ad0f-31c3c541fc11"),
                    new Guid("110da4a4-93f4-46c1-9f78-cfc29d1dd9b9"),
                    new Guid("15e21011-4105-4bf5-b639-13398b637f68"),
                    new Guid("1f0abea1-2039-474e-b2c4-2b2f1032382a"),
                    new Guid("20135180-226b-43bf-af3c-8dca53427587"),
                    new Guid("22698cf8-8957-47b6-aafd-6755f11add5f"),
                    new Guid("242f1809-8378-4312-91fd-3d9436e212a7"),
                    new Guid("2aaab396-0c79-486b-a7f9-85e79858abe7"),
                    new Guid("2ce73928-8537-407c-9301-d4357c1fa0d2"),
                    new Guid("2faae84f-c8eb-484d-a524-b5471fcf6a53"),
                    new Guid("3c90ba9b-0557-4db3-8733-7b0f645c65d0"),
                    new Guid("458d55f7-bd4a-457c-bb19-904e8cfc7dcd"),
                    new Guid("56bc261f-cf87-483a-909c-3e333470fd42"),
                    new Guid("61abd1c8-9fd0-4048-8bf8-f31ed476a52b"),
                    new Guid("626be9f4-63e6-403a-afbe-1455923a8bc2"),
                    new Guid("63c03215-28ed-4edc-a6c6-0ecf5b6ecaa8"),
                    new Guid("67a614da-0127-437b-8b0d-f1d76ac3038e"),
                    new Guid("7032c6f6-f0e6-4f9d-a312-7103a705f8f4"),
                    new Guid("791958aa-29d8-4bab-bee1-9941d86b9e8e"),
                    new Guid("82963531-08b4-42bc-8ca5-2b63217b907b"),
                    new Guid("88200354-7139-4e79-aa8f-0c0f6b52ab1b"),
                    new Guid("8b34735d-8189-45b4-aadc-fb9d6e07a703"),
                    new Guid("8db7735a-bb82-4055-bbe4-4686bd64e07e"),
                    new Guid("93e297f9-d684-41b9-b6c4-b52581acebd8"),
                    new Guid("94335d2b-933b-47db-8d1e-8b4f1b868011"),
                    new Guid("a32b1bbb-7f39-4c68-9a8a-90ab058a2e86"),
                    new Guid("a69ad73a-80b3-4232-b888-96cced0a28f5"),
                    new Guid("a734c5a6-c0a2-491e-8ff4-3709b3e87d62"),
                    new Guid("aff3afa8-65df-4b6f-89ed-a5f46b18a61a"),
                    new Guid("b79b3456-4f33-4d5d-a9e4-7b2fa8ed6e54"),
                    new Guid("b9f76b9f-87fa-4737-ab57-15838554ba80"),
                    new Guid("bae680f2-4836-4cb0-880e-7cb3c6678ea3"),
                    new Guid("bde84c87-6f9f-4899-83a6-5d9c45541342"),
                    new Guid("bf72d2bd-34d8-416d-9e02-6ec1fc83f14a"),
                    new Guid("c6b67aac-5954-469e-acd1-ad7a384d1875"),
                    new Guid("cdfdb77b-5638-4524-a99e-2cfbb27fa5ef"),
                    new Guid("ce24dfd7-b167-44c3-8afd-159bec9e6e2e"),
                    new Guid("d28d44e1-5e12-4413-b773-56d16da146d2"),
                    new Guid("d7b56112-5411-4b1c-8163-95f654f8f5cf"),
                    new Guid("d7df1797-e88e-4e02-95d0-006987426e95"),
                    new Guid("dbba1488-15a3-48f0-a403-9d3846bf310b"),
                    new Guid("de9544ae-7f20-4333-81e0-2d9d035dd0df"),
                    new Guid("df359c88-4541-433b-bc2a-3a2cb305e352"),
                    new Guid("e0fdcf76-17c8-4ed0-8d1f-078e0783a117"),
                    new Guid("f8744b5b-e6ad-4727-9313-ccf899d3b13e"),
                    new Guid("fcf750cf-a001-4a76-a25d-554ee18e0dd1"),
                    new Guid("ff3541f9-4a1a-41c2-8281-dee2c72b683e")
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_OrderId",
                table: "OrderLines",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderLines");

            migrationBuilder.DropTable(
                name: "Line");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
