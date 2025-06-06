﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250118161840_AddedReview")]
    partial class AddedReview
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Backend.Models.EmploymentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double?>("From")
                        .HasColumnType("double precision");

                    b.Property<double?>("From_Chf")
                        .HasColumnType("double precision");

                    b.Property<double?>("From_Eur")
                        .HasColumnType("double precision");

                    b.Property<double?>("From_Gbp")
                        .HasColumnType("double precision");

                    b.Property<double?>("From_Pln")
                        .HasColumnType("double precision");

                    b.Property<double?>("From_Usd")
                        .HasColumnType("double precision");

                    b.Property<bool>("Gross")
                        .HasColumnType("boolean");

                    b.Property<int>("JobOfferId")
                        .HasColumnType("integer");

                    b.Property<double?>("To")
                        .HasColumnType("double precision");

                    b.Property<double?>("To_Chf")
                        .HasColumnType("double precision");

                    b.Property<double?>("To_Eur")
                        .HasColumnType("double precision");

                    b.Property<double?>("To_Gbp")
                        .HasColumnType("double precision");

                    b.Property<double?>("To_Pln")
                        .HasColumnType("double precision");

                    b.Property<double?>("To_usd")
                        .HasColumnType("double precision");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("JobOfferId");

                    b.ToTable("EmploymentTypes");
                });

            modelBuilder.Entity("Backend.Models.JobOffer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CompanyLogoThumbUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ExperienceLevel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<List<string>>("NiceToHaveSkills")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<bool>("OpenToHireUkrainians")
                        .HasColumnType("boolean");

                    b.Property<string>("PublishedAt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("RemoteInterview")
                        .HasColumnType("boolean");

                    b.Property<List<string>>("RequiredSkills")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WorkingTime")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WorkplaceType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("JobOffers");
                });

            modelBuilder.Entity("Backend.Models.MultiLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("JobOfferId")
                        .HasColumnType("integer");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("JobOfferId");

                    b.ToTable("MultiLocations");
                });

            modelBuilder.Entity("Backend.Models.EmploymentType", b =>
                {
                    b.HasOne("Backend.Models.JobOffer", "JobOffer")
                        .WithMany("EmploymentTypes")
                        .HasForeignKey("JobOfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobOffer");
                });

            modelBuilder.Entity("Backend.Models.MultiLocation", b =>
                {
                    b.HasOne("Backend.Models.JobOffer", "JobOffer")
                        .WithMany("MultiLocation")
                        .HasForeignKey("JobOfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobOffer");
                });

            modelBuilder.Entity("Backend.Models.JobOffer", b =>
                {
                    b.Navigation("EmploymentTypes");

                    b.Navigation("MultiLocation");
                });
#pragma warning restore 612, 618
        }
    }
}
