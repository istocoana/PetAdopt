﻿// <auto-generated />
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
    [Migration("20231101153530_InitialCreate")]
    partial class InitialCreate
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

                    b.Property<string>("animal_speacies")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Animal");
                });
#pragma warning restore 612, 618
        }
    }
}
