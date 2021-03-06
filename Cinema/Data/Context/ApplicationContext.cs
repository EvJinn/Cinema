using Cinema.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Cinema.Context
{
    public class ApplicationContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<AgeRestrict> AgeRestricts { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<FilmGenre> FilmGenres { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<SeatCategory> SeatCategories { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        /// <param name="configuration">Конфигурационный файл</param>
        public ApplicationContext(IConfiguration configuration)
        {
            _configuration = configuration;

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration["DbConnectionString"]).LogTo(mes => Debug.WriteLine(mes), LogLevel.Information).EnableSensitiveDataLogging();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity((Action<EntityTypeBuilder<AgeRestrict>>)(entity =>
            {
                entity.ToTable("agerestrict");
                entity.HasKey(e => e.id).HasName("id");
                
                entity.Property(e => e.Name).HasColumnName("name").IsRequired();
            }));
            modelBuilder.Entity((Action<EntityTypeBuilder<Client>>)(entity =>
            {
                entity.ToTable("client");
                entity.HasKey(e => e.id).HasName("id");
                
                entity.Property(e => e.FirstName).HasColumnName("firstname").IsRequired();
                entity.Property(e => e.LastName).HasColumnName("lastname").IsRequired();
                entity.Property(e => e.Patronymic).HasColumnName("patronymic");
                entity.Property(e => e.Discount).HasColumnName("discount");
            }));
            modelBuilder.Entity((Action<EntityTypeBuilder<Film>>)(entity =>
            {
                entity.ToTable("film");
                entity.HasKey(e => e.id).HasName("id");
                
                entity.Property(e => e.Name).HasColumnName("name").IsRequired();
                entity.Property(e => e.Duration).HasColumnName("duration");
                entity.Property(e => e.Markup).HasColumnName("markup");

                entity.HasOne(d => d.AgeRestrict).WithMany(p => p.Film).HasForeignKey(d => d.id_agerestrict)
                    .HasPrincipalKey(p => p.id);
            }));
            modelBuilder.Entity((Action<EntityTypeBuilder<FilmGenre>>)(entity =>
            {
                entity.ToTable("filmgenre");
                entity.HasKey(e => new
                {
                    e.id_Film,
                    e.id_Genre
                });

                entity.HasOne(d => d.Film).WithMany(p => p.FilmGenre).HasForeignKey(d => d.id_Film)
                    .HasPrincipalKey(p => p.id);
                entity.HasOne(d => d.Genre).WithMany(p => p.FilmGenre).HasForeignKey(d => d.id_Genre)
                    .HasPrincipalKey(p => p.id);
            }));
            modelBuilder.Entity((Action<EntityTypeBuilder<Genre>>)(entity =>
            {
                entity.ToTable("genre");
                entity.HasKey(e => e.id).HasName("id");

                entity.Property(e => e.Name).HasColumnName("name").IsRequired();
            }));
            modelBuilder.Entity((Action<EntityTypeBuilder<Hall>>)(entity =>
            {
                entity.ToTable("hall");
                entity.HasKey(e => e.id).HasName("id");

                entity.Property(e => e.Name).HasColumnName("name").IsRequired();
            }));
            modelBuilder.Entity((Action<EntityTypeBuilder<Seat>>)(entity =>
            {
                entity.ToTable("seat");
                entity.HasKey(e => e.id).HasName("id");

                entity.Property(e => e.Number).HasColumnName("number").IsRequired();
                entity.Property(e => e.Row).HasColumnName("row").IsRequired();

                entity.HasOne(d => d.SeatCategory).WithMany(p => p.Seat).HasForeignKey(d => d.id_category)
                    .HasPrincipalKey(p => p.id);
                entity.HasOne(d => d.Hall).WithMany(p => p.Seat).HasForeignKey(d => d.id_hall)
                    .HasPrincipalKey(p => p.id);
            }));
            modelBuilder.Entity((Action<EntityTypeBuilder<SeatCategory>>)(entity =>
            {
                entity.ToTable("seatcategory");
                entity.HasKey(e => e.id).HasName("id");

                entity.Property(e => e.Category).HasColumnName("category").IsRequired();
                entity.Property(e => e.Cost).HasColumnName("cost");
            }));
            modelBuilder.Entity((Action<EntityTypeBuilder<Session>>)(entity =>
            {
                entity.ToTable("session");
                entity.HasKey(e => e.id).HasName("id");

                entity.Property(e => e.Date).HasColumnName("date");
                entity.Property(e => e.Start).HasColumnName("start").IsRequired();
                entity.Property(e => e.Markup).HasColumnName("markup");

                entity.HasOne(d => d.Film).WithMany(p => p.Session).HasForeignKey(d => d.id_film)
                    .HasPrincipalKey(p => p.id).IsRequired();
                entity.HasOne(d => d.Hall).WithMany(p => p.Session).HasForeignKey(d => d.id_hall)
                    .HasPrincipalKey(p => p.id).IsRequired();
            }));
            modelBuilder.Entity((Action<EntityTypeBuilder<Ticket>>)(entity =>
            {
                entity.ToTable("ticket");
                entity.HasKey(e => e.id).HasName("id");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.HasOne(d => d.Session).WithMany(p => p.Tickets).HasForeignKey(d => d.id_session)
                    .HasPrincipalKey(p => p.id);
                entity.HasOne(d => d.Seat).WithMany(p => p.Tickets).HasForeignKey(d => d.id_seat)
                    .HasPrincipalKey(p => p.id);
                entity.HasOne(d => d.Client).WithMany(p => p.Tickets).HasForeignKey(d => d.id_client)
                    .HasPrincipalKey(p=>p.id);
            }));

            base.OnModelCreating(modelBuilder);
        }
    }
}
