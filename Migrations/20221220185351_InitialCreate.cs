using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AdminApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AmountAllowed = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Resumen = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transaction_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "AmountAllowed", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("b850770d-270d-4d96-b69e-062030a37d27"), 10000.0, "Gastos Realizados en comida mensual/diaria", "Comida" },
                    { new Guid("b850770d-270d-4d96-b69e-062030a37d58"), 7000.0, "Gastos Realizados Mantenimiento", "Mantenimiento Casa" }
                });

            migrationBuilder.InsertData(
                table: "Transaction",
                columns: new[] { "TransactionId", "CategoryId", "Date", "Description", "Resumen", "Title", "Type" },
                values: new object[,]
                {
                    { new Guid("b850770d-270d-4d96-b69e-062030a37d11"), new Guid("b850770d-270d-4d96-b69e-062030a37d27"), new DateTime(2022, 12, 20, 15, 53, 49, 337, DateTimeKind.Local).AddTicks(9744), "Compra mensual y asado", null, "Compra del dino", 3 },
                    { new Guid("b850770d-270d-4d96-b69e-062030a37d37"), new Guid("b850770d-270d-4d96-b69e-062030a37d58"), new DateTime(2022, 12, 20, 15, 53, 49, 337, DateTimeKind.Local).AddTicks(9776), "Reparacion Inodoro", null, "Gotita", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CategoryId",
                table: "Transaction",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
