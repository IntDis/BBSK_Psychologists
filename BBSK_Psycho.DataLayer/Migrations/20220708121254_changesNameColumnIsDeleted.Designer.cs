﻿// <auto-generated />
using System;
using BBSK_Psycho.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BBSK_Psycho.DataLayer.Migrations
{
    [DbContext(typeof(BBSK_PsychoContext))]
    [Migration("20220708121254_changesNameColumnIsDeleted")]
    partial class changesNameColumnIsDeleted
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BBSK_Psycho.DataLayer.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(140)
                        .HasColumnType("nvarchar(140)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(140)
                        .HasColumnType("nvarchar(140)");

                    b.HasKey("Id");

                    b.ToTable("Client", (string)null);
                });

            modelBuilder.Entity("BBSK_Psycho.DataLayer.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("PsychologistId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("PsychologistId");

                    b.ToTable("Comment", (string)null);
                });

            modelBuilder.Entity("BBSK_Psycho.DataLayer.Entities.Education", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("EducationData")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("PsychologistId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PsychologistId");

                    b.ToTable("Education", (string)null);
                });

            modelBuilder.Entity("BBSK_Psycho.DataLayer.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<decimal>("Cost")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderPaymentStatus")
                        .HasColumnType("int");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("PayDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PsychologistId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SessionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("PsychologistId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("BBSK_Psycho.DataLayer.Entities.Problem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ProblemName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Problem", (string)null);
                });

            modelBuilder.Entity("BBSK_Psycho.DataLayer.Entities.Psychologist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CheckStatus")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(140)
                        .HasColumnType("nvarchar(140)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasportData")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(140)
                        .HasColumnType("nvarchar(140)");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<decimal>("Price")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.Property<int?>("WorkExperience")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Psychologist", (string)null);
                });

            modelBuilder.Entity("BBSK_Psycho.DataLayer.Entities.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("PsychologistId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PsychologistId");

                    b.ToTable("Schedule", (string)null);
                });

            modelBuilder.Entity("BBSK_Psycho.DataLayer.Entities.TherapyMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("TherapyMethod", (string)null);
                });

            modelBuilder.Entity("ProblemPsychologist", b =>
                {
                    b.Property<int>("ProblemsId")
                        .HasColumnType("int");

                    b.Property<int>("PsychologistsId")
                        .HasColumnType("int");

                    b.HasKey("ProblemsId", "PsychologistsId");

                    b.HasIndex("PsychologistsId");

                    b.ToTable("ProblemPsychologist");
                });

            modelBuilder.Entity("PsychologistTherapyMethod", b =>
                {
                    b.Property<int>("PsychologistsId")
                        .HasColumnType("int");

                    b.Property<int>("TherapyMethodsId")
                        .HasColumnType("int");

                    b.HasKey("PsychologistsId", "TherapyMethodsId");

                    b.HasIndex("TherapyMethodsId");

                    b.ToTable("PsychologistTherapyMethod");
                });

            modelBuilder.Entity("BBSK_Psycho.DataLayer.Entities.Comment", b =>
                {
                    b.HasOne("BBSK_Psycho.DataLayer.Entities.Client", "Client")
                        .WithMany("Comments")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BBSK_Psycho.DataLayer.Entities.Psychologist", "Psychologist")
                        .WithMany("Comments")
                        .HasForeignKey("PsychologistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Psychologist");
                });

            modelBuilder.Entity("BBSK_Psycho.DataLayer.Entities.Education", b =>
                {
                    b.HasOne("BBSK_Psycho.DataLayer.Entities.Psychologist", "Psychologist")
                        .WithMany("Educations")
                        .HasForeignKey("PsychologistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Psychologist");
                });

            modelBuilder.Entity("BBSK_Psycho.DataLayer.Entities.Order", b =>
                {
                    b.HasOne("BBSK_Psycho.DataLayer.Entities.Client", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BBSK_Psycho.DataLayer.Entities.Psychologist", "Psychologist")
                        .WithMany("Orders")
                        .HasForeignKey("PsychologistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Psychologist");
                });

            modelBuilder.Entity("BBSK_Psycho.DataLayer.Entities.Schedule", b =>
                {
                    b.HasOne("BBSK_Psycho.DataLayer.Entities.Psychologist", "Psychologist")
                        .WithMany("Schedules")
                        .HasForeignKey("PsychologistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Psychologist");
                });

            modelBuilder.Entity("ProblemPsychologist", b =>
                {
                    b.HasOne("BBSK_Psycho.DataLayer.Entities.Problem", null)
                        .WithMany()
                        .HasForeignKey("ProblemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BBSK_Psycho.DataLayer.Entities.Psychologist", null)
                        .WithMany()
                        .HasForeignKey("PsychologistsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PsychologistTherapyMethod", b =>
                {
                    b.HasOne("BBSK_Psycho.DataLayer.Entities.Psychologist", null)
                        .WithMany()
                        .HasForeignKey("PsychologistsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BBSK_Psycho.DataLayer.Entities.TherapyMethod", null)
                        .WithMany()
                        .HasForeignKey("TherapyMethodsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BBSK_Psycho.DataLayer.Entities.Client", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("BBSK_Psycho.DataLayer.Entities.Psychologist", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Educations");

                    b.Navigation("Orders");

                    b.Navigation("Schedules");
                });
#pragma warning restore 612, 618
        }
    }
}
