using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GPA = table.Column<float>(type: "real", nullable: false),
                    School = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FullName" },
                values: new object[,]
                {
                    { 1, "Ahmed Mostafa" },
                    { 2, "Mariam Adel" },
                    { 3, "Omar Khaled" },
                    { 4, "Nour Samir" },
                    { 5, "Salma Hany" }
                });

            migrationBuilder.InsertData(
                table: "Emails",
                columns: new[] { "Id", "EmailAddress", "UserId" },
                values: new object[,]
                {
                    { 1, "ahmed.mostafa@example.com", 1 },
                    { 2, "ahmed.personal@example.com", 1 },
                    { 3, "mariam.adel@example.com", 2 },
                    { 4, "omar.khaled@example.com", 3 },
                    { 5, "nour.samir@example.com", 4 },
                    { 6, "nour.work@example.com", 4 },
                    { 7, "salma.hany@example.com", 5 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "GPA", "School", "UserId" },
                values: new object[,]
                {
                    { "202100101", 3.45f, "Engineering", 1 },
                    { "202100102", 3.82f, "Computational Sciences and AI", 2 },
                    { "202100103", 2.95f, "Science", 3 },
                    { "202100104", 3.6f, "Business", 4 },
                    { "202100105", 3.1f, "Engineering", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Emails_UserId",
                table: "Emails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                table: "Students",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
