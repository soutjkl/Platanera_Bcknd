using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using bckPlatanera.Data.Models;

namespace bckPlatanera.Data
{
    public partial class BdPlatContext : DbContext
    {
        public BdPlatContext()
        {
        }

        public BdPlatContext(DbContextOptions<BdPlatContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<CityHasSubastum> CityHasSubasta { get; set; } = null!;
        public virtual DbSet<Credential> Credentials { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Inscription> Inscriptions { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<Purchaseoffer> Purchaseoffers { get; set; } = null!;
        public virtual DbSet<Session> Sessions { get; set; } = null!;
        public virtual DbSet<Subastum> Subasta { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;uid=root;pwd=7611;database=bd_plat_", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => new { e.IdCity, e.DepartmentsIdDepartments })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("city");

                entity.HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.HasIndex(e => e.DepartmentsIdDepartments, "fk_City_Departments1_idx");

                entity.Property(e => e.IdCity).HasColumnName("idCity");

                entity.Property(e => e.DepartmentsIdDepartments).HasColumnName("Departments_idDepartments");

                entity.Property(e => e.NameCity)
                    .HasMaxLength(60)
                    .HasColumnName("nameCity");

                entity.HasOne(d => d.DepartmentsIdDepartmentsNavigation)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.DepartmentsIdDepartments)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_City_Departments1");
            });

            modelBuilder.Entity<CityHasSubastum>(entity =>
            {
                entity.HasKey(e => new { e.CityIdCity, e.SubastaIdSubasta })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("city_has_subasta");

                entity.HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.HasIndex(e => e.CityIdCity, "fk_city_has_subasta_city1_idx");

                entity.HasIndex(e => e.SubastaIdSubasta, "fk_city_has_subasta_subasta1_idx");

                entity.Property(e => e.CityIdCity).HasColumnName("city_idCity");

                entity.Property(e => e.SubastaIdSubasta).HasColumnName("subasta_idSubasta");

                entity.HasOne(d => d.SubastaIdSubastaNavigation)
                    .WithMany(p => p.CityHasSubasta)
                    .HasPrincipalKey(p => p.IdSubasta)
                    .HasForeignKey(d => d.SubastaIdSubasta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_city_has_subasta_subasta1");
            });

            modelBuilder.Entity<Credential>(entity =>
            {
                entity.HasKey(e => new { e.IdCredentials, e.NumberDocumentPerson })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("credentials");

                entity.HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.HasIndex(e => e.NumberDocumentPerson, "fk_Credentials_Person1_idx");

                entity.HasIndex(e => e.IdCredentials, "idCredentials_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdCredentials)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idCredentials");

                entity.Property(e => e.NumberDocumentPerson)
                    .HasMaxLength(45)
                    .HasColumnName("number_document_person");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(45)
                    .HasColumnName("username");

                entity.HasOne(d => d.NumberDocumentPersonNavigation)
                    .WithMany(p => p.Credentials)
                    .HasForeignKey(d => d.NumberDocumentPerson)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_credentials_person1");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.IdDepartments)
                    .HasName("PRIMARY");

                entity.ToTable("departments");

                entity.HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.Property(e => e.IdDepartments)
                    .ValueGeneratedNever()
                    .HasColumnName("idDepartments");

                entity.Property(e => e.NameDepartments).HasMaxLength(45);
            });

            modelBuilder.Entity<Inscription>(entity =>
            {
                entity.HasKey(e => new { e.IdInscription, e.PersonDocumentNumber, e.SubastaIdSubasta })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.ToTable("inscription");

                entity.HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.HasIndex(e => e.PersonDocumentNumber, "fk_inscription_person1_idx");

                entity.HasIndex(e => e.SubastaIdSubasta, "fk_inscription_subasta1_idx");

                entity.HasIndex(e => e.IdInscription, "id_inscription_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdInscription)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_inscription");

                entity.Property(e => e.PersonDocumentNumber)
                    .HasMaxLength(45)
                    .HasColumnName("person_document_number");

                entity.Property(e => e.SubastaIdSubasta).HasColumnName("subasta_idSubasta");

                entity.Property(e => e.DateInscription)
                    .HasColumnType("datetime")
                    .HasColumnName("date_inscription");

                entity.HasOne(d => d.PersonDocumentNumberNavigation)
                    .WithMany(p => p.Inscriptions)
                    .HasForeignKey(d => d.PersonDocumentNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_inscription_person1");

                entity.HasOne(d => d.SubastaIdSubastaNavigation)
                    .WithMany(p => p.Inscriptions)
                    .HasPrincipalKey(p => p.IdSubasta)
                    .HasForeignKey(d => d.SubastaIdSubasta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_inscription_subasta1");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.DocumentNumber)
                    .HasName("PRIMARY");

                entity.ToTable("person");

                entity.HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.HasIndex(e => e.DocumentNumber, "document_number_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.DocumentNumber)
                    .HasMaxLength(45)
                    .HasColumnName("document_number")
                    .HasDefaultValueSql("'idUser'");

                entity.Property(e => e.Address)
                    .HasMaxLength(45)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(45)
                    .HasColumnName("email");

                entity.Property(e => e.NameUser)
                    .HasMaxLength(45)
                    .HasColumnName("name_user");

                entity.Property(e => e.Photo)
                    .HasMaxLength(45)
                    .HasColumnName("photo");

                entity.Property(e => e.SurnameUser)
                    .HasMaxLength(45)
                    .HasColumnName("surname_user");

                entity.Property(e => e.Telephone)
                    .HasMaxLength(45)
                    .HasColumnName("telephone");

                entity.Property(e => e.TypeDocument)
                    .HasMaxLength(2)
                    .HasColumnName("type_document")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Purchaseoffer>(entity =>
            {
                entity.HasKey(e => new { e.Idpurchaseoffer, e.InscriptionIdInscription })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("purchaseoffer");

                entity.HasIndex(e => e.InscriptionIdInscription, "fk_purchaseoffer_Inscription1_idx");

                entity.Property(e => e.Idpurchaseoffer)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idpurchaseoffer");

                entity.Property(e => e.InscriptionIdInscription).HasColumnName("Inscription_idInscription");

                entity.Property(e => e.DatePurchase)
                    .HasColumnType("datetime")
                    .HasColumnName("datePurchase");

                entity.Property(e => e.PricePurchase).HasColumnName("pricePurchase");

                entity.HasOne(d => d.InscriptionIdInscriptionNavigation)
                    .WithMany(p => p.Purchaseoffers)
                    .HasPrincipalKey(p => p.IdInscription)
                    .HasForeignKey(d => d.InscriptionIdInscription)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_purchaseoffer_Inscription1");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasKey(e => new { e.IdSession, e.PersonDocumentNumber })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("session");

                entity.HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.HasIndex(e => e.PersonDocumentNumber, "fk_session_person1_idx");

                entity.Property(e => e.IdSession)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_session");

                entity.Property(e => e.PersonDocumentNumber)
                    .HasMaxLength(45)
                    .HasColumnName("person_document_number");

                entity.Property(e => e.SessionEnd)
                    .HasColumnType("datetime")
                    .HasColumnName("session_end");

                entity.Property(e => e.SessionStart)
                    .HasColumnType("datetime")
                    .HasColumnName("session_start");

                entity.Property(e => e.Status)
                    .HasMaxLength(2)
                    .HasColumnName("status")
                    .IsFixedLength();

                entity.HasOne(d => d.PersonDocumentNumberNavigation)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.PersonDocumentNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_session_person1");
            });

            modelBuilder.Entity<Subastum>(entity =>
            {
                entity.HasKey(e => new { e.IdSubasta, e.PersonDocumentNumber })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("subasta");

                entity.HasCharSet("utf8mb3")
                    .UseCollation("utf8mb3_general_ci");

                entity.HasIndex(e => e.PersonDocumentNumber, "fk_subasta_person1_idx");

                entity.HasIndex(e => e.IdSubasta, "idSubasta_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdSubasta)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idSubasta");

                entity.Property(e => e.PersonDocumentNumber)
                    .HasMaxLength(45)
                    .HasColumnName("person_document_number");

                entity.Property(e => e.BananaType)
                    .HasMaxLength(70)
                    .HasColumnName("banana_type");

                entity.Property(e => e.DateEnded)
                    .HasColumnType("datetime")
                    .HasColumnName("date_ended");

                entity.Property(e => e.DateStarted)
                    .HasColumnType("datetime")
                    .HasColumnName("date_started");

                entity.Property(e => e.DescriptionProduct)
                    .HasMaxLength(500)
                    .HasColumnName("description_product");

                entity.Property(e => e.InitialPrice).HasColumnName("initial_price");

                entity.Property(e => e.MeasurementUnits).HasColumnName("measurement_units");

                entity.Property(e => e.Photos)
                    .HasMaxLength(45)
                    .HasColumnName("photos");

                entity.HasOne(d => d.PersonDocumentNumberNavigation)
                    .WithMany(p => p.Subasta)
                    .HasForeignKey(d => d.PersonDocumentNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_subasta_person1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
