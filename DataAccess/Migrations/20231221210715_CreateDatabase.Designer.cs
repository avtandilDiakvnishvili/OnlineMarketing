﻿// <auto-generated />
using System;
using DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231221210715_CreateDatabase")]
    partial class CreateDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataAccess.Models.Distributor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("birth_date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("first_name");

                    b.Property<int>("Gender")
                        .HasColumnType("int")
                        .HasColumnName("gender");

                    b.Property<string>("ImgPath")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("img_path");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("last_name");

                    b.Property<int>("RecommendedCount")
                        .HasColumnType("int")
                        .HasColumnName("recommended_count");

                    b.Property<int?>("Recommender")
                        .HasColumnType("int")
                        .HasColumnName("recommender");

                    b.HasKey("Id");

                    b.ToTable("Distributor", "dbo");
                });

            modelBuilder.Entity("DataAccess.Models.DistributorAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DistributorId")
                        .HasColumnType("int")
                        .HasColumnName("distributor_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("address");

                    b.Property<int>("Type")
                        .HasColumnType("int")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.HasIndex("DistributorId");

                    b.ToTable("Address", "dbo");
                });

            modelBuilder.Entity("DataAccess.Models.PersonalContact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ContactInfo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("contact_info");

                    b.Property<int>("DistributorId")
                        .HasColumnType("int")
                        .HasColumnName("distributor_id");

                    b.Property<int>("Type")
                        .HasColumnType("int")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.HasIndex("DistributorId");

                    b.ToTable("PersonalContact", "dbo");
                });

            modelBuilder.Entity("DataAccess.Models.PersonalDocument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Agency")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("agency");

                    b.Property<int>("DistributorId")
                        .HasColumnType("int")
                        .HasColumnName("distributor_id");

                    b.Property<string>("DocumentNumber")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("document_number");

                    b.Property<string>("DocumentSeria")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("document_seria");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("due_date");

                    b.Property<string>("PersonalNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("personal_number");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("release_date");

                    b.Property<int>("Type")
                        .HasMaxLength(50)
                        .HasColumnType("int")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.HasIndex("DistributorId");

                    b.ToTable("PersonalDocument", "dbo");
                });

            modelBuilder.Entity("DataAccess.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("code");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("unit_price");

                    b.HasKey("Id");

                    b.ToTable("Product", "dbo");
                });

            modelBuilder.Entity("DataAccess.Models.Sale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DistributorId")
                        .HasColumnType("int")
                        .HasColumnName("distributor_id");

                    b.Property<DateTime>("TDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("tdate");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("total");

                    b.HasKey("Id");

                    b.HasIndex("DistributorId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("DataAccess.Models.SalesProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("product_id");

                    b.Property<decimal>("ProductSalePrice")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("product_price");

                    b.Property<decimal>("ProductSelfCost")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("product_self_cost");

                    b.Property<int>("SaleId")
                        .HasColumnType("int")
                        .HasColumnName("sale_id");

                    b.HasKey("Id");

                    b.HasIndex("SaleId");

                    b.ToTable("SalesProduct");
                });

            modelBuilder.Entity("DataAccess.Models.DistributorAddress", b =>
                {
                    b.HasOne("DataAccess.Models.Distributor", "Distributor")
                        .WithMany("DistributorAddress")
                        .HasForeignKey("DistributorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Distributor");
                });

            modelBuilder.Entity("DataAccess.Models.PersonalContact", b =>
                {
                    b.HasOne("DataAccess.Models.Distributor", "Distributor")
                        .WithMany("PersonalContacts")
                        .HasForeignKey("DistributorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Distributor");
                });

            modelBuilder.Entity("DataAccess.Models.PersonalDocument", b =>
                {
                    b.HasOne("DataAccess.Models.Distributor", "Distributor")
                        .WithMany("PersonalDocuments")
                        .HasForeignKey("DistributorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Distributor");
                });

            modelBuilder.Entity("DataAccess.Models.Sale", b =>
                {
                    b.HasOne("DataAccess.Models.Distributor", "Distributor")
                        .WithMany()
                        .HasForeignKey("DistributorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Distributor");
                });

            modelBuilder.Entity("DataAccess.Models.SalesProduct", b =>
                {
                    b.HasOne("DataAccess.Models.Sale", "Sale")
                        .WithMany("ProductList")
                        .HasForeignKey("SaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sale");
                });

            modelBuilder.Entity("DataAccess.Models.Distributor", b =>
                {
                    b.Navigation("DistributorAddress");

                    b.Navigation("PersonalContacts");

                    b.Navigation("PersonalDocuments");
                });

            modelBuilder.Entity("DataAccess.Models.Sale", b =>
                {
                    b.Navigation("ProductList");
                });
#pragma warning restore 612, 618
        }
    }
}
