using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppMovie.Data.Migrations
{
    public partial class ManytomanyProducerMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieProducer");

            migrationBuilder.CreateTable(
                name: "ProducerMovies",
                columns: table => new
                {
                    ProducerId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProducerMovies", x => new { x.MovieId, x.ProducerId });
                    table.ForeignKey(
                        name: "FK_ProducerMovies_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProducerMovies_Producers_ProducerId",
                        column: x => x.ProducerId,
                        principalTable: "Producers",
                        principalColumn: "ProducerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProducerMovies_ProducerId",
                table: "ProducerMovies",
                column: "ProducerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProducerMovies");

            migrationBuilder.CreateTable(
                name: "MovieProducer",
                columns: table => new
                {
                    MoviesMovieId = table.Column<int>(type: "int", nullable: false),
                    ProducersProducerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieProducer", x => new { x.MoviesMovieId, x.ProducersProducerId });
                    table.ForeignKey(
                        name: "FK_MovieProducer_Movies_MoviesMovieId",
                        column: x => x.MoviesMovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieProducer_Producers_ProducersProducerId",
                        column: x => x.ProducersProducerId,
                        principalTable: "Producers",
                        principalColumn: "ProducerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieProducer_ProducersProducerId",
                table: "MovieProducer",
                column: "ProducersProducerId");
        }
    }
}
