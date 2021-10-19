﻿// <auto-generated />
using System;
using IceCream3.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IceCream3.Migrations
{
    [DbContext(typeof(IceCream3Context))]
    [Migration("20211019173750_Remove_Pollution")]
    partial class Remove_Pollution
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IceCream3.Models.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("IceCream3.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Flavor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HouseNum")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MeasuredTempId")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeOrdered")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MeasuredTempId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("IceCream3.Models.Temperature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Degree")
                        .HasColumnType("real");

                    b.Property<float>("Humidity")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Temperature");
                });

            modelBuilder.Entity("IceCream3.Models.Order", b =>
                {
                    b.HasOne("IceCream3.Models.Temperature", "MeasuredTemp")
                        .WithMany()
                        .HasForeignKey("MeasuredTempId");

                    b.Navigation("MeasuredTemp");
                });
#pragma warning restore 612, 618
        }
    }
}
