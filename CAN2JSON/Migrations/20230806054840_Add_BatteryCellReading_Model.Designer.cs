﻿// <auto-generated />
using System;
using CAN2JSON.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CAN2JSON.Migrations
{
    [DbContext(typeof(Can2JsonContext))]
    [Migration("20230806054840_Add_BatteryCellReading_Model")]
    partial class Add_BatteryCellReading_Model
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.9");

            modelBuilder.Entity("CAN2JSON.Data.Models.BatteryCellReading", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Cell01")
                        .HasColumnType("decimal(4, 3)");

                    b.Property<decimal>("Cell02")
                        .HasColumnType("decimal(4, 3)");

                    b.Property<decimal>("Cell03")
                        .HasColumnType("decimal(4, 3)");

                    b.Property<decimal>("Cell04")
                        .HasColumnType("decimal(4, 3)");

                    b.Property<decimal>("Cell05")
                        .HasColumnType("decimal(4, 3)");

                    b.Property<decimal>("Cell06")
                        .HasColumnType("decimal(4, 3)");

                    b.Property<decimal>("Cell07")
                        .HasColumnType("decimal(4, 3)");

                    b.Property<decimal>("Cell08")
                        .HasColumnType("decimal(4, 3)");

                    b.Property<decimal>("Cell09")
                        .HasColumnType("decimal(4, 3)");

                    b.Property<decimal>("Cell10")
                        .HasColumnType("decimal(4, 3)");

                    b.Property<decimal>("Cell11")
                        .HasColumnType("decimal(4, 3)");

                    b.Property<decimal>("Cell12")
                        .HasColumnType("decimal(4, 3)");

                    b.Property<decimal>("Cell13")
                        .HasColumnType("decimal(4, 3)");

                    b.Property<decimal>("Cell14")
                        .HasColumnType("decimal(4, 3)");

                    b.Property<decimal>("Cell15")
                        .HasColumnType("decimal(4, 3)");

                    b.Property<decimal>("Cell16")
                        .HasColumnType("decimal(4, 3)");

                    b.Property<int>("Date")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("MinPos")
                        .HasColumnType("decimal(0, 0)");

                    b.Property<int>("SlaveNumber")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("BatteryCellReadings");
                });

            modelBuilder.Entity("CAN2JSON.Data.Models.BatteryReading", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("BatteryCurrent")
                        .HasColumnType("decimal(3, 1)");

                    b.Property<decimal>("BatteryVoltage")
                        .HasColumnType("decimal(3, 1)");

                    b.Property<int?>("BmsReadingId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("CellVoltageDelta")
                        .HasColumnType("decimal(4, 3)");

                    b.Property<decimal>("CellVoltageHigh")
                        .HasColumnType("decimal(4, 3)");

                    b.Property<decimal>("CellVoltageLow")
                        .HasColumnType("decimal(4, 3)");

                    b.Property<decimal>("ChargedTotal")
                        .HasColumnType("decimal(7, 3)");

                    b.Property<decimal>("CurrentLimit")
                        .HasColumnType("decimal(3, 1)");

                    b.Property<decimal>("CurrentLimitMax")
                        .HasColumnType("decimal(3, 1)");

                    b.Property<decimal>("Cycles")
                        .HasColumnType("decimal(4, 3)");

                    b.Property<int>("Date")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("DischargedTotal")
                        .HasColumnType("decimal(7, 3)");

                    b.Property<int>("SlaveNumber")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("StateOfCharge")
                        .HasColumnType("decimal(3, 1)");

                    b.Property<decimal>("StateOfHealth")
                        .HasColumnType("decimal(3, 1)");

                    b.Property<decimal>("TemperatureMos")
                        .HasColumnType("decimal(3, 1)");

                    b.Property<decimal>("TemperatureOne")
                        .HasColumnType("decimal(3, 1)");

                    b.Property<decimal>("TemperatureTwo")
                        .HasColumnType("decimal(3, 1)");

                    b.HasKey("Id");

                    b.HasIndex("BmsReadingId");

                    b.ToTable("BatteryReadings");
                });

            modelBuilder.Entity("CAN2JSON.Data.Models.BmsReading", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amps")
                        .HasColumnType("decimal(3, 1)");

                    b.Property<decimal>("BatteryCapacity")
                        .HasColumnType("decimal(3, 1)");

                    b.Property<decimal>("BatteryCutoffVoltage")
                        .HasColumnType("decimal(3, 1)");

                    b.Property<decimal>("BmsTemperatureHigh")
                        .HasColumnType("decimal(3, 1)");

                    b.Property<decimal>("BmsTemperatureLow")
                        .HasColumnType("decimal(3, 1)");

                    b.Property<decimal>("CellVoltageDelta")
                        .HasColumnType("decimal(4, 3)");

                    b.Property<decimal>("CellVoltageHigh")
                        .HasColumnType("decimal(4, 3)");

                    b.Property<decimal>("CellVoltageLow")
                        .HasColumnType("decimal(4, 3)");

                    b.Property<decimal>("ChargeCurrentLimit")
                        .HasColumnType("decimal(3, 0)");

                    b.Property<decimal>("ChargeCurrentLimitMax")
                        .HasColumnType("decimal(3, 0)");

                    b.Property<decimal>("ChargeVoltage")
                        .HasColumnType("decimal(3, 1)");

                    b.Property<decimal>("CurrentLimit")
                        .HasColumnType("decimal(3, 1)");

                    b.Property<int>("Date")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("DischargeLimit")
                        .HasColumnType("decimal(3, 1)");

                    b.Property<decimal>("FullChargedRestingVoltage")
                        .HasColumnType("decimal(3, 1)");

                    b.Property<decimal>("StateOfCharge")
                        .HasColumnType("decimal(3, 1)");

                    b.Property<decimal>("StateOfHealth")
                        .HasColumnType("decimal(3, 1)");

                    b.Property<decimal>("Temperature")
                        .HasColumnType("decimal(3, 1)");

                    b.Property<decimal>("Voltage")
                        .HasColumnType("decimal(3, 1)");

                    b.Property<decimal>("Watts")
                        .HasColumnType("decimal(5, 1)");

                    b.HasKey("Id");

                    b.ToTable("BmsReadings");
                });

            modelBuilder.Entity("CAN2JSON.Data.Models.BatteryReading", b =>
                {
                    b.HasOne("CAN2JSON.Data.Models.BmsReading", null)
                        .WithMany("BatteryReadings")
                        .HasForeignKey("BmsReadingId");
                });

            modelBuilder.Entity("CAN2JSON.Data.Models.BmsReading", b =>
                {
                    b.Navigation("BatteryReadings");
                });
#pragma warning restore 612, 618
        }
    }
}
