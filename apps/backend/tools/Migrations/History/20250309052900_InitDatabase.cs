using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FwksLabs.Boilerplate.Tools.Migrations.History;

/// <inheritdoc />
public partial class InitDatabase : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "App");

        migrationBuilder.CreateTable(
            name: "Posts",
            schema: "App",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                ModerationStatus = table.Column<int>(type: "integer", nullable: false),
                Title = table.Column<string>(type: "text", nullable: false),
                Content = table.Column<string>(type: "text", nullable: false),
                PublishedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                ReferenceId = table.Column<Guid>(type: "uuid", nullable: false),
                Author = table.Column<string>(type: "jsonb", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Posts", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Comments",
            schema: "App",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                PostId = table.Column<Guid>(type: "uuid", nullable: false),
                Content = table.Column<string>(type: "text", nullable: false),
                ReferenceId = table.Column<Guid>(type: "uuid", nullable: false),
                Author = table.Column<string>(type: "jsonb", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Comments", x => x.Id);
                table.ForeignKey(
                    name: "FK_Comments_Posts_PostId",
                    column: x => x.PostId,
                    principalSchema: "App",
                    principalTable: "Posts",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Comments_PostId",
            schema: "App",
            table: "Comments",
            column: "PostId");

        migrationBuilder.CreateIndex(
            name: "IX_UK_Comments",
            schema: "App",
            table: "Comments",
            column: "ReferenceId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_UK_Posts",
            schema: "App",
            table: "Posts",
            column: "ReferenceId",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Comments",
            schema: "App");

        migrationBuilder.DropTable(
            name: "Posts",
            schema: "App");
    }
}