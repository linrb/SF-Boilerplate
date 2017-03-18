using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SF.WebHost.Migrations
{
    public partial class ModifyUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayInMemberList",
                table: "Core_User");

            migrationBuilder.DropColumn(
                name: "Trusted",
                table: "Core_User");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Core_User",
                newName: "WeChat");

            migrationBuilder.RenameColumn(
                name: "LastLoginUtc",
                table: "Core_User",
                newName: "PreviousVisit");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Core_User",
                newName: "Question");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Core_User",
                newName: "OICQ");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Core_User",
                newName: "LockStartDate");

            migrationBuilder.RenameColumn(
                name: "MoudleId",
                table: "Core_Permission",
                newName: "ModuleId");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Backend_UserExtensionEntity",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Backend_UserExtensionEntity",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 280,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "Backend_UserExtensionEntity",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Backend_UserExtensionEntity",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AddColumn<DateTime>(
                name: "AllowEndTime",
                table: "Core_User",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AllowStartTime",
                table: "Core_User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnswerQuestion",
                table: "Core_User",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "Core_User",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CheckOnLine",
                table: "Core_User",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DepartmentId",
                table: "Core_User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Core_User",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DutyId",
                table: "Core_User",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FirstVisit",
                table: "Core_User",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastVisit",
                table: "Core_User",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LockEndDate",
                table: "Core_User",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LogOnCount",
                table: "Core_User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MSN",
                table: "Core_User",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ManagerId",
                table: "Core_User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "Core_User",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OrganizeId",
                table: "Core_User",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PostId",
                table: "Core_User",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecurityLevel",
                table: "Core_User",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserOnLine",
                table: "Core_User",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WorkGroupId",
                table: "Core_User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowEndTime",
                table: "Core_User");

            migrationBuilder.DropColumn(
                name: "AllowStartTime",
                table: "Core_User");

            migrationBuilder.DropColumn(
                name: "AnswerQuestion",
                table: "Core_User");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Core_User");

            migrationBuilder.DropColumn(
                name: "CheckOnLine",
                table: "Core_User");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Core_User");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Core_User");

            migrationBuilder.DropColumn(
                name: "DutyId",
                table: "Core_User");

            migrationBuilder.DropColumn(
                name: "FirstVisit",
                table: "Core_User");

            migrationBuilder.DropColumn(
                name: "LastVisit",
                table: "Core_User");

            migrationBuilder.DropColumn(
                name: "LockEndDate",
                table: "Core_User");

            migrationBuilder.DropColumn(
                name: "LogOnCount",
                table: "Core_User");

            migrationBuilder.DropColumn(
                name: "MSN",
                table: "Core_User");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Core_User");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "Core_User");

            migrationBuilder.DropColumn(
                name: "OrganizeId",
                table: "Core_User");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Core_User");

            migrationBuilder.DropColumn(
                name: "SecurityLevel",
                table: "Core_User");

            migrationBuilder.DropColumn(
                name: "UserOnLine",
                table: "Core_User");

            migrationBuilder.DropColumn(
                name: "WorkGroupId",
                table: "Core_User");

            migrationBuilder.RenameColumn(
                name: "WeChat",
                table: "Core_User",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Question",
                table: "Core_User",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "PreviousVisit",
                table: "Core_User",
                newName: "LastLoginUtc");

            migrationBuilder.RenameColumn(
                name: "OICQ",
                table: "Core_User",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "LockStartDate",
                table: "Core_User",
                newName: "DateOfBirth");

            migrationBuilder.RenameColumn(
                name: "ModuleId",
                table: "Core_Permission",
                newName: "MoudleId");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Backend_UserExtensionEntity",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Backend_UserExtensionEntity",
                maxLength: 280,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "Backend_UserExtensionEntity",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Backend_UserExtensionEntity",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DisplayInMemberList",
                table: "Core_User",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Trusted",
                table: "Core_User",
                nullable: false,
                defaultValue: false);
        }
    }
}
