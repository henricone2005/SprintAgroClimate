﻿// <auto-generated />
using AgroClimate.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace AgroClimate.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241101023540_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AgricultorFazenda", b =>
                {
                    b.Property<int>("AgricultorId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("FazendaId")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("AgricultorId", "FazendaId");

                    b.HasIndex("FazendaId");

                    b.ToTable("AgricultorFazenda");
                });

            modelBuilder.Entity("AgroClimate.Models.Agricultor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("NVARCHAR2(11)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.HasKey("Id");

                    b.ToTable("NomeDaTabelaRealNoBanco", (string)null);
                });

            modelBuilder.Entity("AgroClimate.Models.Fazenda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Area")
                        .HasMaxLength(50)
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.HasKey("Id");

                    b.ToTable("FazendasSP");
                });

            modelBuilder.Entity("AgricultorFazenda", b =>
                {
                    b.HasOne("AgroClimate.Models.Agricultor", null)
                        .WithMany()
                        .HasForeignKey("AgricultorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgroClimate.Models.Fazenda", null)
                        .WithMany()
                        .HasForeignKey("FazendaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
