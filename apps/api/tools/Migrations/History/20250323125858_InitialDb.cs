using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FwksLabs.Boilerplate.Tools.Migrations.History;

/// <inheritdoc />
public partial class InitialDb : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "App");

        migrationBuilder.CreateTable(
            name: "Customers",
            schema: "App",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "text", nullable: false),
                PhoneNumber = table.Column<string>(type: "text", nullable: true),
                Email = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Customers", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Orders",
            schema: "App",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                Products = table.Column<string>(type: "jsonb", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Orders", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Products",
            schema: "App",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "text", nullable: false),
                Description = table.Column<string>(type: "text", nullable: false),
                Price = table.Column<decimal>(type: "numeric", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Products", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Customers",
            schema: "App");

        migrationBuilder.DropTable(
            name: "Orders",
            schema: "App");

        migrationBuilder.DropTable(
            name: "Products",
            schema: "App");
    }
}