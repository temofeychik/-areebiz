﻿// <auto-generated />
using System;
using CareebizExam.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CareebizExam.DbContext.Migrations
{
    [DbContext(typeof(CareebizExamDbContext))]
    [Migration("20190916123231_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CareebizExam.Models.Rectangle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<float>("East");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<float>("North");

                    b.Property<float>("South");

                    b.Property<float>("West");

                    b.HasKey("Id");

                    b.ToTable("Rectangles");
                });
#pragma warning restore 612, 618
        }
    }
}