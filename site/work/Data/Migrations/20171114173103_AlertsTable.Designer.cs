﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using QFlow.Data;
using System;

namespace QFlow.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20171114173103_AlertsTable")]
    partial class AlertsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.AlertNotification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Email");

                    b.Property<bool>("IsRead");

                    b.Property<int?>("RequestId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("RequestId");

                    b.ToTable("AlertNotifications");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.AlertType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsSendingEmail");

                    b.Property<string>("Message");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("AlertTypes");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.AlertTypePeriodTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AlertTypeId");

                    b.Property<int>("PeriodTimeId");

                    b.HasKey("Id");

                    b.HasIndex("AlertTypeId");

                    b.HasIndex("PeriodTimeId");

                    b.ToTable("AlertTypePeriodTime");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.EmailNotification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<DateTime?>("Completed");

                    b.Property<DateTime>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("EmailsTo");

                    b.Property<bool>("IsProcessing");

                    b.Property<bool>("IsSent");

                    b.Property<string>("Subject");

                    b.HasKey("Id");

                    b.ToTable("EmailNotifications");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.Manufacturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.ManufacturerModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ManufacturerId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("ManufacturerModels");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Subject");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.PeriodTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("PeriodTimes");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AncillaryItems");

                    b.Property<int>("ClientId");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("DateOfStudy");

                    b.Property<string>("DurationOfStudy");

                    b.Property<string>("FullAddress");

                    b.Property<int?>("ManagerId");

                    b.Property<int>("RequestStatusId");

                    b.Property<string>("SpecialInstructions");

                    b.Property<string>("StudyInformation");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("ManagerId");

                    b.HasIndex("RequestStatusId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.RequestDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ManufacturerId");

                    b.Property<int?>("ManufacturerModelId");

                    b.Property<int?>("RequestId");

                    b.Property<string>("ServiceWarrantyOptions");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.HasIndex("ManufacturerModelId");

                    b.HasIndex("RequestId");

                    b.ToTable("RequestDetails");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.RequestStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("RequestStatuses");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.RequestStatusMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MessageId");

                    b.Property<int>("RequestStatusId");

                    b.HasKey("Id");

                    b.HasIndex("MessageId");

                    b.HasIndex("RequestStatusId");

                    b.ToTable("RequestStatusMessages");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.Users.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.Users.Manager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("QFlow.Models.DataModels.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("QFlow.Models.DataModels.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("QFlow.Models.DataModels.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("QFlow.Models.DataModels.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QFlow.Models.DataModels.AlertNotification", b =>
                {
                    b.HasOne("QFlow.Models.DataModels.Request", "Request")
                        .WithMany("Alerts")
                        .HasForeignKey("RequestId");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.AlertTypePeriodTime", b =>
                {
                    b.HasOne("QFlow.Models.DataModels.AlertType", "AlertType")
                        .WithMany()
                        .HasForeignKey("AlertTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("QFlow.Models.DataModels.PeriodTime", "PeriodTime")
                        .WithMany()
                        .HasForeignKey("PeriodTimeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QFlow.Models.DataModels.ManufacturerModel", b =>
                {
                    b.HasOne("QFlow.Models.DataModels.Manufacturer", "Manufacturer")
                        .WithMany()
                        .HasForeignKey("ManufacturerId");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.Request", b =>
                {
                    b.HasOne("QFlow.Models.DataModels.Users.Client", "Client")
                        .WithMany("Requests")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("QFlow.Models.DataModels.Users.Manager", "Manager")
                        .WithMany("Requests")
                        .HasForeignKey("ManagerId");

                    b.HasOne("QFlow.Models.DataModels.RequestStatus", "RequestStatus")
                        .WithMany()
                        .HasForeignKey("RequestStatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QFlow.Models.DataModels.RequestDetail", b =>
                {
                    b.HasOne("QFlow.Models.DataModels.Manufacturer", "Manufacturer")
                        .WithMany()
                        .HasForeignKey("ManufacturerId");

                    b.HasOne("QFlow.Models.DataModels.ManufacturerModel", "ManufacturerModel")
                        .WithMany()
                        .HasForeignKey("ManufacturerModelId");

                    b.HasOne("QFlow.Models.DataModels.Request", "Request")
                        .WithMany("Details")
                        .HasForeignKey("RequestId");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.RequestStatusMessage", b =>
                {
                    b.HasOne("QFlow.Models.DataModels.Message", "Message")
                        .WithMany()
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("QFlow.Models.DataModels.RequestStatus", "RequestStatus")
                        .WithMany()
                        .HasForeignKey("RequestStatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
