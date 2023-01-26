﻿// <auto-generated />
using System;
using EfCore7.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EfCore7.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("EfCore7.Entities.Employee", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Added")
                        .HasColumnType("TEXT");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsOld")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsDescending();

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("EfCore7.Entities.Employee", b =>
                {
                    b.OwnsOne("EfCore7.Entities.ContactDetails", "ContactDetails", b1 =>
                        {
                            b1.Property<long>("EmployeeId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("PhoneNumber")
                                .HasMaxLength(999999999)
                                .HasColumnType("TEXT");

                            b1.HasKey("EmployeeId");

                            b1.ToTable("Employees");

                            b1.WithOwner()
                                .HasForeignKey("EmployeeId");

                            b1.OwnsOne("EfCore7.Entities.Address", "Address", b2 =>
                                {
                                    b2.Property<long>("ContactDetailsEmployeeId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<string>("City")
                                        .IsRequired()
                                        .HasColumnType("TEXT");

                                    b2.Property<string>("Country")
                                        .IsRequired()
                                        .HasColumnType("TEXT");

                                    b2.Property<string>("Postcode")
                                        .IsRequired()
                                        .HasColumnType("TEXT");

                                    b2.Property<string>("Street")
                                        .IsRequired()
                                        .HasColumnType("TEXT");

                                    b2.HasKey("ContactDetailsEmployeeId");

                                    b2.ToTable("Employees");

                                    b2.WithOwner()
                                        .HasForeignKey("ContactDetailsEmployeeId");
                                });

                            b1.Navigation("Address")
                                .IsRequired();
                        });

                    b.Navigation("ContactDetails")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}