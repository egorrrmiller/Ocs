﻿using System;
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
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2023, 4, 26, 20, 30, 26, 97, DateTimeKind.Utc).AddTicks(4450)),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Qty = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                column: "Id",
                values: new object[]
                {
                    new Guid("00f6c93c-7a95-46fc-943c-47ab80c88b0d"),
                    new Guid("01707c7d-19cc-45ee-b8a5-7bc2769ee857"),
                    new Guid("035dffad-d121-47ef-867d-c1123dfec03e"),
                    new Guid("076468af-d6b8-42dc-9cd2-6c0539c785b9"),
                    new Guid("08f5d07c-5eb2-40cd-8169-37e6c2e5c34d"),
                    new Guid("092ec219-85f8-4abe-9ea2-3378e56e43b9"),
                    new Guid("0ae756c7-3038-4d06-9eef-59fa31357578"),
                    new Guid("1afe945c-a7b1-48d8-8157-4c482eed2824"),
                    new Guid("201397e4-5fda-401d-8f5a-1d1dd9405c77"),
                    new Guid("206e96fd-19fd-4e36-8a06-f187afc2c6f2"),
                    new Guid("218f4d43-d4ce-4f22-825a-8c81bde8dfc0"),
                    new Guid("248b9aef-cc0f-4817-916d-ddf5352199f2"),
                    new Guid("2e66a63b-f746-42e4-977b-763cd02ff9b7"),
                    new Guid("33a9b2f0-b85b-4042-b33d-de0d675d911b"),
                    new Guid("3615b061-772c-4f74-be99-20af97ffce86"),
                    new Guid("36f04eb5-7ea6-4cf2-9b3d-01b543b8d07b"),
                    new Guid("38dce964-de3b-4d1b-a481-4c59c25f6a45"),
                    new Guid("495be9ae-7f80-4312-b799-06fb5ab19d0b"),
                    new Guid("4d4122ef-ffe1-4427-947f-e8f4f83cf4db"),
                    new Guid("4e65504d-bc3d-44ca-972d-5c855f7a771c"),
                    new Guid("509ec21d-b270-4c3d-8370-ad3efeeafb9a"),
                    new Guid("67978dcd-a62c-4dd5-a6ae-76a841cc4f11"),
                    new Guid("71296abd-521a-4131-8a77-76770b62d1ae"),
                    new Guid("7fdb0f83-c37d-427b-af5a-7bbf107bd55f"),
                    new Guid("856a2c3e-8411-474c-a863-b38db6257a5d"),
                    new Guid("871525aa-3a30-4f60-8870-0ae431c18e02"),
                    new Guid("876af8b8-2004-4d99-a30d-872ed682e2a7"),
                    new Guid("8a4f891a-616d-45f0-b090-586d14b49ce2"),
                    new Guid("8d454183-cad4-4180-9ae0-17d8b3d4ba10"),
                    new Guid("8f04c6e7-67af-44b9-80fe-706ae4bf50ba"),
                    new Guid("8fb4a0ce-22ea-43d6-a384-93f875b001f1"),
                    new Guid("962f38b9-d3b5-44f9-a097-1a33244d8007"),
                    new Guid("98edb4c9-9ba1-4f20-a3de-c453e7f63f1e"),
                    new Guid("991e3e9d-3541-475c-a0cc-c9408cd96aa7"),
                    new Guid("9928d308-b707-43a5-9a0b-2101d977222c"),
                    new Guid("a1247e1d-2d45-48f4-97f5-d9166ed3174b"),
                    new Guid("a7921430-9e08-44ef-800e-a12c2a5a178c"),
                    new Guid("afbc58dc-7491-4039-86ef-07ceb7246663"),
                    new Guid("b1c5226b-4c44-4268-b660-1693615236b9"),
                    new Guid("b3b6625d-85a2-47d6-88c6-53ba222a2cd4"),
                    new Guid("b671c1a9-599d-4d2b-824e-9c483d59b418"),
                    new Guid("cb75ed5c-a8c1-4bcd-b76c-d950dc1fd29d"),
                    new Guid("cd718122-9cbb-4962-90e4-7e73eaa1e12a"),
                    new Guid("d78fc202-a650-4cd8-a4e7-6be5c3735311"),
                    new Guid("d7cb769d-3e16-43e1-8044-142a229a8514"),
                    new Guid("e898cc64-9593-4735-920e-1b211908c689"),
                    new Guid("eb3c21f5-435c-4ca0-932d-73e5aacdd640"),
                    new Guid("edd32b73-002a-473d-819e-e7acb0dc6463"),
                    new Guid("fafe3fd8-51af-4437-ba94-dff7105cd2f9"),
                    new Guid("fec237f7-da60-4242-b7ac-2eb6d8544c77")
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProducts",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
