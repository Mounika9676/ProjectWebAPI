using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectWebAPI.Migrations
{
    public partial class New : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    OrgID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrgName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.OrgID);
                });

            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    SiteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrgID = table.Column<int>(type: "int", nullable: false),
                    SiteName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.SiteID);
                    table.ForeignKey(
                        name: "FK_Sites_Organizations_OrgID",
                        column: x => x.OrgID,
                        principalTable: "Organizations",
                        principalColumn: "OrgID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteID = table.Column<int>(type: "int", nullable: false),
                    SubjectName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectID);
                    table.ForeignKey(
                        name: "FK_Subjects_Sites_SiteID",
                        column: x => x.SiteID,
                        principalTable: "Sites",
                        principalColumn: "SiteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestStructures",
                columns: table => new
                {
                    TestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteID = table.Column<int>(type: "int", nullable: false),
                    TestName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NoOfQuestions = table.Column<int>(type: "int", nullable: false),
                    TotalMarks = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestStructures", x => x.TestID);
                    table.ForeignKey(
                        name: "FK_TestStructures_Sites_SiteID",
                        column: x => x.SiteID,
                        principalTable: "Sites",
                        principalColumn: "SiteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "char(5)", maxLength: 5, nullable: false),
                    Username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Role = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    SiteID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Sites_SiteID",
                        column: x => x.SiteID,
                        principalTable: "Sites",
                        principalColumn: "SiteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionBanks",
                columns: table => new
                {
                    QuestionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectID = table.Column<int>(type: "int", nullable: false),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionBanks", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK_QuestionBanks_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignedTests",
                columns: table => new
                {
                    AssignmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestID = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "char(5)", nullable: false),
                    ScheduledDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedTests", x => x.AssignmentID);
                    table.ForeignKey(
                        name: "FK_AssignedTests_TestStructures_TestID",
                        column: x => x.TestID,
                        principalTable: "TestStructures",
                        principalColumn: "TestID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignedTests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "UserResponses",
                columns: table => new
                {
                    ResponseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestID = table.Column<int>(type: "int", nullable: false),
                    QuestionID = table.Column<int>(type: "int", nullable: false),
                    UserAnswer = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserResponses", x => x.ResponseID);
                    table.ForeignKey(
                        name: "FK_UserResponses_QuestionBanks_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "QuestionBanks",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserResponses_TestStructures_TestID",
                        column: x => x.TestID,
                        principalTable: "TestStructures",
                        principalColumn: "TestID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignedTests_TestID",
                table: "AssignedTests",
                column: "TestID");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedTests_UserId",
                table: "AssignedTests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBanks_SubjectID",
                table: "QuestionBanks",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Sites_OrgID",
                table: "Sites",
                column: "OrgID");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_SiteID",
                table: "Subjects",
                column: "SiteID");

            migrationBuilder.CreateIndex(
                name: "IX_TestStructures_SiteID",
                table: "TestStructures",
                column: "SiteID");

            migrationBuilder.CreateIndex(
                name: "IX_UserResponses_QuestionID",
                table: "UserResponses",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_UserResponses_TestID",
                table: "UserResponses",
                column: "TestID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SiteID",
                table: "Users",
                column: "SiteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignedTests");

            migrationBuilder.DropTable(
                name: "UserResponses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "QuestionBanks");

            migrationBuilder.DropTable(
                name: "TestStructures");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Sites");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
