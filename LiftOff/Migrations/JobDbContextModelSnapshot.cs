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

            modelBuilder.Entity("LiftOff.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<int?>("JobId");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("ApplicationUser");
                });

            modelBuilder.Entity("LiftOff.Models.Benefit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BenefitName");

                    b.Property<int>("JobId");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("Benefits");
                });

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

            modelBuilder.Entity("LiftOff.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("JobId1");

                    b.Property<string>("TagName");

                    b.HasKey("Id");

                    b.HasIndex("JobId1");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("LiftOff.Models.ApplicationUser", b =>
                {
                    b.HasOne("LiftOff.Models.Job")
                        .WithMany("Applicants")
                        .HasForeignKey("JobId");
                });

            modelBuilder.Entity("LiftOff.Models.Benefit", b =>
                {
                    b.HasOne("LiftOff.Models.Job")
                        .WithMany("Benefits")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LiftOff.Models.Requirement", b =>
                {
                    b.HasOne("LiftOff.Models.Job")
                        .WithMany("Requirements")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LiftOff.Models.Tag", b =>
                {
                    b.HasOne("LiftOff.Models.Job", "JobId")
                        .WithMany("Tags")
                        .HasForeignKey("JobId1");
                });
#pragma warning restore 612, 618
        }
    }
}
