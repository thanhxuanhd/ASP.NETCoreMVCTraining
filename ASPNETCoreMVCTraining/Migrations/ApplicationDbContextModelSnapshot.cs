﻿// <auto-generated />
using System;
using ASPNETCoreMVCTraining.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ASPNETCoreMVCTraining.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ASPNETCoreMVCTraining.Models.Dept", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Depts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "IT"
                        },
                        new
                        {
                            Id = 2,
                            Name = "HR"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Payroll"
                        });
                });

            modelBuilder.Entity("ASPNETCoreMVCTraining.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<int>("DeptId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhotoPath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DeptId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DeptId = 1,
                            Email = "evin@testemail.com",
                            Name = "Kevin"
                        },
                        new
                        {
                            Id = 2,
                            DeptId = 2,
                            Email = "John@testemail.com",
                            Name = "John"
                        });
                });

            modelBuilder.Entity("ASPNETCoreMVCTraining.Models.Employee", b =>
                {
                    b.HasOne("ASPNETCoreMVCTraining.Models.Dept", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DeptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("ASPNETCoreMVCTraining.Models.Dept", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
