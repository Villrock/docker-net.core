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
    [Migration("20171212121534_AddNewFiledsToDetails")]
    partial class AddNewFiledsToDetails
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

            modelBuilder.Entity("QFlow.Models.DataModels.Alerts.AlertNotification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AlertTypeId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Email");

                    b.Property<bool>("IsRead");

                    b.Property<int?>("RequestId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("AlertTypeId");

                    b.HasIndex("RequestId");

                    b.ToTable("AlertNotifications");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.Alerts.AlertType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsSendingEmail");

                    b.Property<string>("Message");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("AlertTypes");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.Alerts.AlertTypePeriodTime", b =>
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

            modelBuilder.Entity("QFlow.Models.DataModels.Alerts.PeriodTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("PeriodTimes");
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

            modelBuilder.Entity("QFlow.Models.DataModels.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.Emails.EmailNotification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AlertTypeId");

                    b.Property<string>("Body");

                    b.Property<DateTime?>("Completed");

                    b.Property<DateTime>("Created");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("EmailsTo");

                    b.Property<bool>("IsProcessing");

                    b.Property<bool>("IsSent");

                    b.Property<int?>("RequestId");

                    b.Property<DateTime?>("SendDate");

                    b.Property<string>("Subject");

                    b.HasKey("Id");

                    b.HasIndex("AlertTypeId");

                    b.HasIndex("RequestId");

                    b.ToTable("EmailNotifications");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.Emails.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Subject");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.Emails.RequestStatusMessage", b =>
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

            modelBuilder.Entity("QFlow.Models.DataModels.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClientId");

                    b.Property<DateTime>("Created");

                    b.Property<string>("CreatedAsString");

                    b.Property<DateTime?>("DateOfStudy");

                    b.Property<string>("DurationOfStudy");

                    b.Property<string>("FullAddress");

                    b.Property<int?>("ManagerId");

                    b.Property<int?>("RequestId");

                    b.Property<string>("RequestNumber");

                    b.Property<int>("RequestStatusId");

                    b.Property<string>("SpecialInstructions");

                    b.Property<string>("StudyInformation");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("ManagerId");

                    b.HasIndex("RequestStatusId");

                    b.HasIndex("Title");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.Requests.Manufacturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.Requests.ManufacturerModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ManufacturerId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("ManufacturerModels");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.Requests.RequestDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("ActualDespatchDate");

                    b.Property<DateTime?>("ActualInstallationDate");

                    b.Property<string>("AlternativesRecommendation");

                    b.Property<string>("AncillaryItems");

                    b.Property<string>("ApprovalsCertificationRequired");

                    b.Property<string>("CalibrationServiceRequirements");

                    b.Property<DateTime?>("ConfirmedDeliveryDate");

                    b.Property<string>("ContactPersons");

                    b.Property<string>("Currency");

                    b.Property<DateTime?>("DeInstallDueDate");

                    b.Property<DateTime?>("EstimateDeliveryDate");

                    b.Property<DateTime?>("EstimatedLeadTime");

                    b.Property<Guid?>("FolderId");

                    b.Property<DateTime?>("InstallationDate");

                    b.Property<string>("InvoiceFile");

                    b.Property<DateTime?>("IssuedDate");

                    b.Property<string>("LinkToBrochure");

                    b.Property<string>("LocalTechnicalSupport");

                    b.Property<int?>("ManufacturerId");

                    b.Property<int?>("ManufacturerModelId");

                    b.Property<string>("NameOfPerson");

                    b.Property<DateTime?>("PaymentDueDate");

                    b.Property<DateTime?>("PaymentReceivedDate");

                    b.Property<DateTime?>("PaymentRunDate");

                    b.Property<string>("ProofOfDelivery");

                    b.Property<int?>("Quantity");

                    b.Property<int?>("RequestId");

                    b.Property<string>("SerialNumber");

                    b.Property<DateTime?>("ServiceDueDate");

                    b.Property<string>("ServiceWarrantyOptions");

                    b.Property<DateTime?>("ShippingDate");

                    b.Property<bool?>("SiteActivated");

                    b.Property<DateTime?>("SivRequiredDate");

                    b.Property<string>("TrackingNumberOrCourierDetails");

                    b.Property<DateTime?>("TrainingDate");

                    b.Property<string>("TrainingDocuments");

                    b.Property<int?>("UnitPrice");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.HasIndex("ManufacturerModelId");

                    b.HasIndex("RequestId");

                    b.ToTable("RequestDetails");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.Requests.RequestStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("RequestStatuses");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.Users.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.HasIndex("FirstName", "LastName");

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

            modelBuilder.Entity("QFlow.Models.DataModels.Alerts.AlertNotification", b =>
                {
                    b.HasOne("QFlow.Models.DataModels.Alerts.AlertType", "AlertType")
                        .WithMany()
                        .HasForeignKey("AlertTypeId");

                    b.HasOne("QFlow.Models.DataModels.Request", "Request")
                        .WithMany("Alerts")
                        .HasForeignKey("RequestId");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.Alerts.AlertTypePeriodTime", b =>
                {
                    b.HasOne("QFlow.Models.DataModels.Alerts.AlertType", "AlertType")
                        .WithMany()
                        .HasForeignKey("AlertTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("QFlow.Models.DataModels.Alerts.PeriodTime", "PeriodTime")
                        .WithMany()
                        .HasForeignKey("PeriodTimeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QFlow.Models.DataModels.Emails.EmailNotification", b =>
                {
                    b.HasOne("QFlow.Models.DataModels.Alerts.AlertType", "AlertType")
                        .WithMany()
                        .HasForeignKey("AlertTypeId");

                    b.HasOne("QFlow.Models.DataModels.Request", "Request")
                        .WithMany()
                        .HasForeignKey("RequestId");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.Emails.RequestStatusMessage", b =>
                {
                    b.HasOne("QFlow.Models.DataModels.Emails.Message", "Message")
                        .WithMany()
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("QFlow.Models.DataModels.Requests.RequestStatus", "RequestStatus")
                        .WithMany()
                        .HasForeignKey("RequestStatusId")
                        .OnDelete(DeleteBehavior.Cascade);
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

                    b.HasOne("QFlow.Models.DataModels.Requests.RequestStatus", "RequestStatus")
                        .WithMany()
                        .HasForeignKey("RequestStatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QFlow.Models.DataModels.Requests.ManufacturerModel", b =>
                {
                    b.HasOne("QFlow.Models.DataModels.Requests.Manufacturer", "Manufacturer")
                        .WithMany()
                        .HasForeignKey("ManufacturerId");
                });

            modelBuilder.Entity("QFlow.Models.DataModels.Requests.RequestDetail", b =>
                {
                    b.HasOne("QFlow.Models.DataModels.Requests.Manufacturer", "Manufacturer")
                        .WithMany()
                        .HasForeignKey("ManufacturerId");

                    b.HasOne("QFlow.Models.DataModels.Requests.ManufacturerModel", "ManufacturerModel")
                        .WithMany()
                        .HasForeignKey("ManufacturerModelId");

                    b.HasOne("QFlow.Models.DataModels.Request", "Request")
                        .WithMany("Details")
                        .HasForeignKey("RequestId");
                });
#pragma warning restore 612, 618
        }
    }
}
