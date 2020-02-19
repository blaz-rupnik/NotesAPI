﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NotesApp.Persistence;

namespace NotesApp.Migrations
{
    [DbContext(typeof(NotesDbContext))]
    [Migration("20200219191416_ExpandedNotes")]
    partial class ExpandedNotes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NotesApp.Models.Folder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Folders");
                });

            modelBuilder.Entity("NotesApp.Models.Note", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("FolderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsShared")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<Guid>("NoteTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FolderId");

                    b.HasIndex("NoteTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("NotesApp.Models.NoteType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("NoteType");
                });

            modelBuilder.Entity("NotesApp.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NotesApp.Models.Folder", b =>
                {
                    b.HasOne("NotesApp.Models.User", "User")
                        .WithMany("Folders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NotesApp.Models.Note", b =>
                {
                    b.HasOne("NotesApp.Models.Folder", "Folder")
                        .WithMany("Notes")
                        .HasForeignKey("FolderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotesApp.Models.NoteType", "NoteType")
                        .WithMany()
                        .HasForeignKey("NoteTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotesApp.Models.User", "User")
                        .WithMany("Notes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
