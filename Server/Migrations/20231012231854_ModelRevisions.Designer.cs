﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server.Data;

#nullable disable

namespace Server.Migrations
{
    [DbContext(typeof(BoardContext))]
    [Migration("20231012231854_ModelRevisions")]
    partial class ModelRevisions
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.12");

            modelBuilder.Entity("Server.Models.BlazorBoard", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("BlazorBoards");
                });

            modelBuilder.Entity("Server.Models.Board", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("BlazorBoardId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BlazorBoardId");

                    b.ToTable("Board");
                });

            modelBuilder.Entity("Server.Models.Checklist", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDone")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("TaskId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("Checklist");
                });

            modelBuilder.Entity("Server.Models.Label", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Background")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("BlazorBoardId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("TaskId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BlazorBoardId");

                    b.HasIndex("TaskId");

                    b.ToTable("Label");
                });

            modelBuilder.Entity("Server.Models.Task", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("BoardId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(10000)
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.ToTable("Task");
                });

            modelBuilder.Entity("Server.Models.Board", b =>
                {
                    b.HasOne("Server.Models.BlazorBoard", null)
                        .WithMany("Boards")
                        .HasForeignKey("BlazorBoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Server.Models.Checklist", b =>
                {
                    b.HasOne("Server.Models.Task", null)
                        .WithMany("Checklist")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Server.Models.Label", b =>
                {
                    b.HasOne("Server.Models.BlazorBoard", null)
                        .WithMany("Labels")
                        .HasForeignKey("BlazorBoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Models.Task", null)
                        .WithMany("Labels")
                        .HasForeignKey("TaskId");
                });

            modelBuilder.Entity("Server.Models.Task", b =>
                {
                    b.HasOne("Server.Models.Board", null)
                        .WithMany("Tasks")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Server.Models.BlazorBoard", b =>
                {
                    b.Navigation("Boards");

                    b.Navigation("Labels");
                });

            modelBuilder.Entity("Server.Models.Board", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("Server.Models.Task", b =>
                {
                    b.Navigation("Checklist");

                    b.Navigation("Labels");
                });
#pragma warning restore 612, 618
        }
    }
}
