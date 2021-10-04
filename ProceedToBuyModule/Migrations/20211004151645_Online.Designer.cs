﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProceedToBuyModule;

namespace ProceedToBuyModule.Migrations
{
    [DbContext(typeof(CustomerProductDbContext))]
    [Migration("20211004151645_Online")]
    partial class Online
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProceedToBuyModule.Cart", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DeliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ZipCode")
                        .HasColumnType("int");

                    b.HasKey("Id", "ProductId", "DeliveryDate");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("ProceedToBuyModule.Model.CustomerWishList", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateAddedToWishList")
                        .HasColumnType("datetime2");

                    b.HasKey("Id", "ProductId", "DateAddedToWishList");

                    b.ToTable("CustomerWishLists");
                });
#pragma warning restore 612, 618
        }
    }
}