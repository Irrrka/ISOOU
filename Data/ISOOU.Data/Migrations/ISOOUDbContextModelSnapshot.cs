﻿// <auto-generated />
using System;
using ISOOU.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ISOOU.Data.Migrations
{
    [DbContext(typeof(ISOOUDbContext))]
    partial class ISOOUDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ISOOU.Data.Models.AddressDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Current");

                    b.Property<int>("CurrentCity");

                    b.Property<int>("CurrentDistrictId");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Permanent");

                    b.Property<int>("PermanentCity");

                    b.Property<int>("PermanentDistrictId");

                    b.HasKey("Id");

                    b.HasIndex("CurrentDistrictId");

                    b.HasIndex("PermanentDistrictId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("ISOOU.Data.Models.AdmissionProcedure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("EndApplyDocuments");

                    b.Property<DateTime>("EndEnrollment");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<DateTime>("RankingDate");

                    b.Property<DateTime>("StartApplyDocuments");

                    b.Property<DateTime>("StartEnrollment");

                    b.Property<int>("Status");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("AdmissionProcedures");
                });

            modelBuilder.Entity("ISOOU.Data.Models.Candidate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BasicScores");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("Desease");

                    b.Property<int?>("FatherId");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<bool>("Immunization");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("KinderGarten");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<int?>("MotherId");

                    b.Property<int?>("ParentId");

                    b.Property<bool>("SEN");

                    b.Property<int>("Status");

                    b.Property<string>("UCN")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("FatherId");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("MotherId");

                    b.HasIndex("ParentId");

                    b.HasIndex("UserId");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("ISOOU.Data.Models.CandidateApplication", b =>
                {
                    b.Property<int>("CandidateId");

                    b.Property<int>("SchoolId");

                    b.Property<int>("AdditionalScoresForSchool");

                    b.Property<int?>("AdmissionProcedureId");

                    b.Property<int>("TotalScoresForSchool");

                    b.HasKey("CandidateId", "SchoolId");

                    b.HasIndex("AdmissionProcedureId");

                    b.HasIndex("SchoolId");

                    b.ToTable("CandidatesApplications");
                });

            modelBuilder.Entity("ISOOU.Data.Models.Criteria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("DisplayName");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<int>("Scores");

                    b.HasKey("Id");

                    b.ToTable("Criterias");
                });

            modelBuilder.Entity("ISOOU.Data.Models.CriteriaForCandidate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CandidateId");

                    b.Property<int>("CriteriaId");

                    b.Property<string>("Name");

                    b.Property<int>("Sch");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.HasIndex("CriteriaId");

                    b.ToTable("CriteriasForCandidates");
                });

            modelBuilder.Entity("ISOOU.Data.Models.District", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("ISOOU.Data.Models.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CandidateId");

                    b.Property<string>("Name");

                    b.Property<int>("SchoolId");

                    b.Property<DateTime>("UploadDate");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.HasIndex("SchoolId");

                    b.ToTable("Document");
                });

            modelBuilder.Entity("ISOOU.Data.Models.Parent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("PhoneNumber");

                    b.Property<int>("Role");

                    b.Property<string>("UCN")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("UserId");

                    b.Property<int>("WorkDistrictId");

                    b.Property<string>("WorkName");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("UserId");

                    b.HasIndex("WorkDistrictId");

                    b.ToTable("Parents");
                });

            modelBuilder.Entity("ISOOU.Data.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Subject")
                        .IsRequired();

                    b.Property<string>("SystemUserId");

                    b.HasKey("Id");

                    b.HasIndex("SystemUserId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("ISOOU.Data.Models.School", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("DirectorName")
                        .IsRequired();

                    b.Property<int>("DistrictId");

                    b.Property<string>("Email");

                    b.Property<int>("FreeSpots");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.Property<string>("URLOfMap");

                    b.Property<string>("URLOfSchool");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("ISOOU.Data.Models.SystemRole", b =>
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

            modelBuilder.Entity("ISOOU.Data.Models.SystemUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<int>("DirectorsSchoolId");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UCN");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<string>("UserRoleId");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("UserRoleId");

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

            modelBuilder.Entity("ISOOU.Data.Models.AddressDetails", b =>
                {
                    b.HasOne("ISOOU.Data.Models.District", "CurrentDistrict")
                        .WithMany()
                        .HasForeignKey("CurrentDistrictId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ISOOU.Data.Models.District", "PermanentDistrict")
                        .WithMany()
                        .HasForeignKey("PermanentDistrictId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ISOOU.Data.Models.Candidate", b =>
                {
                    b.HasOne("ISOOU.Data.Models.Parent", "Father")
                        .WithMany()
                        .HasForeignKey("FatherId");

                    b.HasOne("ISOOU.Data.Models.Parent", "Mother")
                        .WithMany()
                        .HasForeignKey("MotherId");

                    b.HasOne("ISOOU.Data.Models.Parent")
                        .WithMany("Candidates")
                        .HasForeignKey("ParentId");

                    b.HasOne("ISOOU.Data.Models.SystemUser", "User")
                        .WithMany("Candidates")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ISOOU.Data.Models.CandidateApplication", b =>
                {
                    b.HasOne("ISOOU.Data.Models.AdmissionProcedure", "AdmissionProcedure")
                        .WithMany("ParticipatedCandidates")
                        .HasForeignKey("AdmissionProcedureId");

                    b.HasOne("ISOOU.Data.Models.Candidate", "Candidate")
                        .WithMany("Applications")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ISOOU.Data.Models.School", "School")
                        .WithMany("Candidates")
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ISOOU.Data.Models.CriteriaForCandidate", b =>
                {
                    b.HasOne("ISOOU.Data.Models.Candidate", "Candidate")
                        .WithMany("Criterias")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ISOOU.Data.Models.Criteria", "Criteria")
                        .WithMany("Candidates")
                        .HasForeignKey("CriteriaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ISOOU.Data.Models.Document", b =>
                {
                    b.HasOne("ISOOU.Data.Models.Candidate", "Candidate")
                        .WithMany("Documents")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ISOOU.Data.Models.School", "School")
                        .WithMany()
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ISOOU.Data.Models.Parent", b =>
                {
                    b.HasOne("ISOOU.Data.Models.AddressDetails", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ISOOU.Data.Models.SystemUser", "User")
                        .WithMany("Parents")
                        .HasForeignKey("UserId");

                    b.HasOne("ISOOU.Data.Models.District", "WorkDistrict")
                        .WithMany()
                        .HasForeignKey("WorkDistrictId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ISOOU.Data.Models.Question", b =>
                {
                    b.HasOne("ISOOU.Data.Models.SystemUser", "User")
                        .WithMany("Questions")
                        .HasForeignKey("SystemUserId");
                });

            modelBuilder.Entity("ISOOU.Data.Models.School", b =>
                {
                    b.HasOne("ISOOU.Data.Models.District", "District")
                        .WithMany()
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ISOOU.Data.Models.SystemUser", b =>
                {
                    b.HasOne("ISOOU.Data.Models.SystemRole", "UserRole")
                        .WithMany()
                        .HasForeignKey("UserRoleId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("ISOOU.Data.Models.SystemRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ISOOU.Data.Models.SystemUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ISOOU.Data.Models.SystemUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("ISOOU.Data.Models.SystemRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ISOOU.Data.Models.SystemUser")
                        .WithMany("Roles")
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
