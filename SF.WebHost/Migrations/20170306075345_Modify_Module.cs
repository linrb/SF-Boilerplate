using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SF.WebHost.Migrations
{
    public partial class Modify_Module : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Core_Module",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AllowDelete = table.Column<int>(nullable: true),
                    AllowEdit = table.Column<int>(nullable: true),
                    AllowExpand = table.Column<int>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeleteMark = table.Column<int>(nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    Description = table.Column<string>(maxLength: 280, nullable: true),
                    EnCode = table.Column<string>(maxLength: 127, nullable: true),
                    EnabledMark = table.Column<int>(nullable: true),
                    FullName = table.Column<string>(maxLength: 127, nullable: true),
                    Icon = table.Column<string>(maxLength: 127, nullable: true),
                    IsMenu = table.Column<int>(nullable: true),
                    IsPublic = table.Column<int>(nullable: true),
                    ParentId = table.Column<long>(nullable: true),
                    SortIndex = table.Column<int>(nullable: false),
                    Target = table.Column<string>(maxLength: 127, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false),
                    UrlAddress = table.Column<string>(maxLength: 127, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Core_Module", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Core_RoleModule",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    ModuleId = table.Column<long>(nullable: false),
                    RoleId = table.Column<long>(nullable: false),
                    SortIndex = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Core_RoleModule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Core_RoleModule_Core_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Core_Module",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_RoleModule_Core_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Core_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Core_RoleModule_ModuleId",
                table: "Core_RoleModule",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Core_RoleModule_RoleId",
                table: "Core_RoleModule",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Core_RoleModule");

            migrationBuilder.DropTable(
                name: "Core_Module");

        }
    }
}
