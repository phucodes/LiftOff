﻿// <auto-generated />
using LiftOff.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace LiftOff.Migrations
{
    [DbContext(typeof(JobDbContext))]
    partial class JobDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LiftOff.Models.Job", b =>
                {
                    b.Property<int>("JobId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DatePosted");

                    b.Property<string>("Description");

                    b.Property<string>("Employer");

                    b.Property<bool>("IsOpened");

                    b.Property<string>("Location");

                    b.Property<string>("Name");

                    b.Property<string>("PositionLevel");

                    b.Property<string>("PositionType");

                    b.HasKey("JobId");

                    b.ToTable("Job");
                });

            modelBuilder.Entity("LiftOff.Models.Requirement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("JobId");

                    b.Property<string>("RequirementName");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("Requirements");
                });

            modelBuilder.Entity("LiftOff.Models.Requirement", b =>
                {
                    b.HasOne("LiftOff.Models.Job")
                        .WithMany("Requirements")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
