﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SistemaJuridicoWebAPI.Data;

#nullable disable

namespace SistemaJuridicoWebAPI.Migrations
{
    [DbContext(typeof(SistemaJuridicoDbContext))]
    partial class SistemaJuridicoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SistemaJuridicoWebAPI.Models.PROCESSO", b =>
                {
                    b.Property<Guid>("ID_PROCESSO")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AMBITO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AREA_DO_DIREITO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CIDADE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DATA_CADASTRO_PROCESSO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DATA_CITACAO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DATA_DISTRIBUICAO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DATA_ENCERRAMENTO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DATA_ULTIMO_ANDAMENTO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EMPRESA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EMPRESA_CNPJ")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ESTADO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FASE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FORO_TRIBUNAL_ORGAO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ID_PARTE_CONTRARIA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ID_USUARIO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MOTIVO_BAIXA_PROVISORIA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MOTIVO_ENCERRAMENTO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NUMERO_PROCESSO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PAIS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PARTE_CONTRARIA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PATRONOS_ANTERIORES")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PATRONO_RESPONSAVEL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("STATUS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TEXTO_DO_OBJETO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TIPO_DE_ACAO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("VALOR_DO_PEDIDO")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric");

                    b.Property<decimal>("VALOR_INSTANCIA1")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric");

                    b.Property<decimal>("VALOR_INSTANCIA2")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric");

                    b.Property<decimal>("VALOR_INSTANCIA3")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric");

                    b.Property<decimal>("VALOR_INSTANCIA_EXTRAORDINARIA")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric");

                    b.Property<string>("VARA")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_PROCESSO");

                    b.ToTable("PROCESSO");
                });

            modelBuilder.Entity("SistemaJuridicoWebAPI.Models.PROCESSO_ACORDO", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CONDICOES_TENTATIVA_DE_ACORDO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CRIADOR_ACORDO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DATA_ACORDO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ID_PROCESSO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("VALOR_ACORDO")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric");

                    b.HasKey("ID");

                    b.ToTable("PROCESSO_ACORDO");
                });

            modelBuilder.Entity("SistemaJuridicoWebAPI.Models.PROCESSO_AMBITO", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AMBITO")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PROCESSO_AMBITO");
                });

            modelBuilder.Entity("SistemaJuridicoWebAPI.Models.PROCESSO_ANDAMENTO", b =>
                {
                    b.Property<Guid?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CRIADOR_ANDAMENTO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DATA_ANDAMENTO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DATA_CADASTRO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DESC_ANDAMENTO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ID_PROCESSO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TIPO_DE_ANDAMENTO")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PROCESSO_ANDAMENTO");
                });

            modelBuilder.Entity("SistemaJuridicoWebAPI.Models.PROCESSO_AREA_DO_DIREITO", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AREA_DO_DIREITO")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PROCESSO_AREA_DO_DIREITO");
                });

            modelBuilder.Entity("SistemaJuridicoWebAPI.Models.PROCESSO_EMPRESAS", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CPF_CNPJ")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EMPRESA")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PROCESSO_EMPRESAS");
                });

            modelBuilder.Entity("SistemaJuridicoWebAPI.Models.PROCESSO_FASE", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FASE")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PROCESSO_FASE");
                });

            modelBuilder.Entity("SistemaJuridicoWebAPI.Models.PROCESSO_FORO_TRIBUNAL_ORGAO", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FORO_TRIBUNAL_ORGAO")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PROCESSO_FORO_TRIBUNAL_ORGAO");
                });

            modelBuilder.Entity("SistemaJuridicoWebAPI.Models.PROCESSO_LOG_ALTERACOES", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DATA_ALTERACAO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ID_PROCESSO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ID_USUARIO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("INPUT_ALTERADO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NOME_USUARIO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VALOR_ATUAL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VALOR_ORIGINAL")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PROCESSO_LOG_ALTERACOES");
                });

            modelBuilder.Entity("SistemaJuridicoWebAPI.Models.PROCESSO_MOTIVO_DO_ENCERRAMENTO", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MOTIVO_DO_ENCERRAMENTO")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PROCESSO_MOTIVO_DO_ENCERRAMENTO");
                });

            modelBuilder.Entity("SistemaJuridicoWebAPI.Models.PROCESSO_PARTE_CONTRARIA", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CARGO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CEP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CIDADE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CNPJ")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("COMPLEMENTO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CPF")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DATA_ADMISSAO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DATA_DEMISSAO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ENDERECO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ESTADO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NOME")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NOME_FANTASIA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NUMERO")
                        .HasColumnType("int");

                    b.Property<string>("OBSERVACAO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PAIS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PF_PJ")
                        .HasColumnType("int");

                    b.Property<string>("RG")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ULTIMO_SALARIO")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric");

                    b.HasKey("ID");

                    b.ToTable("PROCESSO_PARTE_CONTRARIA");
                });

            modelBuilder.Entity("SistemaJuridicoWebAPI.Models.PROCESSO_PATRONOS_ANTERIORES", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DATA_ALTERACAO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ID_PROCESSO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ID_USUARIO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NOME_USUARIO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PATRONO_RESPONSAVEL_ANTERIOR")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PROCESSO_PATRONOS_ANTERIORES");
                });

            modelBuilder.Entity("SistemaJuridicoWebAPI.Models.PROCESSO_PATRONO_RESPONSAVEL", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PATRONO_RESPONSAVEL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PATRONO_RESPONSAVEL_CPF_CNPJ")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PROCESSO_PATRONO_RESPONSAVEL");
                });

            modelBuilder.Entity("SistemaJuridicoWebAPI.Models.PROCESSO_STATUS", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("STATUS")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PROCESSO_STATUS");
                });

            modelBuilder.Entity("SistemaJuridicoWebAPI.Models.PROCESSO_TIPO_DE_ACAO", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TIPO_DE_ACAO")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PROCESSO_TIPO_DE_ACAO");
                });

            modelBuilder.Entity("SistemaJuridicoWebAPI.Models.PROCESSO_TIPO_DE_ANDAMENTO", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TIPO_DE_ANDAMENTO")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PROCESSO_TIPO_DE_ANDAMENTO");
                });

            modelBuilder.Entity("SistemaJuridicoWebAPI.Models.PROCESSO_VARA", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VARA")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PROCESSO_VARA");
                });

            modelBuilder.Entity("SistemaJuridicoWebAPI.Models.USUARIO", b =>
                {
                    b.Property<Guid>("ID_USUARIO")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ACESSO_GESTAO")
                        .HasColumnType("int");

                    b.Property<string>("NOME_USUARIO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SENHA")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_USUARIO");

                    b.ToTable("USUARIO");
                });
#pragma warning restore 612, 618
        }
    }
}
