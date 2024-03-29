﻿// <auto-generated />
using System;
using CelupartsPoC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CelupartsPoC.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230314215856_SixtyFourMigrationCelupartsPoC")]
    partial class SixtyFourMigrationCelupartsPoC
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CelupartsPoC.Brand", b =>
                {
                    b.Property<int>("IdBrand")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdBrand"), 1L, 1);

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdTypeOfEquipment")
                        .HasColumnType("int");

                    b.HasKey("IdBrand");

                    b.HasIndex("IdTypeOfEquipment");

                    b.ToTable("Brand");
                });

            modelBuilder.Entity("CelupartsPoC.CelupartsInfo", b =>
                {
                    b.Property<int>("IdCelupartsInfo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCelupartsInfo"), 1L, 1);

                    b.Property<string>("ContactEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCelupartsInfo");

                    b.ToTable("CelupartsInfo");
                });

            modelBuilder.Entity("CelupartsPoC.Courier", b =>
                {
                    b.Property<int>("IdCourier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCourier"), 1L, 1);

                    b.Property<string>("AccountStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Names")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surnames")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCourier");

                    b.ToTable("Courier");
                });

            modelBuilder.Entity("CelupartsPoC.Equipment", b =>
                {
                    b.Property<int>("IdEquipment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEquipment"), 1L, 1);

                    b.Property<string>("EquipmentBrand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("EquipmentInvoice")
                        .HasColumnType("varbinary(max)");

                    b.Property<int?>("IdTypeOfEquipment")
                        .HasColumnType("int");

                    b.Property<string>("ImeiOrSerial")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModelOrReference")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdEquipment");

                    b.HasIndex("IdTypeOfEquipment");

                    b.ToTable("Equipment");
                });

            modelBuilder.Entity("CelupartsPoC.HomeService", b =>
                {
                    b.Property<int>("IdHomeService")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdHomeService"), 1L, 1);

                    b.Property<DateTime?>("DeliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IdCourier")
                        .HasColumnType("int");

                    b.Property<int?>("IdRequest")
                        .HasColumnType("int");

                    b.Property<DateTime>("PickUpDate")
                        .HasColumnType("datetime2");

                    b.HasKey("IdHomeService");

                    b.HasIndex("IdCourier");

                    b.HasIndex("IdRequest");

                    b.ToTable("HomeService");
                });

            modelBuilder.Entity("CelupartsPoC.PartsInfo", b =>
                {
                    b.Property<int>("IdPartsInfo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPartsInfo"), 1L, 1);

                    b.Property<string>("PartName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPartsInfo");

                    b.ToTable("PartsInfo");
                });

            modelBuilder.Entity("CelupartsPoC.PartsToRepair", b =>
                {
                    b.Property<int>("IdPartsToRepair")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPartsToRepair"), 1L, 1);

                    b.Property<int>("IdRepair")
                        .HasColumnType("int");

                    b.Property<string>("Part")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ToRepair")
                        .HasColumnType("bit");

                    b.Property<bool>("ToReplace")
                        .HasColumnType("bit");

                    b.HasKey("IdPartsToRepair");

                    b.HasIndex("IdRepair");

                    b.ToTable("PartsToRepair");
                });

            modelBuilder.Entity("CelupartsPoC.Repair", b =>
                {
                    b.Property<int>("IdRepair")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRepair"), 1L, 1);

                    b.Property<string>("DeviceDiagnostic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdRequest")
                        .HasColumnType("int");

                    b.Property<int?>("IdTechnician")
                        .HasColumnType("int");

                    b.Property<bool>("PriceReviewedByAdmin")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("RepairDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RepairDiagnostic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RepairQuote")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RepairStartDate")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan?>("RepairTime")
                        .HasColumnType("time");

                    b.HasKey("IdRepair");

                    b.HasIndex("IdRequest");

                    b.HasIndex("IdTechnician");

                    b.ToTable("Repair");
                });

            modelBuilder.Entity("CelupartsPoC.RepairPayment", b =>
                {
                    b.Property<int>("IdRepairPayment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRepairPayment"), 1L, 1);

                    b.Property<string>("BillPayment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdRepair")
                        .HasColumnType("int");

                    b.Property<DateTime?>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdRepairPayment");

                    b.HasIndex("IdRepair");

                    b.ToTable("RepairPayment");
                });

            modelBuilder.Entity("CelupartsPoC.RequestHistory", b =>
                {
                    b.Property<int>("IdRequestHistory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRequestHistory"), 1L, 1);

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IdRequest")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdRequestHistory");

                    b.HasIndex("IdRequest");

                    b.ToTable("RequestHistory");
                });

            modelBuilder.Entity("CelupartsPoC.RequestNotification", b =>
                {
                    b.Property<int>("IdRequestNotification")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRequestNotification"), 1L, 1);

                    b.Property<int?>("IdRequest")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NotificationType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("WasReviewed")
                        .HasColumnType("bit");

                    b.HasKey("IdRequestNotification");

                    b.HasIndex("IdRequest");

                    b.ToTable("RequestNotification");
                });

            modelBuilder.Entity("CelupartsPoC.RequestStatus", b =>
                {
                    b.Property<int>("IdRequestStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRequestStatus"), 1L, 1);

                    b.Property<int>("IdRequest")
                        .HasColumnType("int");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ProductReturned")
                        .HasColumnType("bit");

                    b.Property<bool>("ProductSold")
                        .HasColumnType("bit");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdRequestStatus");

                    b.HasIndex("IdRequest");

                    b.ToTable("RequestStatus");
                });

            modelBuilder.Entity("CelupartsPoC.RequestWithEquipments", b =>
                {
                    b.Property<int>("IdRequest")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRequest"), 1L, 1);

                    b.Property<string>("AlternativePhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AutoDiagnosis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeliveryAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdEquipment")
                        .HasColumnType("int");

                    b.Property<string>("IdNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdUser")
                        .HasColumnType("int");

                    b.Property<string>("Names")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PickUpAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RequestType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusQuote")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surnames")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdRequest");

                    b.HasIndex("IdEquipment");

                    b.HasIndex("IdUser");

                    b.ToTable("Request");
                });

            modelBuilder.Entity("CelupartsPoC.Retoma", b =>
                {
                    b.Property<int>("IdRetoma")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRetoma"), 1L, 1);

                    b.Property<string>("DeviceDiagnostic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdRequest")
                        .HasColumnType("int");

                    b.Property<int?>("IdTechnician")
                        .HasColumnType("int");

                    b.Property<bool>("PriceReviewedByAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("RetomaQuote")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdRetoma");

                    b.HasIndex("IdRequest");

                    b.HasIndex("IdTechnician");

                    b.ToTable("Retoma");
                });

            modelBuilder.Entity("CelupartsPoC.RetomaPayment", b =>
                {
                    b.Property<int>("IdRetomaPayment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRetomaPayment"), 1L, 1);

                    b.Property<string>("BillPaymentPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdRetoma")
                        .HasColumnType("int");

                    b.Property<DateTime?>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VoucherNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdRetomaPayment");

                    b.HasIndex("IdRetoma");

                    b.ToTable("RetomaPayment");
                });

            modelBuilder.Entity("CelupartsPoC.Technician", b =>
                {
                    b.Property<int>("IdTechnician")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTechnician"), 1L, 1);

                    b.Property<string>("AccountStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Names")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surnames")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdTechnician");

                    b.ToTable("Technician");
                });

            modelBuilder.Entity("CelupartsPoC.TypeOfEquipment", b =>
                {
                    b.Property<int>("IdTypeOfEquipment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTypeOfEquipment"), 1L, 1);

                    b.Property<string>("EquipmentTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdTypeOfEquipment");

                    b.ToTable("TypeOfEquipment");
                });

            modelBuilder.Entity("CelupartsPoC.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdUser")
                        .HasColumnType("int");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdUser");

                    b.ToTable("User");
                });

            modelBuilder.Entity("CelupartsPoC.UserDto", b =>
                {
                    b.Property<int?>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("IdUser"), 1L, 1);

                    b.Property<string>("AccountStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AlternativePhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IdNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IdType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LoginAttempts")
                        .HasColumnType("int");

                    b.Property<string>("Names")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surnames")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TokenRecovery")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUser");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("IdNumber")
                        .IsUnique();

                    b.ToTable("UsersDto");
                });

            modelBuilder.Entity("CelupartsPoC.Brand", b =>
                {
                    b.HasOne("CelupartsPoC.TypeOfEquipment", "TypeOfEquipment")
                        .WithMany()
                        .HasForeignKey("IdTypeOfEquipment")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TypeOfEquipment");
                });

            modelBuilder.Entity("CelupartsPoC.Equipment", b =>
                {
                    b.HasOne("CelupartsPoC.TypeOfEquipment", "TypeOfEquipment")
                        .WithMany()
                        .HasForeignKey("IdTypeOfEquipment");

                    b.Navigation("TypeOfEquipment");
                });

            modelBuilder.Entity("CelupartsPoC.HomeService", b =>
                {
                    b.HasOne("CelupartsPoC.Courier", "Courier")
                        .WithMany()
                        .HasForeignKey("IdCourier");

                    b.HasOne("CelupartsPoC.RequestWithEquipments", "Request")
                        .WithMany("HomeServices")
                        .HasForeignKey("IdRequest");

                    b.Navigation("Courier");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("CelupartsPoC.PartsToRepair", b =>
                {
                    b.HasOne("CelupartsPoC.Repair", "Repair")
                        .WithMany("PartsToRepair")
                        .HasForeignKey("IdRepair")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Repair");
                });

            modelBuilder.Entity("CelupartsPoC.Repair", b =>
                {
                    b.HasOne("CelupartsPoC.RequestWithEquipments", "Request")
                        .WithMany("Repairs")
                        .HasForeignKey("IdRequest")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CelupartsPoC.Technician", "Technician")
                        .WithMany()
                        .HasForeignKey("IdTechnician");

                    b.Navigation("Request");

                    b.Navigation("Technician");
                });

            modelBuilder.Entity("CelupartsPoC.RepairPayment", b =>
                {
                    b.HasOne("CelupartsPoC.Repair", "Repair")
                        .WithMany("RepairPayments")
                        .HasForeignKey("IdRepair")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Repair");
                });

            modelBuilder.Entity("CelupartsPoC.RequestHistory", b =>
                {
                    b.HasOne("CelupartsPoC.RequestWithEquipments", "Request")
                        .WithMany()
                        .HasForeignKey("IdRequest");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("CelupartsPoC.RequestNotification", b =>
                {
                    b.HasOne("CelupartsPoC.RequestWithEquipments", "Request")
                        .WithMany("RequestNotifications")
                        .HasForeignKey("IdRequest");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("CelupartsPoC.RequestStatus", b =>
                {
                    b.HasOne("CelupartsPoC.RequestWithEquipments", "Request")
                        .WithMany("RequestStatus")
                        .HasForeignKey("IdRequest")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");
                });

            modelBuilder.Entity("CelupartsPoC.RequestWithEquipments", b =>
                {
                    b.HasOne("CelupartsPoC.Equipment", "Equipment")
                        .WithMany()
                        .HasForeignKey("IdEquipment");

                    b.HasOne("CelupartsPoC.UserDto", "UserDto")
                        .WithMany("Requests")
                        .HasForeignKey("IdUser");

                    b.Navigation("Equipment");

                    b.Navigation("UserDto");
                });

            modelBuilder.Entity("CelupartsPoC.Retoma", b =>
                {
                    b.HasOne("CelupartsPoC.RequestWithEquipments", "Request")
                        .WithMany("Retoma")
                        .HasForeignKey("IdRequest")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CelupartsPoC.Technician", "Technician")
                        .WithMany()
                        .HasForeignKey("IdTechnician");

                    b.Navigation("Request");

                    b.Navigation("Technician");
                });

            modelBuilder.Entity("CelupartsPoC.RetomaPayment", b =>
                {
                    b.HasOne("CelupartsPoC.Retoma", "Retoma")
                        .WithMany("RetomaPayments")
                        .HasForeignKey("IdRetoma")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Retoma");
                });

            modelBuilder.Entity("CelupartsPoC.User", b =>
                {
                    b.HasOne("CelupartsPoC.UserDto", "UserDto")
                        .WithMany()
                        .HasForeignKey("IdUser");

                    b.Navigation("UserDto");
                });

            modelBuilder.Entity("CelupartsPoC.Repair", b =>
                {
                    b.Navigation("PartsToRepair");

                    b.Navigation("RepairPayments");
                });

            modelBuilder.Entity("CelupartsPoC.RequestWithEquipments", b =>
                {
                    b.Navigation("HomeServices");

                    b.Navigation("Repairs");

                    b.Navigation("RequestNotifications");

                    b.Navigation("RequestStatus");

                    b.Navigation("Retoma");
                });

            modelBuilder.Entity("CelupartsPoC.Retoma", b =>
                {
                    b.Navigation("RetomaPayments");
                });

            modelBuilder.Entity("CelupartsPoC.UserDto", b =>
                {
                    b.Navigation("Requests");
                });
#pragma warning restore 612, 618
        }
    }
}
