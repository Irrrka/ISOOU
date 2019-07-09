﻿// <auto-generated />
using System;
using ISOOU.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ISOOU.Data.Migrations
{
    [DbContext(typeof(ISOOUContext))]
    [Migration("20190708053752_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ISOOU.Data.Models.AddmissionProcedure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndApplyDocuments");

                    b.Property<DateTime>("EndEnrollment");

                    b.Property<DateTime>("StartApplyDocuments");

                    b.Property<DateTime>("StartEnrollment");

                    b.HasKey("Id");

                    b.ToTable("Procedures");
                });

            modelBuilder.Entity("ISOOU.Data.Models.AddressDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Current");

                    b.Property<string>("District");

                    b.Property<string>("Permanent");

                    b.Property<string>("Quarter");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("ISOOU.Data.Models.AdmissionCriteria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Score");

                    b.HasKey("Id");

                    b.ToTable("Criterias");
                });

            modelBuilder.Entity("ISOOU.Data.Models.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("ISOOU.Data.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("ApplicationUser");
                });

            modelBuilder.Entity("ISOOU.Data.Models.DocumentSubmission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CandidateId");

                    b.Property<DateTime>("DateTimeUploaded");

                    b.Property<string>("PathFile");

                    b.Property<int?>("SchoolId");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.HasIndex("SchoolId");

                    b.ToTable("DocumentSubmissions");
                });

            modelBuilder.Entity("ISOOU.Data.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SystemUserId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("SystemUserId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("ISOOU.Data.Models.School", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AddressId");

                    b.Property<int>("CandidateId");

                    b.Property<string>("DirectorId");

                    b.Property<string>("Name");

                    b.Property<int>("NecessaryDocumentId");

                    b.Property<int>("NumberOfClasses");

                    b.Property<int?>("ProcedureId");

                    b.Property<string>("Ref");

                    b.Property<int>("StudentsPerClass");

                    b.Property<string>("SystemUserId");

                    b.Property<string>("URLOfMap");

                    b.Property<string>("URLOfSchool");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("DirectorId");

                    b.HasIndex("ProcedureId");

                    b.HasIndex("SystemUserId");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("ISOOU.Data.Models.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("ISOOU.Data.Models.SystemUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<int?>("AddressId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int?>("CriteriaId");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FathersEGN");

                    b.Property<string>("FathersFullName");

                    b.Property<string>("FathersPhoneNumber");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("MiddleName");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("MothersEGN");

                    b.Property<string>("MothersFullName");

                    b.Property<string>("MothersPhoneNumber");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<int>("QuestionId");

                    b.Property<int?>("SchoolId");

                    b.Property<string>("SecurityStamp");

                    b.Property<int>("Status");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UCN");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<int>("YearOfBirth");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("CriteriaId");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("SchoolId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("SystemUserId");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("SystemUserId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("SystemUserId");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("SystemUserId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.Property<string>("SystemUserId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("SystemUserId");

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

            modelBuilder.Entity("ISOOU.Data.Models.DocumentSubmission", b =>
                {
                    b.HasOne("ISOOU.Data.Models.SystemUser", "Candidate")
                        .WithMany()
                        .HasForeignKey("CandidateId");

                    b.HasOne("ISOOU.Data.Models.School", "School")
                        .WithMany("NecessaryDocuments")
                        .HasForeignKey("SchoolId");
                });

            modelBuilder.Entity("ISOOU.Data.Models.Question", b =>
                {
                    b.HasOne("ISOOU.Data.Models.SystemUser")
                        .WithMany("Questions")
                        .HasForeignKey("SystemUserId");
                });

            modelBuilder.Entity("ISOOU.Data.Models.School", b =>
                {
                    b.HasOne("ISOOU.Data.Models.AddressDetails", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("ISOOU.Data.Models.SystemUser", "Director")
                        .WithMany()
                        .HasForeignKey("DirectorId");

                    b.HasOne("ISOOU.Data.Models.AddmissionProcedure", "Procedure")
                        .WithMany()
                        .HasForeignKey("ProcedureId");

                    b.HasOne("ISOOU.Data.Models.SystemUser")
                        .WithMany("Schools")
                        .HasForeignKey("SystemUserId");
                });

            modelBuilder.Entity("ISOOU.Data.Models.SystemUser", b =>
                {
                    b.HasOne("ISOOU.Data.Models.AddressDetails", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("ISOOU.Data.Models.AdmissionCriteria", "Criteria")
                        .WithMany()
                        .HasForeignKey("CriteriaId");

                    b.HasOne("ISOOU.Data.Models.School")
                        .WithMany("Candidates")
                        .HasForeignKey("SchoolId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("ISOOU.Data.Models.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ISOOU.Data.Models.SystemUser")
                        .WithMany("Claims")
                        .HasForeignKey("SystemUserId");

                    b.HasOne("ISOOU.Data.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ISOOU.Data.Models.SystemUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ISOOU.Data.Models.SystemUser")
                        .WithMany("Logins")
                        .HasForeignKey("SystemUserId");

                    b.HasOne("ISOOU.Data.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ISOOU.Data.Models.SystemUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("ISOOU.Data.Models.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ISOOU.Data.Models.SystemUser")
                        .WithMany("Roles")
                        .HasForeignKey("SystemUserId");

                    b.HasOne("ISOOU.Data.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ISOOU.Data.Models.SystemUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ISOOU.Data.Models.SystemUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
