using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

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
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2023, 4, 26, 7, 22, 37, 86, DateTimeKind.Utc).AddTicks(3352)),
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Qty = table.Column<int>(type: "integer", nullable: false)
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
                columns: new[] { "Id", "Qty" },
                values: new object[,]
                {
                    { new Guid("13f0b6bd-44d4-462e-b043-fc798cfcf5fe"), 1000 },
                    { new Guid("1c929cdf-3a03-41f5-8a77-56966d367ccd"), 1000 },
                    { new Guid("1d1404a6-6e36-4a7d-89fa-9e47d7cc41d8"), 1000 },
                    { new Guid("1dbe376f-f233-4604-b5eb-39e0371eefd0"), 1000 },
                    { new Guid("1dc8bd49-5311-4009-a146-8a1e32082349"), 1000 },
                    { new Guid("1ee79ce2-2662-4708-aa9c-7467e1ebbfe0"), 1000 },
                    { new Guid("2580ae71-7148-4352-a546-7deb2c83616b"), 1000 },
                    { new Guid("26a39d59-c59c-4e9d-97fd-92eb88b42bcf"), 1000 },
                    { new Guid("27edb7b4-6197-4965-8c3e-c7b80374e1b9"), 1000 },
                    { new Guid("2d5b2208-949d-4784-920d-7e81b1660c80"), 1000 },
                    { new Guid("37abb814-24a0-454a-bfc2-5e1ff28e1b3a"), 1000 },
                    { new Guid("4467bafa-1870-4a8a-89a1-40fe37f1b23a"), 1000 },
                    { new Guid("4645c070-07d7-4068-9f49-11ed038dc2ef"), 1000 },
                    { new Guid("4bde830b-9556-43da-bd40-fa8936067530"), 1000 },
                    { new Guid("4cc645fc-5c3a-4737-a059-5c9bf8a034cb"), 1000 },
                    { new Guid("4d5ea812-92b5-4601-be9b-f75932493c49"), 1000 },
                    { new Guid("5cb923ea-e162-46e0-b200-29f01a0322af"), 1000 },
                    { new Guid("5d1196d9-21aa-44f5-989c-d0d990b5034c"), 1000 },
                    { new Guid("62c030fd-0d83-42fb-9e10-90a25d7fd2e8"), 1000 },
                    { new Guid("755c27ac-546d-4a0a-9dff-9f913a8d76de"), 1000 },
                    { new Guid("76fa884b-bf81-4ae6-8e49-c0aec22b7c6d"), 1000 },
                    { new Guid("88e3fb80-3c2b-4e62-a8e4-cb4bf4aabe35"), 1000 },
                    { new Guid("8d0f2f31-62d1-4fde-9d3a-9a6a1b388109"), 1000 },
                    { new Guid("8e8dd283-2180-4a0f-88e4-87815765aaae"), 1000 },
                    { new Guid("8f1dd192-15da-4d45-b93c-d4eab6f4c9ae"), 1000 },
                    { new Guid("910d8112-94c3-4d4f-b5d0-1ae36857a74f"), 1000 },
                    { new Guid("92085817-64f1-4463-8477-2bd1d71f4cda"), 1000 },
                    { new Guid("92e50e46-ffc0-422d-9ad6-6d267e96eaee"), 1000 },
                    { new Guid("9352e199-fd21-44d4-8897-428c507118ee"), 1000 },
                    { new Guid("948390e4-9f99-4bbb-a950-8c80a008c689"), 1000 },
                    { new Guid("95679236-2bf3-45d9-a1b3-6063723f6c38"), 1000 },
                    { new Guid("a1dbc78e-1240-4938-b25e-d5f9afcc3328"), 1000 },
                    { new Guid("b61f1006-8b19-46a8-a116-63948b64452a"), 1000 },
                    { new Guid("bbb06749-f595-4024-a07e-9f4dd89ffa88"), 1000 },
                    { new Guid("bc083f34-f951-47e6-9696-12f2102607ef"), 1000 },
                    { new Guid("bdc4e483-0c61-4b1b-b53c-f4c60ea41895"), 1000 },
                    { new Guid("c0b01f51-21f8-4e2f-b418-eb7645e69826"), 1000 },
                    { new Guid("c59dfb45-6dbe-4daf-8d7c-b11b900e41be"), 1000 },
                    { new Guid("d1d3b54b-c6df-4ea8-9c3d-eb2e55ae1804"), 1000 },
                    { new Guid("d3d4bbd6-d0fd-41ca-b58c-7ed625393134"), 1000 },
                    { new Guid("dc048231-6a12-4e22-b810-a3d0aac79b96"), 1000 },
                    { new Guid("e0ef3a49-a235-4614-ac8b-a9dd299c62f8"), 1000 },
                    { new Guid("e301449b-f9f1-4e4e-9062-b0157bf322ba"), 1000 },
                    { new Guid("e38780b2-4d84-4359-b76c-f409584f7e6a"), 1000 },
                    { new Guid("edd998c2-0512-431e-a10c-3aa989bdcb8e"), 1000 },
                    { new Guid("f29c9621-5f1f-4eaf-a01f-2bf77f7e7a8c"), 1000 },
                    { new Guid("f835e192-cbf1-4941-b2d7-f3cce1a1d83e"), 1000 },
                    { new Guid("f9bc1bdf-5dbd-4627-b04a-faa07da93e16"), 1000 },
                    { new Guid("fa704faa-b21a-4a50-a99e-e928b5868685"), 1000 },
                    { new Guid("fb7b1092-a3f6-41b5-a70f-243b65364990"), 1000 }
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
