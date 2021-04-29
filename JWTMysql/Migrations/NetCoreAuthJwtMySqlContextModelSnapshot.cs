﻿// <auto-generated />
using JWTMysql.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JWTMysql.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class NetCoreAuthJwtMySqlContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("JWTMysql.Models.Db.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("EMAIL")
                        .HasDefaultValueSql("'0'");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)")
                        .HasColumnName("PASSWORD")
                        .HasDefaultValueSql("'0'");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("ROLE")
                        .HasDefaultValueSql("'0'");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("SALT")
                        .HasDefaultValueSql("'0'");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Email" }, "IX_EMAIL");

                    b.ToTable("user");
                });
#pragma warning restore 612, 618
        }
    }
}