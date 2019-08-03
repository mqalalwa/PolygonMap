using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PolygonMap.API.Migrations
{
    public partial class PolygonMapDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shape",
                columns: table => new
                {
                    ShapeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    FixedLongitude = table.Column<float>(nullable: false),
                    FixedLatitude = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shape", x => x.ShapeID);
                });

            migrationBuilder.CreateTable(
                name: "Point",
                columns: table => new
                {
                    PointID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ShapeID = table.Column<int>(nullable: false),
                    Longitude = table.Column<float>(nullable: false),
                    Latitude = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Point", x => x.PointID);
                    table.ForeignKey(
                        name: "FK_Point_Shape_ShapeID",
                        column: x => x.ShapeID,
                        principalTable: "Shape",
                        principalColumn: "ShapeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Polygon",
                columns: table => new
                {
                    PolygonID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ShapeID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    RealLongitude = table.Column<float>(nullable: false),
                    RealLatitude = table.Column<float>(nullable: false),
                    Points = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polygon", x => x.PolygonID);
                    table.ForeignKey(
                        name: "FK_Polygon_Shape_ShapeID",
                        column: x => x.ShapeID,
                        principalTable: "Shape",
                        principalColumn: "ShapeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Shape",
                columns: new[] { "ShapeID", "FixedLatitude", "FixedLongitude", "Name" },
                values: new object[,]
                {
                    { 1, 31.95229f, 35.25868f, "Triangle" },
                    { 2, 31.95229f, 35.25868f, "Quadrilateral" },
                    { 3, 31.95229f, 35.25868f, "Pentagon" },
                    { 4, 31.95229f, 35.25868f, "Hexagon" }
                });

            migrationBuilder.InsertData(
                table: "Point",
                columns: new[] { "PointID", "Latitude", "Longitude", "ShapeID" },
                values: new object[,]
                {
                    { 1, 31.96356f, 35.26228f, 1 },
                    { 16, 31.9402f, 35.26005f, 4 },
                    { 15, 31.94705f, 35.26795f, 4 },
                    { 14, 31.9552f, 35.26743f, 4 },
                    { 13, 31.96176f, 35.25919f, 4 },
                    { 12, 31.95477f, 35.2513f, 3 },
                    { 11, 31.96103f, 35.25954f, 3 },
                    { 10, 31.95491f, 35.26778f, 3 },
                    { 9, 31.94865f, 35.26778f, 3 },
                    { 8, 31.9485f, 35.2513f, 3 },
                    { 7, 31.94865f, 35.26778f, 2 },
                    { 6, 31.94836f, 35.2513f, 2 },
                    { 5, 31.96074f, 35.2513f, 2 },
                    { 4, 31.96074f, 35.26778f, 2 },
                    { 3, 31.95366f, 35.26966f, 1 },
                    { 2, 31.95322f, 35.2513f, 1 },
                    { 17, 31.94676f, 35.2513f, 4 },
                    { 18, 31.95535f, 35.25061f, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Point_ShapeID",
                table: "Point",
                column: "ShapeID");

            migrationBuilder.CreateIndex(
                name: "IX_Polygon_ShapeID",
                table: "Polygon",
                column: "ShapeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Point");

            migrationBuilder.DropTable(
                name: "Polygon");

            migrationBuilder.DropTable(
                name: "Shape");
        }
    }
}
