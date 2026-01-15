using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PAWPALme.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadePaths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_AdoptionApplication_AdoptionApplicationId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Pet_Shelter_ShelterId",
                table: "Pet");

            migrationBuilder.DropIndex(
                name: "IX_Shelter_OwnerUserId",
                table: "Shelter");

            migrationBuilder.DropColumn(
                name: "ManagedByUserId",
                table: "Appointment");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerUserId",
                table: "Shelter",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Pet",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(400)",
                oldMaxLength: 400,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Pet",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Pet",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "AdoptionApplicationId",
                table: "Appointment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AdopterUserId",
                table: "Appointment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PetId",
                table: "Appointment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShelterId",
                table: "Appointment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_AdopterUserId",
                table: "Appointment",
                column: "AdopterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_PetId",
                table: "Appointment",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_ShelterId",
                table: "Appointment",
                column: "ShelterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_AdoptionApplication_AdoptionApplicationId",
                table: "Appointment",
                column: "AdoptionApplicationId",
                principalTable: "AdoptionApplication",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_AspNetUsers_AdopterUserId",
                table: "Appointment",
                column: "AdopterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Pet_PetId",
                table: "Appointment",
                column: "PetId",
                principalTable: "Pet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Shelter_ShelterId",
                table: "Appointment",
                column: "ShelterId",
                principalTable: "Shelter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_Shelter_ShelterId",
                table: "Pet",
                column: "ShelterId",
                principalTable: "Shelter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_AdoptionApplication_AdoptionApplicationId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_AspNetUsers_AdopterUserId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Pet_PetId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Shelter_ShelterId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Pet_Shelter_ShelterId",
                table: "Pet");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_AdopterUserId",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_PetId",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_ShelterId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Pet");

            migrationBuilder.DropColumn(
                name: "AdopterUserId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "PetId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "ShelterId",
                table: "Appointment");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerUserId",
                table: "Shelter",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Pet",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Pet",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<int>(
                name: "AdoptionApplicationId",
                table: "Appointment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagedByUserId",
                table: "Appointment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shelter_OwnerUserId",
                table: "Shelter",
                column: "OwnerUserId",
                unique: true,
                filter: "[OwnerUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_AdoptionApplication_AdoptionApplicationId",
                table: "Appointment",
                column: "AdoptionApplicationId",
                principalTable: "AdoptionApplication",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_Shelter_ShelterId",
                table: "Pet",
                column: "ShelterId",
                principalTable: "Shelter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
