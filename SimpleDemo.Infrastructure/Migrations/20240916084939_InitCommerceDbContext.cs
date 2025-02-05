using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SimpleDemo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitCommerceDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    Target = table.Column<string>(type: "TEXT", nullable: true),
                    Category = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    ModifiedBy = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    ModifiedBy = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Label = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    ModifiedBy = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permission_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    NickName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    ModifiedBy = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "ModifiedBy", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { "356948299a4e4daeb3eb6ef16f2e082b", "Application", new DateTimeOffset(new DateTime(2024, 9, 16, 8, 49, 38, 812, DateTimeKind.Unspecified).AddTicks(2117), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, null, "Admin" },
                    { "7960226b50474667bce3f9ec9c9e8a11", "Application", new DateTimeOffset(new DateTime(2024, 9, 16, 8, 49, 38, 812, DateTimeKind.Unspecified).AddTicks(2122), new TimeSpan(0, 0, 0, 0, 0)), "Visitor", null, null, "Visitor" }
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "Label", "ModifiedBy", "ModifiedDate", "RoleId" },
                values: new object[,]
                {
                    { "11fc9285577043f4bd31b662b848eb64", "Application", new DateTimeOffset(new DateTime(2024, 9, 16, 8, 49, 38, 812, DateTimeKind.Unspecified).AddTicks(2149), new TimeSpan(0, 0, 0, 0, 0)), "FullAccess access for permission.", "Permission.FullAccess", null, null, "356948299a4e4daeb3eb6ef16f2e082b" },
                    { "a44531e21c12495c8c8d6e5946c438df", "Application", new DateTimeOffset(new DateTime(2024, 9, 16, 8, 49, 38, 812, DateTimeKind.Unspecified).AddTicks(2153), new TimeSpan(0, 0, 0, 0, 0)), "FullAccess access for user.", "User.FullAccess", null, null, "356948299a4e4daeb3eb6ef16f2e082b" },
                    { "bc5d773f947042f3a007c8040736f5b1", "Application", new DateTimeOffset(new DateTime(2024, 9, 16, 8, 49, 38, 812, DateTimeKind.Unspecified).AddTicks(2186), new TimeSpan(0, 0, 0, 0, 0)), "Read the user self data.", "User.Read", null, null, "7960226b50474667bce3f9ec9c9e8a11" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Email", "ModifiedBy", "ModifiedDate", "Name", "NickName", "Password", "RoleId" },
                values: new object[,]
                {
                    { "0610d93676b14cd799e64254e347a49a", "Application", new DateTimeOffset(new DateTime(2024, 9, 16, 8, 49, 38, 812, DateTimeKind.Unspecified).AddTicks(2221), new TimeSpan(0, 0, 0, 0, 0)), "visitor@domain.com", null, null, "Visitor", "Hellen", "Hellen", "356948299a4e4daeb3eb6ef16f2e082b" },
                    { "a8a95945c3c94fb1a677aa6394d3ee01", "Application", new DateTimeOffset(new DateTime(2024, 9, 16, 8, 49, 38, 812, DateTimeKind.Unspecified).AddTicks(2216), new TimeSpan(0, 0, 0, 0, 0)), "admin@domain.com", null, null, "Admin", "Judy", "Admin", "356948299a4e4daeb3eb6ef16f2e082b" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Log_Category",
                table: "Log",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_Log_CreatedDate",
                table: "Log",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Log_Level",
                table: "Log",
                column: "Level");

            migrationBuilder.CreateIndex(
                name: "IX_Log_Title",
                table: "Log",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_Label",
                table: "Permission",
                column: "Label");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_RoleId",
                table: "Permission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Name",
                table: "Role",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Name",
                table: "User",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_NickName",
                table: "User",
                column: "NickName");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
