using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SF.WebHost.Migrations
{
    public partial class DeleteUserEx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Backend_UserExtensionEntity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Backend_UserExtensionEntity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AllowEndTime = table.Column<DateTime>(nullable: true),
                    AllowStartTime = table.Column<DateTime>(nullable: true),
                    AnswerQuestion = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: true),
                    CheckOnLine = table.Column<int>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    DepartmentId = table.Column<long>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DutyId = table.Column<long>(nullable: true),
                    DutyName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EnabledMark = table.Column<int>(nullable: true),
                    FirstVisit = table.Column<DateTime>(nullable: true),
                    Gender = table.Column<int>(nullable: true),
                    HeadIcon = table.Column<string>(nullable: true),
                    LastVisit = table.Column<DateTime>(nullable: true),
                    LockEndDate = table.Column<DateTime>(nullable: true),
                    LockStartDate = table.Column<DateTime>(nullable: true),
                    LogOnCount = table.Column<int>(nullable: true),
                    MSN = table.Column<string>(nullable: true),
                    Manager = table.Column<string>(nullable: true),
                    ManagerId = table.Column<long>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    NickName = table.Column<string>(nullable: true),
                    OICQ = table.Column<string>(nullable: true),
                    OrganizeId = table.Column<long>(nullable: true),
                    PostId = table.Column<string>(nullable: true),
                    PostName = table.Column<long>(nullable: true),
                    PreviousVisit = table.Column<DateTime>(nullable: true),
                    Question = table.Column<string>(nullable: true),
                    QuickQuery = table.Column<string>(nullable: true),
                    SecurityLevel = table.Column<int>(nullable: true),
                    SimpleSpelling = table.Column<string>(nullable: true),
                    SortIndex = table.Column<int>(nullable: false),
                    Telephone = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    UserOnLine = table.Column<int>(nullable: true),
                    WeChat = table.Column<string>(nullable: true),
                    WorkGroupId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Backend_UserExtensionEntity", x => x.Id);
                });
        }
    }
}
