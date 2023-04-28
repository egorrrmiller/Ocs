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
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2023, 4, 28, 18, 38, 40, 599, DateTimeKind.Utc).AddTicks(8723)),
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
                    new Guid("08f97d91-09bf-4e38-8265-e4717d6de36e"),
                    new Guid("11f55e7f-9742-4d09-9d0f-9014a82549cc"),
                    new Guid("1590212e-1b8d-4f30-a650-7e3569677156"),
                    new Guid("1fb65a2c-f82d-45ac-a295-ee98afb020a2"),
                    new Guid("22824e4e-5c8d-4808-9983-b040f1baef8d"),
                    new Guid("3daff089-ed2d-4d05-a794-290c01794384"),
                    new Guid("41907136-5441-48c6-b592-6130086afb75"),
                    new Guid("41eacc74-e8d6-48fc-baff-b5fa8257945b"),
                    new Guid("4c234817-d2d4-4b35-9d25-26803496cb1c"),
                    new Guid("5007cb05-788c-4fd0-b377-64c2ff528fd1"),
                    new Guid("509ba40a-a9f9-4218-89f1-57db8459cc55"),
                    new Guid("5b525892-b9e8-4779-94c8-8bfe287f43f4"),
                    new Guid("680896c3-6d63-4e5d-9262-a69af2f8c38b"),
                    new Guid("68b4dbcb-330b-41b5-b69f-595bc41764ba"),
                    new Guid("78f6428c-e0aa-4593-b8ea-fab86d040440"),
                    new Guid("867e99a0-8c68-48c6-b623-9b43cf1aa992"),
                    new Guid("8a0858da-a7d0-4de0-831d-9ddaa9cfbeff"),
                    new Guid("8b51ef0c-0a0c-4ba8-81ae-c5f3227ceee7"),
                    new Guid("8ebe58ba-245a-4710-8081-4bec4d8ea5d0"),
                    new Guid("90f6708b-5448-4f3f-a16e-947f6de7b182"),
                    new Guid("9168a98a-a902-4d4b-a58d-a9de2ac180bf"),
                    new Guid("9563d4a7-80dd-4714-a101-5492cb425712"),
                    new Guid("9c8dd8af-8bf8-4747-a6f8-8417b6f0148c"),
                    new Guid("9f1c38a8-0a4a-44e6-9a7b-2b0a62f77000"),
                    new Guid("a6fbed27-e339-45fa-a552-410dee22739e"),
                    new Guid("a75fd813-10b9-4723-9be6-0acdc1334789"),
                    new Guid("ab22f83d-0508-4d91-9f16-8c0d0a04490d"),
                    new Guid("ab6a4a74-b266-4bc1-a261-f1fdddb5d395"),
                    new Guid("afca6456-d05a-4dda-805a-942bd2d8157a"),
                    new Guid("b176ee00-a04a-429b-8d6c-9b47480bcadd"),
                    new Guid("b5c871c8-092a-4055-8fdb-1de201df4eca"),
                    new Guid("ba0cfb15-6bd5-4f70-8698-fb5d288b8dcc"),
                    new Guid("ba7b29e4-4d53-4570-ac08-a48f5d232c4f"),
                    new Guid("bc486be6-683e-4f4a-8f86-b8451f8ce798"),
                    new Guid("c01b07b1-270a-4598-961b-c77cd0c3bf72"),
                    new Guid("c27012a3-e89c-405e-a103-fc6535f06e20"),
                    new Guid("c56a1b48-1683-4aaa-b848-791e2d25ca3b"),
                    new Guid("d1a454fe-af60-433b-ac4f-bfd0fab0b39b"),
                    new Guid("d255964e-4d97-4ba1-9e29-6f227cd634c5"),
                    new Guid("d7257e6e-3ce0-4804-ab31-1c9517003b5a"),
                    new Guid("d7dedb60-efbd-42e7-804e-1c1ea82c87b4"),
                    new Guid("d99a9bfe-89a1-45f5-a852-9f10b22fde33"),
                    new Guid("e39845b5-4896-4b2b-9372-ac0227c1a2c1"),
                    new Guid("e9610579-fd2b-4e29-9336-d41e6f489dbd"),
                    new Guid("eca26b2e-b7f0-4049-9425-f4633f7d2f69"),
                    new Guid("f38a6125-6f60-4c4a-9642-d999c9df022b"),
                    new Guid("f618d6b6-33e8-4e28-9c70-222f60da9852"),
                    new Guid("fbd0b791-f552-4dc8-ac5b-29760691f221"),
                    new Guid("fd009a1e-0817-4c6b-a9f9-e16d0e294ae4"),
                    new Guid("fed8dd97-abb0-4d48-8648-ce89f70e24de")
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
