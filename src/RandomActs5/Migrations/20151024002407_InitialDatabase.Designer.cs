using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using RandomActs.Models;

namespace RandomActs5.Migrations
{
    [DbContext(typeof(RAOKContext))]
    [Migration("20151024002407_InitialDatabase")]
    partial class InitialDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta8-15964")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RandomActs.Models.RandomAct", b =>
                {
                    b.Property<int>("RandomActId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndTime");

                    b.Property<string>("Location");

                    b.Property<int>("MaxActors");

                    b.Property<DateTime>("StartTime");

                    b.Property<string>("State");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("RandomActId");
                });

            modelBuilder.Entity("RandomActs.Models.RandomActActor", b =>
                {
                    b.Property<int>("RandomActActorId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Message");

                    b.Property<int>("RandomActId");

                    b.Property<int>("RandomActorId");

                    b.Property<bool>("WaitList");

                    b.Property<string>("WhatICanBring");

                    b.HasKey("RandomActActorId");
                });

            modelBuilder.Entity("RandomActs.Models.RandomActor", b =>
                {
                    b.Property<int>("RandomActorId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("TwitterHandle");

                    b.HasKey("RandomActorId");
                });

            modelBuilder.Entity("RandomActs.Models.RandomActActor", b =>
                {
                    b.HasOne("RandomActs.Models.RandomAct")
                        .WithMany()
                        .ForeignKey("RandomActId");

                    b.HasOne("RandomActs.Models.RandomActor")
                        .WithMany()
                        .ForeignKey("RandomActorId");
                });
        }
    }
}
