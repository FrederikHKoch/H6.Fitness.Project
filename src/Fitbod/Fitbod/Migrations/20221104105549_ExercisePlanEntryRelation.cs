using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitbod.Migrations
{
    public partial class ExercisePlanEntryRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_ExercisePlan_ExercisePlanId",
                table: "Exercise");

            migrationBuilder.DropIndex(
                name: "IX_Exercise_ExercisePlanId",
                table: "Exercise");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Exercise");

            migrationBuilder.DropColumn(
                name: "ExercisePlanId",
                table: "Exercise");

            migrationBuilder.DropColumn(
                name: "Repetition",
                table: "Exercise");

            migrationBuilder.DropColumn(
                name: "Set",
                table: "Exercise");

            migrationBuilder.CreateTable(
                name: "ExercisePlanEntry",
                columns: table => new
                {
                    EntryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Repetitions = table.Column<int>(type: "int", nullable: false),
                    Sets = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: true),
                    ExercisePlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExercisePlanEntry", x => x.EntryId);
                    table.ForeignKey(
                        name: "FK_ExercisePlanEntry_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "ExerciseId");
                    table.ForeignKey(
                        name: "FK_ExercisePlanEntry_ExercisePlan_ExercisePlanId",
                        column: x => x.ExercisePlanId,
                        principalTable: "ExercisePlan",
                        principalColumn: "ExercisePlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExercisePlanEntry_ExerciseId",
                table: "ExercisePlanEntry",
                column: "ExerciseId",
                unique: true,
                filter: "[ExerciseId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ExercisePlanEntry_ExercisePlanId",
                table: "ExercisePlanEntry",
                column: "ExercisePlanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExercisePlanEntry");

            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "Exercise",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ExercisePlanId",
                table: "Exercise",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Repetition",
                table: "Exercise",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Set",
                table: "Exercise",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_ExercisePlanId",
                table: "Exercise",
                column: "ExercisePlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_ExercisePlan_ExercisePlanId",
                table: "Exercise",
                column: "ExercisePlanId",
                principalTable: "ExercisePlan",
                principalColumn: "ExercisePlanId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
