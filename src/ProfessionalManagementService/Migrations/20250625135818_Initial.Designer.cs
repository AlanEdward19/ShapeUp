﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProfessionalManagementService.Connections.Database;

#nullable disable

namespace ProfessionalManagementService.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20250625135818_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProfessionalManagementService.Clients.Client", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("ProfessionalManagementService.Clients.ClientServicePlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Feedback")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ServicePlanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("ServicePlanId");

                    b.ToTable("ClientServicePlans");
                });

            modelBuilder.Entity("ProfessionalManagementService.Professionals.Professional", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Professionals");
                });

            modelBuilder.Entity("ProfessionalManagementService.Reviews.ClientProfessionalReview", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("ClientServicePlanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProfessionalId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("ClientServicePlanId");

                    b.HasIndex("ProfessionalId");

                    b.ToTable("ClientProfessionalReviews");
                });

            modelBuilder.Entity("ProfessionalManagementService.ServicePlans.ServicePlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DurationInDays")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("ProfessionalId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProfessionalId");

                    b.ToTable("ServicePlans");
                });

            modelBuilder.Entity("ProfessionalManagementService.Clients.ClientServicePlan", b =>
                {
                    b.HasOne("ProfessionalManagementService.Clients.Client", "Client")
                        .WithMany("ClientServicePlans")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProfessionalManagementService.ServicePlans.ServicePlan", "ServicePlan")
                        .WithMany()
                        .HasForeignKey("ServicePlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("ServicePlan");
                });

            modelBuilder.Entity("ProfessionalManagementService.Reviews.ClientProfessionalReview", b =>
                {
                    b.HasOne("ProfessionalManagementService.Clients.Client", "Client")
                        .WithMany("ClientProfessionalReviews")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProfessionalManagementService.Clients.ClientServicePlan", "ClientServicePlan")
                        .WithMany()
                        .HasForeignKey("ClientServicePlanId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProfessionalManagementService.Professionals.Professional", "Professional")
                        .WithMany()
                        .HasForeignKey("ProfessionalId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Client");

                    b.Navigation("ClientServicePlan");

                    b.Navigation("Professional");
                });

            modelBuilder.Entity("ProfessionalManagementService.ServicePlans.ServicePlan", b =>
                {
                    b.HasOne("ProfessionalManagementService.Professionals.Professional", "Professional")
                        .WithMany("ServicePlans")
                        .HasForeignKey("ProfessionalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Professional");
                });

            modelBuilder.Entity("ProfessionalManagementService.Clients.Client", b =>
                {
                    b.Navigation("ClientProfessionalReviews");

                    b.Navigation("ClientServicePlans");
                });

            modelBuilder.Entity("ProfessionalManagementService.Professionals.Professional", b =>
                {
                    b.Navigation("ServicePlans");
                });
#pragma warning restore 612, 618
        }
    }
}
