﻿// <auto-generated />
using System;
using EpmDashboard.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EpmDashboard.Migrations
{
    [DbContext(typeof(EPMContext))]
    [Migration("20221219173508_madeProblemSolverNullableInProblems2")]
    partial class madeProblemSolverNullableInProblems2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ApplicationRoleApplicationUser", b =>
                {
                    b.Property<string>("RolesId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UsersId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("ApplicationRoleApplicationUser");
                });

            modelBuilder.Entity("EpmDashboard.Models.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("ApplicationRole");
                });

            modelBuilder.Entity("EpmDashboard.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("ApplicationUser");
                });

            modelBuilder.Entity("EpmDashboard.Models.EPM.EngineeringArea", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("School")
                        .HasColumnType("longtext");

                    b.Property<string>("area")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("EngineeringAreas");
                });

            modelBuilder.Entity("EpmDashboard.Models.EPM.Problem", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ProblemMakerid")
                        .HasColumnType("int");

                    b.Property<int?>("ProblemSolverid")
                        .HasColumnType("int");

                    b.Property<DateTime?>("createtime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_time");

                    b.Property<string>("description")
                        .HasColumnType("longtext");

                    b.Property<decimal?>("funding")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("title")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("updatetime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("update_time");

                    b.HasKey("id");

                    b.HasIndex("ProblemMakerid");

                    b.HasIndex("ProblemSolverid");

                    b.ToTable("Problem");
                });

            modelBuilder.Entity("EpmDashboard.Models.EPM.ProblemMaker", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("about")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("createtime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_time");

                    b.Property<string>("email")
                        .HasColumnType("longtext");

                    b.Property<string>("name")
                        .HasColumnType("longtext");

                    b.Property<string>("phone")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("updatetime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("update_time");

                    b.Property<string>("user_id")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.HasIndex("ApplicationUserId")
                        .IsUnique();

                    b.ToTable("ProblemMaker");
                });

            modelBuilder.Entity("EpmDashboard.Models.EPM.ProblemSolver", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("EngineeringAreasid")
                        .HasColumnType("int")
                        .HasColumnName("EngineeringAreas_id");

                    b.Property<DateTime?>("createtime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_time");

                    b.Property<string>("email")
                        .HasColumnType("longtext");

                    b.Property<string>("name")
                        .HasColumnType("longtext");

                    b.Property<string>("phone")
                        .HasColumnType("longtext");

                    b.Property<string>("profession")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("updatetime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("update_time");

                    b.Property<string>("user_id")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.HasIndex("ApplicationUserId")
                        .IsUnique();

                    b.HasIndex("EngineeringAreasid");

                    b.ToTable("ProblemSolver");
                });

            modelBuilder.Entity("ApplicationRoleApplicationUser", b =>
                {
                    b.HasOne("EpmDashboard.Models.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EpmDashboard.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EpmDashboard.Models.EPM.Problem", b =>
                {
                    b.HasOne("EpmDashboard.Models.EPM.ProblemMaker", "ProblemMaker")
                        .WithMany("Problems")
                        .HasForeignKey("ProblemMakerid");

                    b.HasOne("EpmDashboard.Models.EPM.ProblemSolver", "ProblemSolver")
                        .WithMany("Problems")
                        .HasForeignKey("ProblemSolverid");

                    b.Navigation("ProblemMaker");

                    b.Navigation("ProblemSolver");
                });

            modelBuilder.Entity("EpmDashboard.Models.EPM.ProblemMaker", b =>
                {
                    b.HasOne("EpmDashboard.Models.ApplicationUser", "ApplicationUser")
                        .WithOne("ProblemMaker")
                        .HasForeignKey("EpmDashboard.Models.EPM.ProblemMaker", "ApplicationUserId");

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("EpmDashboard.Models.EPM.ProblemSolver", b =>
                {
                    b.HasOne("EpmDashboard.Models.ApplicationUser", "ApplicationUser")
                        .WithOne("ProblemSolver")
                        .HasForeignKey("EpmDashboard.Models.EPM.ProblemSolver", "ApplicationUserId");

                    b.HasOne("EpmDashboard.Models.EPM.EngineeringArea", "EngineeringArea")
                        .WithMany("ProblemSolvers")
                        .HasForeignKey("EngineeringAreasid");

                    b.Navigation("ApplicationUser");

                    b.Navigation("EngineeringArea");
                });

            modelBuilder.Entity("EpmDashboard.Models.ApplicationUser", b =>
                {
                    b.Navigation("ProblemMaker");

                    b.Navigation("ProblemSolver");
                });

            modelBuilder.Entity("EpmDashboard.Models.EPM.EngineeringArea", b =>
                {
                    b.Navigation("ProblemSolvers");
                });

            modelBuilder.Entity("EpmDashboard.Models.EPM.ProblemMaker", b =>
                {
                    b.Navigation("Problems");
                });

            modelBuilder.Entity("EpmDashboard.Models.EPM.ProblemSolver", b =>
                {
                    b.Navigation("Problems");
                });
#pragma warning restore 612, 618
        }
    }
}
