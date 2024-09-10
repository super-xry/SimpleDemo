using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SimpleDemo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitCommerceDatabase : Migration
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
                    { "600b8e0b5bbc4b23a1e781311ef00e49", "Application", new DateTimeOffset(new DateTime(2024, 9, 10, 14, 28, 4, 122, DateTimeKind.Unspecified).AddTicks(8587), new TimeSpan(0, 0, 0, 0, 0)), "Visitor", null, null, "Visitor" },
                    { "e5c937f8008341459823324edc21c4d2", "Application", new DateTimeOffset(new DateTime(2024, 9, 10, 14, 28, 4, 122, DateTimeKind.Unspecified).AddTicks(8582), new TimeSpan(0, 0, 0, 0, 0)), "Admin", null, null, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "Label", "ModifiedBy", "ModifiedDate", "RoleId" },
                values: new object[,]
                {
                    { "aaee094758984331a9ea3fbeb01274ba", "Application", new DateTimeOffset(new DateTime(2024, 9, 10, 14, 28, 4, 122, DateTimeKind.Unspecified).AddTicks(8629), new TimeSpan(0, 0, 0, 0, 0)), "Full access for user.", "Permission.Full", null, null, "e5c937f8008341459823324edc21c4d2" },
                    { "d1a6c5fdcbd044a18d646c18a157cd29", "Application", new DateTimeOffset(new DateTime(2024, 9, 10, 14, 28, 4, 122, DateTimeKind.Unspecified).AddTicks(8616), new TimeSpan(0, 0, 0, 0, 0)), "Full access for permission.", "Permission.Full", null, null, "e5c937f8008341459823324edc21c4d2" },
                    { "ebb3d124da194877b7626d4597542ec4", "Application", new DateTimeOffset(new DateTime(2024, 9, 10, 14, 28, 4, 122, DateTimeKind.Unspecified).AddTicks(8631), new TimeSpan(0, 0, 0, 0, 0)), "Read the user self data.", "User.Read", null, null, "600b8e0b5bbc4b23a1e781311ef00e49" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Email", "ModifiedBy", "ModifiedDate", "Name", "NickName", "Password", "RoleId" },
                values: new object[,]
                {
                    { "3b63e730f3a94f04a236d83fc6e49da1", "Application", new DateTimeOffset(new DateTime(2024, 9, 10, 14, 28, 4, 122, DateTimeKind.Unspecified).AddTicks(8670), new TimeSpan(0, 0, 0, 0, 0)), "visitor@domain.com", null, null, "Visitor", "Hellen", "Hellen", "e5c937f8008341459823324edc21c4d2" },
                    { "eb7c2bced0f544f8990d3db8b03a9468", "Application", new DateTimeOffset(new DateTime(2024, 9, 10, 14, 28, 4, 122, DateTimeKind.Unspecified).AddTicks(8664), new TimeSpan(0, 0, 0, 0, 0)), "admin@domain.com", null, null, "Admin", "Judy", "Admin", "e5c937f8008341459823324edc21c4d2" }
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
