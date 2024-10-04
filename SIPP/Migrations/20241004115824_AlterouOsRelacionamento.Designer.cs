﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SIPP.Data;

#nullable disable

namespace SIPP.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241004115824_AlterouOsRelacionamento")]
    partial class AlterouOsRelacionamento
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SIPP.Models.Agendamento", b =>
                {
                    b.Property<Guid>("AgendamentoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CorretorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("DataAge")
                        .HasColumnType("date");

                    b.Property<TimeOnly>("HoraAge")
                        .HasColumnType("time");

                    b.HasKey("AgendamentoId");

                    b.ToTable("tbAgendamento", (string)null);
                });

            modelBuilder.Entity("SIPP.Models.Cliente", b =>
                {
                    b.Property<Guid>("ClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CEP")
                        .HasColumnType("int");

                    b.Property<int>("CPF")
                        .HasColumnType("int");

                    b.Property<DateOnly>("DataNascimento")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Telefone")
                        .HasColumnType("int");

                    b.Property<string>("campoTeste")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("complemento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("numero")
                        .HasColumnType("int");

                    b.Property<string>("rua")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClienteId");

                    b.ToTable("tbCliente", (string)null);
                });

            modelBuilder.Entity("SIPP.Models.Corretor", b =>
                {
                    b.Property<Guid>("CorretorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CRECI")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TemDeTrabalho")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CorretorId");

                    b.ToTable("tbCorretor", (string)null);
                });

            modelBuilder.Entity("SIPP.Models.Imovel", b =>
                {
                    b.Property<Guid>("ImovelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MetodoPagamento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QntDormitorios")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QntGarragem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tamanho")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TamanhoAreaContuida")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("cidade")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImovelId");

                    b.ToTable("tbImovel", (string)null);
                });

            modelBuilder.Entity("SIPP.Models.RelacionandoImoATipo", b =>
                {
                    b.Property<Guid>("TipodeTransacaoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ImovelId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TipodeTransacaoId", "ImovelId");

                    b.HasIndex("ImovelId");

                    b.ToTable("tbRelacionandoImoATipo", (string)null);
                });

            modelBuilder.Entity("SIPP.Models.TipodeTransacao", b =>
                {
                    b.Property<Guid>("TipodeTransacaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TipodeTransacaoId");

                    b.ToTable("tbTipodeTransacao", (string)null);
                });

            modelBuilder.Entity("SIPP.Models.RelacionandoImoATipo", b =>
                {
                    b.HasOne("SIPP.Models.Imovel", "Imovel")
                        .WithMany("Relacionamentos")
                        .HasForeignKey("ImovelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SIPP.Models.TipodeTransacao", "TipoDeTransacao")
                        .WithMany("Relacionamentos")
                        .HasForeignKey("TipodeTransacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Imovel");

                    b.Navigation("TipoDeTransacao");
                });

            modelBuilder.Entity("SIPP.Models.Imovel", b =>
                {
                    b.Navigation("Relacionamentos");
                });

            modelBuilder.Entity("SIPP.Models.TipodeTransacao", b =>
                {
                    b.Navigation("Relacionamentos");
                });
#pragma warning restore 612, 618
        }
    }
}
