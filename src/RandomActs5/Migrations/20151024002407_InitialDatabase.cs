using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace RandomActs.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RandomAct",
                columns: table => new
                {
                    RandomActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    MaxActors = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    State = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RandomAct", x => x.RandomActId);
                });
            migrationBuilder.CreateTable(
                name: "RandomActor",
                columns: table => new
                {
                    RandomActorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    TwitterHandle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RandomActor", x => x.RandomActorId);
                });
            migrationBuilder.CreateTable(
                name: "RandomActActor",
                columns: table => new
                {
                    RandomActActorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Message = table.Column<string>(nullable: true),
                    RandomActId = table.Column<int>(nullable: false),
                    RandomActorId = table.Column<int>(nullable: false),
                    WaitList = table.Column<bool>(nullable: false),
                    WhatICanBring = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RandomActActor", x => x.RandomActActorId);
                    table.ForeignKey(
                        name: "FK_RandomActActor_RandomAct_RandomActId",
                        column: x => x.RandomActId,
                        principalTable: "RandomAct",
                        principalColumn: "RandomActId");
                    table.ForeignKey(
                        name: "FK_RandomActActor_RandomActor_RandomActorId",
                        column: x => x.RandomActorId,
                        principalTable: "RandomActor",
                        principalColumn: "RandomActorId");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("RandomActActor");
            migrationBuilder.DropTable("RandomAct");
            migrationBuilder.DropTable("RandomActor");
        }
    }
}
