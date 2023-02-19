﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskManagement.Data;

#nullable disable

namespace TaskManagement.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TaskManagement.Models.Board", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("TaskManagement.Models.BoardColumn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BoardId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.HasIndex("UserId");

                    b.ToTable("Columns");
                });

            modelBuilder.Entity("TaskManagement.Models.BoardTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BoardColumnId")
                        .HasColumnType("int");

                    b.Property<int?>("BoardId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BoardColumnId");

                    b.HasIndex("BoardId");

                    b.HasIndex("UserId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("TaskManagement.Models.SubTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TaskId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("SubTasks");
                });

            modelBuilder.Entity("TaskManagement.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TaskManagement.Models.Board", b =>
                {
                    b.HasOne("TaskManagement.Models.UserModel", "User")
                        .WithMany("Boards")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskManagement.Models.BoardColumn", b =>
                {
                    b.HasOne("TaskManagement.Models.Board", "Board")
                        .WithMany("Columns")
                        .HasForeignKey("BoardId");

                    b.HasOne("TaskManagement.Models.UserModel", "User")
                        .WithMany("BoardsColumn")
                        .HasForeignKey("UserId");

                    b.Navigation("Board");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskManagement.Models.BoardTask", b =>
                {
                    b.HasOne("TaskManagement.Models.BoardColumn", "BoardColumn")
                        .WithMany("Tasks")
                        .HasForeignKey("BoardColumnId");

                    b.HasOne("TaskManagement.Models.Board", "Board")
                        .WithMany("Tasks")
                        .HasForeignKey("BoardId");

                    b.HasOne("TaskManagement.Models.UserModel", "User")
                        .WithMany("BoardsTask")
                        .HasForeignKey("UserId");

                    b.Navigation("Board");

                    b.Navigation("BoardColumn");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskManagement.Models.SubTask", b =>
                {
                    b.HasOne("TaskManagement.Models.BoardTask", "Task")
                        .WithMany("SubTasks")
                        .HasForeignKey("TaskId");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("TaskManagement.Models.Board", b =>
                {
                    b.Navigation("Columns");

                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("TaskManagement.Models.BoardColumn", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("TaskManagement.Models.BoardTask", b =>
                {
                    b.Navigation("SubTasks");
                });

            modelBuilder.Entity("TaskManagement.Models.UserModel", b =>
                {
                    b.Navigation("Boards");

                    b.Navigation("BoardsColumn");

                    b.Navigation("BoardsTask");
                });
#pragma warning restore 612, 618
        }
    }
}
