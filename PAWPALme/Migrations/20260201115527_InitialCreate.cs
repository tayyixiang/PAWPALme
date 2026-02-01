using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PAWPALme.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionApplication_AspNetUsers_UserId",
                table: "AdoptionApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionApplication_Pet_PetId",
                table: "AdoptionApplication");

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
                name: "FK_AspNetUsers_Shelter_ShelterId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Pet_Shelter_ShelterId",
                table: "Pet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shelter",
                table: "Shelter");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pet",
                table: "Pet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdoptionApplication",
                table: "AdoptionApplication");

            migrationBuilder.RenameTable(
                name: "Shelter",
                newName: "Shelters");

            migrationBuilder.RenameTable(
                name: "Pet",
                newName: "Pets");

            migrationBuilder.RenameTable(
                name: "Appointment",
                newName: "Appointments");

            migrationBuilder.RenameTable(
                name: "AdoptionApplication",
                newName: "AdoptionApplications");

            migrationBuilder.RenameIndex(
                name: "IX_Pet_ShelterId",
                table: "Pets",
                newName: "IX_Pets_ShelterId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_ShelterId",
                table: "Appointments",
                newName: "IX_Appointments_ShelterId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_PetId",
                table: "Appointments",
                newName: "IX_Appointments_PetId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_AdoptionApplicationId",
                table: "Appointments",
                newName: "IX_Appointments_AdoptionApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_AdopterUserId",
                table: "Appointments",
                newName: "IX_Appointments_AdopterUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AdoptionApplication_UserId",
                table: "AdoptionApplications",
                newName: "IX_AdoptionApplications_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AdoptionApplication_PetId",
                table: "AdoptionApplications",
                newName: "IX_AdoptionApplications_PetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shelters",
                table: "Shelters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pets",
                table: "Pets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdoptionApplications",
                table: "AdoptionApplications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionApplications_AspNetUsers_UserId",
                table: "AdoptionApplications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionApplications_Pets_PetId",
                table: "AdoptionApplications",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AdoptionApplications_AdoptionApplicationId",
                table: "Appointments",
                column: "AdoptionApplicationId",
                principalTable: "AdoptionApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_AdopterUserId",
                table: "Appointments",
                column: "AdopterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Pets_PetId",
                table: "Appointments",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Shelters_ShelterId",
                table: "Appointments",
                column: "ShelterId",
                principalTable: "Shelters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Shelters_ShelterId",
                table: "AspNetUsers",
                column: "ShelterId",
                principalTable: "Shelters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Shelters_ShelterId",
                table: "Pets",
                column: "ShelterId",
                principalTable: "Shelters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionApplications_AspNetUsers_UserId",
                table: "AdoptionApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionApplications_Pets_PetId",
                table: "AdoptionApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AdoptionApplications_AdoptionApplicationId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_AdopterUserId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Pets_PetId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Shelters_ShelterId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Shelters_ShelterId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Shelters_ShelterId",
                table: "Pets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shelters",
                table: "Shelters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pets",
                table: "Pets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdoptionApplications",
                table: "AdoptionApplications");

            migrationBuilder.RenameTable(
                name: "Shelters",
                newName: "Shelter");

            migrationBuilder.RenameTable(
                name: "Pets",
                newName: "Pet");

            migrationBuilder.RenameTable(
                name: "Appointments",
                newName: "Appointment");

            migrationBuilder.RenameTable(
                name: "AdoptionApplications",
                newName: "AdoptionApplication");

            migrationBuilder.RenameIndex(
                name: "IX_Pets_ShelterId",
                table: "Pet",
                newName: "IX_Pet_ShelterId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_ShelterId",
                table: "Appointment",
                newName: "IX_Appointment_ShelterId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_PetId",
                table: "Appointment",
                newName: "IX_Appointment_PetId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_AdoptionApplicationId",
                table: "Appointment",
                newName: "IX_Appointment_AdoptionApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_AdopterUserId",
                table: "Appointment",
                newName: "IX_Appointment_AdopterUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AdoptionApplications_UserId",
                table: "AdoptionApplication",
                newName: "IX_AdoptionApplication_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AdoptionApplications_PetId",
                table: "AdoptionApplication",
                newName: "IX_AdoptionApplication_PetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shelter",
                table: "Shelter",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pet",
                table: "Pet",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdoptionApplication",
                table: "AdoptionApplication",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionApplication_AspNetUsers_UserId",
                table: "AdoptionApplication",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionApplication_Pet_PetId",
                table: "AdoptionApplication",
                column: "PetId",
                principalTable: "Pet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_AspNetUsers_Shelter_ShelterId",
                table: "AspNetUsers",
                column: "ShelterId",
                principalTable: "Shelter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_Shelter_ShelterId",
                table: "Pet",
                column: "ShelterId",
                principalTable: "Shelter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
