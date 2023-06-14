using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PermissionManagement.Model.Migrations
{
    public partial class initialv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    EmployeeFirstName = table.Column<string>(nullable: true),
                    EmployeeLastName = table.Column<string>(nullable: true),
                    PermissionTypeId = table.Column<int>(nullable: false),
                    PermissionDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PermissionId = table.Column<int>(nullable: true),
                    PermissionId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionType_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PermissionType_Permission_PermissionId1",
                        column: x => x.PermissionId1,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionType_PermissionId",
                table: "PermissionType",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionType_PermissionId1",
                table: "PermissionType",
                column: "PermissionId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissionType");

            migrationBuilder.DropTable(
                name: "Permission");
        }
    }
}
