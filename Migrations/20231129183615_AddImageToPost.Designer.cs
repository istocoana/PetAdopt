﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PetAdopt.Data;

#nullable disable

namespace PetAdopt.Migrations
{
    [DbContext(typeof(PetAdoptContext))]
    [Migration("20231129183615_AddImageToPost")]
    partial class AddImageToPost
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PetAdopt.Models.Animal", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("animal_age")
                        .HasColumnType("int");

                    b.Property<string>("animal_breed")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("animal_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("animal_speacies")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Animal");
                });

            modelBuilder.Entity("PetAdopt.Models.Post", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("ImageFile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int?>("animalID")
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("animalID");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("PetAdopt.Models.Post", b =>
                {
                    b.HasOne("PetAdopt.Models.Animal", "Animal")
                        .WithMany()
                        .HasForeignKey("animalID");

                    b.Navigation("Animal");
                });
#pragma warning restore 612, 618
        }
    }
}
