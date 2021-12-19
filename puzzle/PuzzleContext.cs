using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using puzzle.Data;

#nullable disable

namespace puzzle
{
    public partial class PuzzleContext : DbContext
    {
        public PuzzleContext()
        {
        }

        public PuzzleContext(DbContextOptions<PuzzleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AssemblyType> AssemblyTypes { get; set; }
        public virtual DbSet<CountingMethod> CountingMethods { get; set; }
        public virtual DbSet<DifficultyLevel> DifficultyLevels { get; set; }
        public virtual DbSet<FragmentType> FragmentTypes { get; set; }
        public virtual DbSet<Gallery> Galleries { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Puzzle> Puzzles { get; set; }
        public virtual DbSet<PuzzleField> PuzzleFields { get; set; }
        public virtual DbSet<PuzzleTape> PuzzleTapes { get; set; }
        public virtual DbSet<Record> Records { get; set; }
        public virtual DbSet<RecordScore> RecordScores { get; set; }
        public virtual DbSet<RecordTime> RecordTimes { get; set; }
        public virtual DbSet<SavedGame> SavedGames { get; set; }
        public virtual DbSet<SavedGameScore> SavedGameScores { get; set; }
        public virtual DbSet<SavedGameTape> SavedGameTapes { get; set; }
        public virtual DbSet<SavedGameTime> SavedGameTimes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<AssemblyType>(entity =>
            {
                entity.ToTable("assembly_types");

                entity.HasIndex(e => e.Name, "name")
                    .IsUnique();

                entity.Property(e => e.AssemblyTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("assembly_type_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(7)
                    .HasColumnName("name")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<CountingMethod>(entity =>
            {
                entity.ToTable("counting_methods");

                entity.HasIndex(e => e.Name, "name")
                    .IsUnique();

                entity.Property(e => e.CountingMethodId)
                    .ValueGeneratedNever()
                    .HasColumnName("counting_method_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<DifficultyLevel>(entity =>
            {
                entity.ToTable("difficulty_levels");

                entity.HasIndex(e => e.AssemblyTypeId, "assembly_type_id");

                entity.HasIndex(e => e.FragmentTypeId, "fragment_type_id");

                entity.HasIndex(e => e.Name, "name")
                    .IsUnique();

                entity.Property(e => e.DifficultyLevelId).HasColumnName("difficulty_level_id");

                entity.Property(e => e.AssemblyTypeId).HasColumnName("assembly_type_id");

                entity.Property(e => e.FragmentTypeId).HasColumnName("fragment_type_id");

                entity.Property(e => e.HorizontalFragmentCount).HasColumnName("horizontal_fragment_count");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("name");

                entity.Property(e => e.VerticalFragmentCount).HasColumnName("vertical_fragment_count");

                entity.HasOne(d => d.AssemblyType)
                    .WithMany(p => p.DifficultyLevels)
                    .HasForeignKey(d => d.AssemblyTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("difficulty_levels_ibfk_2");

                entity.HasOne(d => d.FragmentType)
                    .WithMany(p => p.DifficultyLevels)
                    .HasForeignKey(d => d.FragmentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("difficulty_levels_ibfk_1");
            });

            modelBuilder.Entity<FragmentType>(entity =>
            {
                entity.ToTable("fragment_types");

                entity.HasIndex(e => e.Name, "name")
                    .IsUnique();

                entity.Property(e => e.FragmentTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("fragment_type_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(13)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Gallery>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PRIMARY");

                entity.ToTable("gallery");

                entity.HasIndex(e => e.Name, "name")
                    .IsUnique();

                entity.HasIndex(e => e.Path, "path")
                    .IsUnique();

                entity.Property(e => e.ImageId).HasColumnName("image_id");

                entity.Property(e => e.ImageHash)
                    .IsRequired()
                    .HasColumnType("tinyblob")
                    .HasColumnName("image_hash");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("name");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("path");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("players");

                entity.HasIndex(e => e.Login, "login")
                    .IsUnique();

                entity.Property(e => e.PlayerId).HasColumnName("player_id");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(8)
                    .HasColumnName("login");

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasColumnType("tinyblob")
                    .HasColumnName("password_hash");
            });

            modelBuilder.Entity<Puzzle>(entity =>
            {
                entity.ToTable("puzzles");

                entity.HasIndex(e => e.DifficultyLevelId, "difficulty_level_id");

                entity.HasIndex(e => e.ImageId, "image_id");

                entity.HasIndex(e => e.Name, "name")
                    .IsUnique();

                entity.Property(e => e.PuzzleId).HasColumnName("puzzle_id");

                entity.Property(e => e.DifficultyLevelId).HasColumnName("difficulty_level_id");

                entity.Property(e => e.ImageId).HasColumnName("image_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("name");

                entity.HasOne(d => d.DifficultyLevel)
                    .WithMany(p => p.Puzzles)
                    .HasForeignKey(d => d.DifficultyLevelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("puzzles_ibfk_2");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Puzzles)
                    .HasForeignKey(d => d.ImageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("puzzles_ibfk_1");
            });

            modelBuilder.Entity<PuzzleField>(entity =>
            {
                entity.HasKey(e => e.PuzzleId)
                    .HasName("PRIMARY");

                entity.ToTable("puzzle_fields");

                entity.Property(e => e.PuzzleId)
                    .ValueGeneratedNever()
                    .HasColumnName("puzzle_id");

                entity.Property(e => e.FragmentNumbers)
                    .IsRequired()
                    .HasColumnType("tinyblob")
                    .HasColumnName("fragment_numbers");

                entity.HasOne(d => d.Puzzle)
                    .WithOne(p => p.PuzzleField)
                    .HasForeignKey<PuzzleField>(d => d.PuzzleId)
                    .HasConstraintName("puzzle_fields_ibfk_1");
            });

            modelBuilder.Entity<PuzzleTape>(entity =>
            {
                entity.HasKey(e => e.PuzzleId)
                    .HasName("PRIMARY");

                entity.ToTable("puzzle_tapes");

                entity.Property(e => e.PuzzleId)
                    .ValueGeneratedNever()
                    .HasColumnName("puzzle_id");

                entity.Property(e => e.FragmentNumbers)
                    .IsRequired()
                    .HasColumnType("tinyblob")
                    .HasColumnName("fragment_numbers");

                entity.HasOne(d => d.Puzzle)
                    .WithOne(p => p.PuzzleTape)
                    .HasForeignKey<PuzzleTape>(d => d.PuzzleId)
                    .HasConstraintName("puzzle_tapes_ibfk_1");
            });

            modelBuilder.Entity<Record>(entity =>
            {
                entity.ToTable("records");

                entity.HasIndex(e => e.CountingMethodId, "counting_method_id");

                entity.HasIndex(e => e.PlayerId, "player_id");

                entity.HasIndex(e => e.PuzzleId, "puzzle_id");

                entity.Property(e => e.RecordId).HasColumnName("record_id");

                entity.Property(e => e.CountingMethodId).HasColumnName("counting_method_id");

                entity.Property(e => e.PlayerId).HasColumnName("player_id");

                entity.Property(e => e.PuzzleId).HasColumnName("puzzle_id");

                entity.HasOne(d => d.CountingMethod)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.CountingMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("records_ibfk_3");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("records_ibfk_1");

                entity.HasOne(d => d.Puzzle)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.PuzzleId)
                    .HasConstraintName("records_ibfk_2");
            });

            modelBuilder.Entity<RecordScore>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PRIMARY");

                entity.ToTable("record_scores");

                entity.Property(e => e.RecordId)
                    .ValueGeneratedNever()
                    .HasColumnName("record_id");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.HasOne(d => d.Record)
                    .WithOne(p => p.RecordScore)
                    .HasForeignKey<RecordScore>(d => d.RecordId)
                    .HasConstraintName("record_scores_ibfk_1");
            });

            modelBuilder.Entity<RecordTime>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PRIMARY");

                entity.ToTable("record_times");

                entity.Property(e => e.RecordId)
                    .ValueGeneratedNever()
                    .HasColumnName("record_id");

                entity.Property(e => e.Time)
                    .HasColumnType("mediumint")
                    .HasColumnName("time");

                entity.HasOne(d => d.Record)
                    .WithOne(p => p.RecordTime)
                    .HasForeignKey<RecordTime>(d => d.RecordId)
                    .HasConstraintName("record_times_ibfk_1");
            });

            modelBuilder.Entity<SavedGame>(entity =>
            {
                entity.ToTable("saved_games");

                entity.HasIndex(e => e.CountingMethodId, "counting_method_id");

                entity.HasIndex(e => e.PlayerId, "player_id");

                entity.HasIndex(e => e.PuzzleId, "puzzle_id");

                entity.Property(e => e.SavedGameId).HasColumnName("saved_game_id");

                entity.Property(e => e.CountingMethodId).HasColumnName("counting_method_id");

                entity.Property(e => e.FieldFragmentNumbers)
                    .IsRequired()
                    .HasColumnType("tinyblob")
                    .HasColumnName("field_fragment_numbers");

                entity.Property(e => e.PlayerId).HasColumnName("player_id");

                entity.Property(e => e.PuzzleId).HasColumnName("puzzle_id");

                entity.HasOne(d => d.CountingMethod)
                    .WithMany(p => p.SavedGames)
                    .HasForeignKey(d => d.CountingMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("saved_games_ibfk_3");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.SavedGames)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("saved_games_ibfk_2");

                entity.HasOne(d => d.Puzzle)
                    .WithMany(p => p.SavedGames)
                    .HasForeignKey(d => d.PuzzleId)
                    .HasConstraintName("saved_games_ibfk_1");
            });

            modelBuilder.Entity<SavedGameScore>(entity =>
            {
                entity.HasKey(e => e.SavedGameId)
                    .HasName("PRIMARY");

                entity.ToTable("saved_game_scores");

                entity.Property(e => e.SavedGameId)
                    .ValueGeneratedNever()
                    .HasColumnName("saved_game_id");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.HasOne(d => d.SavedGame)
                    .WithOne(p => p.SavedGameScore)
                    .HasForeignKey<SavedGameScore>(d => d.SavedGameId)
                    .HasConstraintName("saved_game_scores_ibfk_1");
            });

            modelBuilder.Entity<SavedGameTape>(entity =>
            {
                entity.HasKey(e => e.SavedGameId)
                    .HasName("PRIMARY");

                entity.ToTable("saved_game_tapes");

                entity.Property(e => e.SavedGameId)
                    .ValueGeneratedNever()
                    .HasColumnName("saved_game_id");

                entity.Property(e => e.FragmentNumbers)
                    .IsRequired()
                    .HasColumnType("tinyblob")
                    .HasColumnName("fragment_numbers");

                entity.HasOne(d => d.SavedGame)
                    .WithOne(p => p.SavedGameTape)
                    .HasForeignKey<SavedGameTape>(d => d.SavedGameId)
                    .HasConstraintName("saved_game_tapes_ibfk_1");
            });

            modelBuilder.Entity<SavedGameTime>(entity =>
            {
                entity.HasKey(e => e.SavedGameId)
                    .HasName("PRIMARY");

                entity.ToTable("saved_game_times");

                entity.Property(e => e.SavedGameId)
                    .ValueGeneratedNever()
                    .HasColumnName("saved_game_id");

                entity.Property(e => e.Time)
                    .HasColumnType("mediumint")
                    .HasColumnName("time");

                entity.HasOne(d => d.SavedGame)
                    .WithOne(p => p.SavedGameTime)
                    .HasForeignKey<SavedGameTime>(d => d.SavedGameId)
                    .HasConstraintName("saved_game_times_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
