﻿// <auto-generated />
using System;
using AutoPortal.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace angularAPI.Migrations
{
    [DbContext(typeof(AutoPortalDbContext))]
    partial class AutoPortalDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AutoPortal.Model.portaluser", b =>
                {
                    b.Property<Guid?>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("UserId");

                    b.ToTable("portalusers");
                });

            modelBuilder.Entity("AutoPortal.Model.userpolicylist", b =>
                {
                    b.Property<Guid?>("UserPolicyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int?>("PolicyNumber")
                        .HasColumnType("int");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("portaluserUserId")
                        .HasColumnType("char(36)");

                    b.HasKey("UserPolicyId");

                    b.HasIndex("portaluserUserId");

                    b.ToTable("userpolicylists");
                });

            modelBuilder.Entity("AutoPortal.Model.userpolicylist", b =>
                {
                    b.HasOne("AutoPortal.Model.portaluser", "portaluser")
                        .WithMany("userpolicylists")
                        .HasForeignKey("portaluserUserId");

                    b.Navigation("portaluser");
                });

            modelBuilder.Entity("AutoPortal.Model.portaluser", b =>
                {
                    b.Navigation("userpolicylists");
                });
#pragma warning restore 612, 618
        }
    }
}
