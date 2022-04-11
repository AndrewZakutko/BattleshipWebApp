﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220411132539_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.3");

            modelBuilder.Entity("Domain.CellDb", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CellStatus")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte>("X")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Y")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Cells");
                });

            modelBuilder.Entity("Domain.CellShipDb", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CellDbId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("FieldDbId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ShipDbId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CellDbId");

                    b.HasIndex("FieldDbId");

                    b.HasIndex("ShipDbId");

                    b.ToTable("CellShips");
                });

            modelBuilder.Entity("Domain.FieldDb", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("Domain.GameDb", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("FirstPlayerFieldId")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstPlayerName")
                        .HasColumnType("TEXT");

                    b.Property<string>("GameStatus")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("MoveCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NameOfWinner")
                        .HasColumnType("TEXT");

                    b.Property<string>("ResultInfo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("SecondPlayerFieldId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SecondPlayerName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FirstPlayerFieldId");

                    b.HasIndex("SecondPlayerFieldId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Domain.ShipDb", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ShipDirection")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ShipRank")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte>("startPositionX")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("startPositionY")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Ships");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Domain.PlayerDb", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<Guid?>("GameId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasIndex("GameId");

                    b.HasDiscriminator().HasValue("PlayerDb");
                });

            modelBuilder.Entity("Domain.CellShipDb", b =>
                {
                    b.HasOne("Domain.CellDb", null)
                        .WithMany("CellShips")
                        .HasForeignKey("CellDbId");

                    b.HasOne("Domain.FieldDb", null)
                        .WithMany("CellShips")
                        .HasForeignKey("FieldDbId");

                    b.HasOne("Domain.ShipDb", null)
                        .WithMany("CellShips")
                        .HasForeignKey("ShipDbId");
                });

            modelBuilder.Entity("Domain.GameDb", b =>
                {
                    b.HasOne("Domain.FieldDb", "FirstPlayerField")
                        .WithMany()
                        .HasForeignKey("FirstPlayerFieldId");

                    b.HasOne("Domain.FieldDb", "SecondPlayerField")
                        .WithMany()
                        .HasForeignKey("SecondPlayerFieldId");

                    b.Navigation("FirstPlayerField");

                    b.Navigation("SecondPlayerField");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.PlayerDb", b =>
                {
                    b.HasOne("Domain.GameDb", "Game")
                        .WithMany()
                        .HasForeignKey("GameId");

                    b.Navigation("Game");
                });

            modelBuilder.Entity("Domain.CellDb", b =>
                {
                    b.Navigation("CellShips");
                });

            modelBuilder.Entity("Domain.FieldDb", b =>
                {
                    b.Navigation("CellShips");
                });

            modelBuilder.Entity("Domain.ShipDb", b =>
                {
                    b.Navigation("CellShips");
                });
#pragma warning restore 612, 618
        }
    }
}