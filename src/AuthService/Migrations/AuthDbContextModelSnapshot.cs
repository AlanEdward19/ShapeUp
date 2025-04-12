﻿// <auto-generated />
using System;
using AuthService.Connections.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AuthService.Migrations
{
    [DbContext(typeof(AuthDbContext))]
    partial class AuthDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AuthService.Common.User.User", b =>
                {
                    b.Property<string>("ObjectId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ObjectId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AuthService.Group.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("AuthService.Group.GroupPermission", b =>
                {
                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PermissionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GroupId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("GroupPermission");
                });

            modelBuilder.Entity("AuthService.Group.UserGroup", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("UserId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("UserGroup");
                });

            modelBuilder.Entity("AuthService.Permission.Permission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Action")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Theme")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0c9c9154-2479-406c-8bd2-e15a22c8b31e"),
                            Action = 0,
                            CreatedAt = new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Theme = "permission",
                            UpdatedAt = new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("d3856d63-8c6d-41c7-bfb0-4a327662a6dc"),
                            Action = 2,
                            CreatedAt = new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Theme = "permission",
                            UpdatedAt = new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("e0dcbf9b-1d24-413a-8839-5d70f9ace22a"),
                            Action = 3,
                            CreatedAt = new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Theme = "permission",
                            UpdatedAt = new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("a86b7651-2aef-4f55-9c85-7b1f6d486f14"),
                            Action = 1,
                            CreatedAt = new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Theme = "permission",
                            UpdatedAt = new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("AuthService.Group.GroupPermission", b =>
                {
                    b.HasOne("AuthService.Group.Group", "Group")
                        .WithMany("GroupPermissions")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuthService.Permission.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Permission");
                });

            modelBuilder.Entity("AuthService.Group.UserGroup", b =>
                {
                    b.HasOne("AuthService.Group.Group", "Group")
                        .WithMany("Users")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuthService.Common.User.User", "User")
                        .WithMany("UserGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AuthService.Common.User.User", b =>
                {
                    b.Navigation("UserGroups");
                });

            modelBuilder.Entity("AuthService.Group.Group", b =>
                {
                    b.Navigation("GroupPermissions");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
