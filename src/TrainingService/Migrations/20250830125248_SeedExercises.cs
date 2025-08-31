using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainingService.Migrations
{
    /// <inheritdoc />
    public partial class SeedExercises : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "CreatedAt", "ImageUrl", "MuscleGroups", "Name", "RequiresWeight", "UpdatedAt", "VideoUrl" },
                values: new object[,]
                {
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96400"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 73, "Supino Reto", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96401"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 74, "Supino Inclinado", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96402"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 76, "Supino Declinado", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96403"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 73, "Supino com Halteres", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96404"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 65, "Crucifixo com Halteres", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96405"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 65, "Crucifixo no Cabo (Peck-Deck/Crossover)", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96406"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 79, "Flexão de Braço", false, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96407"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 73, "Flexão Diamante", false, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96408"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 73, "Supino Fechado", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96409"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 9264, "Puxada na Frente (Pulldown)", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9640a"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 9776, "Barra Fixa (Pronada)", false, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9640b"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 9264, "Barra Fixa (Supinada)", false, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9640c"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 14384, "Remada Curvada com Barra", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9640d"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 10288, "Remada Unilateral com Halter", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9640e"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 10288, "Remada Baixa (Cabo)", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9640f"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 9217, "Pullover (Cabo/Halter)", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96410"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 791072, "Levantamento Terra", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96411"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 712, "Desenvolvimento Militar (Barra)", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96412"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 200, "Desenvolvimento com Halteres", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96413"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 640, "Elevação Lateral", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96414"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 64, "Elevação Frontal", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96415"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1792, "Elevação Posterior (Peck-Deck Inverso)", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96416"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1792, "Face Pull", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96417"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 704, "Remada Alta", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96418"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1568, "Encolhimento", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96419"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 48, "Rosca Direta (Barra)", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9641a"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 48, "Rosca Alternada (Halteres)", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9641b"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 48, "Rosca Martelo", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9641c"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 48, "Rosca Scott", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9641d"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 48, "Rosca Inversa", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9641e"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 16, "Rosca Concentrada", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9641f"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 72, "Tríceps Testa", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96420"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 40, "Tríceps Corda", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96421"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 8, "Tríceps Francês (Halter)", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96422"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 73, "Paralelas (Dips)", false, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96423"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 264, "Kickback de Tríceps", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96424"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 921600, "Agachamento Livre", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96425"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 918528, "Agachamento Frontal (Front Squat)", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96426"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 655360, "Hack Squat", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96427"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 917504, "Leg Press 45°", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96428"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 917504, "Passada (Avanço)", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96429"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 131072, "Cadeira Extensora", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9642a"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 262144, "Mesa Flexora", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9642b"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 790528, "Stiff (Terra Romeno)", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9642c"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 918016, "Levantamento Terra Sumo", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9642d"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 790528, "Elevação Pélvica", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9642e"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1966080, "Subida no Banco", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9642f"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1048576, "Panturrilha em Pé (Máquina)", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96430"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1048576, "Panturrilha Sentado", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96431"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 786432, "Elevação de Quadril no Cabo", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96432"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2129920, "Flexão de Quadril (Cabo)", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96433"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 16384, "Abdominal Supra", false, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96434"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2129920, "Abdominal Infra", false, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96435"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2129920, "Elevação de Pernas", false, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96436"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 114688, "Prancha", false, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96437"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 114688, "Prancha Lateral", false, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96438"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2211840, "Ab Wheel (Roda)", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96439"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 81920, "Russian Twist", false, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9643a"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2195456, "Mountain Climbers", false, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9643b"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 32, "Rosca de Punho", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9643c"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 32, "Rosca de Punho Inversa", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9643d"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1966624, "Farmer's Walk", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9643e"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4194303, "Burpee", false, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9643f"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 823360, "Kettlebell Swing", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96440"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4194303, "Clean and Press (Haltere/Barra)", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96441"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 10288, "Puxada Neutra (Triângulo)", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96442"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 14352, "Remada Cavalinho (T-Bar)", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96443"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 66, "Crucifixo Inclinado", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96444"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 9728, "Pulldown com Barra Reta (Pulldown no Cabo)", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96445"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 200, "Desenvolvimento Arnold", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96446"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 790528, "Good Morning", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96447"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1966080, "Box Jump", false, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96448"), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 917504, "Afundo Búlgaro", true, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96400"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96401"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96402"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96403"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96404"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96405"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96406"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96407"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96408"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96409"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9640a"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9640b"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9640c"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9640d"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9640e"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9640f"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96410"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96411"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96412"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96413"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96414"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96415"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96416"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96417"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96418"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96419"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9641a"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9641b"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9641c"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9641d"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9641e"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9641f"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96420"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96421"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96422"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96423"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96424"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96425"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96426"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96427"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96428"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96429"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9642a"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9642b"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9642c"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9642d"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9642e"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9642f"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96430"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96431"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96432"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96433"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96434"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96435"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96436"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96437"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96438"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96439"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9643a"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9643b"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9643c"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9643d"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9643e"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc9643f"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96440"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96441"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96442"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96443"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96444"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96445"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96446"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96447"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("6f9619ff-8b86-d011-b42d-00cf4fc96448"));
        }
    }
}
