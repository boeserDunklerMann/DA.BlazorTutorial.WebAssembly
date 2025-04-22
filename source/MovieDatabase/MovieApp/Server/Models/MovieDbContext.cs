using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MovieApp.Server.Models;

public partial class MovieDbContext : DbContext
{
    public MovieDbContext(DbContextOptions<MovieDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    public virtual DbSet<Watchlist> Watchlists { get; set; }

    public virtual DbSet<WatchlistItem> WatchlistItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PRIMARY");

            entity.ToTable("Genre");

            entity.Property(e => e.GenreId)
                .HasColumnType("int(11)")
                .HasColumnName("GenreID");
            entity.Property(e => e.GenreName).HasMaxLength(30);
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PRIMARY");

            entity.ToTable("Movie");

            entity.Property(e => e.MovieId)
                .HasColumnType("int(11)")
                .HasColumnName("MovieID");
            entity.Property(e => e.Duration).HasColumnType("int(11)");
            entity.Property(e => e.Genre).HasMaxLength(30);
            entity.Property(e => e.Language)
                .HasMaxLength(30)
                .HasColumnName("LANGUAGE");
            entity.Property(e => e.Overview).HasMaxLength(1024);
            entity.Property(e => e.PosterPath)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Rating)
                .HasPrecision(3, 1)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("User");

            entity.HasIndex(e => e.UserTypeId, "FK_User_UserType");

            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("UserID");
            entity.Property(e => e.FirstName).HasMaxLength(150);
            entity.Property(e => e.Gender)
                .HasMaxLength(6)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.LastName).HasMaxLength(150);
            entity.Property(e => e.Password)
                .HasMaxLength(40)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.UserTypeId)
                .HasColumnType("int(11)")
                .HasColumnName("UserTypeID");
            entity.Property(e => e.Username).HasMaxLength(20);

            entity.HasOne(d => d.UserType).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_User_UserType");
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.HasKey(e => e.UserTypeId).HasName("PRIMARY");

            entity.ToTable("UserType");

            entity.Property(e => e.UserTypeId)
                .HasColumnType("int(11)")
                .HasColumnName("UserTypeID");
            entity.Property(e => e.UserTypeName).HasMaxLength(20);
        });

        modelBuilder.Entity<Watchlist>(entity =>
        {
            entity.HasKey(e => e.WatchlistId).HasName("PRIMARY");

            entity.ToTable("Watchlist");

            entity.Property(e => e.WatchlistId)
                .HasColumnType("int(11)")
                .HasColumnName("WatchlistID");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("'current_timestamp()'")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("UserID");
        });

        modelBuilder.Entity<WatchlistItem>(entity =>
        {
            entity.HasKey(e => e.WatchlistItemId).HasName("PRIMARY");

            entity.ToTable("WatchlistItem");

            entity.HasIndex(e => e.MovieId, "FK_WatchlistIteem_Movie");

            entity.HasIndex(e => e.WatchlistId, "FK_WatchlistItem_Watchlist");

            entity.Property(e => e.WatchlistItemId)
                .HasColumnType("int(11)")
                .HasColumnName("WatchlistItemID");
            entity.Property(e => e.MovieId)
                .HasColumnType("int(11)")
                .HasColumnName("MovieID");
            entity.Property(e => e.WatchlistId)
                .HasColumnType("int(11)")
                .HasColumnName("WatchlistID");

            entity.HasOne(d => d.Movie).WithMany(p => p.WatchlistItems)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_WatchlistIteem_Movie");

            entity.HasOne(d => d.Watchlist).WithMany(p => p.WatchlistItems)
                .HasForeignKey(d => d.WatchlistId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_WatchlistItem_Watchlist");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
