using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using puzzle.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace puzzle.Services
{
    static class Db
    {
        public static DbContextOptions<PuzzleContext> Options { get; private set; }

        public static void InitOptions()
        {
            if (Options != null)
            {
                throw new InvalidOperationException();
            }

            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<PuzzleContext>();
            Options = optionsBuilder
                .UseMySql(connectionString, ServerVersion.Parse("8.0.27-mysql"))
                .Options;

            optionsBuilder.LogTo(
                message => Debug.WriteLine(message),
                LogLevel.Trace);
        }

        public static IList<ImageVM> LoadGallery()
        {
            List<ImageVM> gallery;
            using (var db = new PuzzleContext(Options))
            {
                gallery = db.Galleries
                    .Select(i => new ImageVM
                    {
                        Id = i.ImageId,
                        Name = i.Name,
                        Path = i.Path,
                        ImageHash = i.ImageHash
                    }).ToList();
            }
            List<ImageVM> newGallery = new();
            foreach (var g in gallery)
            {
                if (LocalStorage.Exists(g.Path)
                    && Hasher.HashImageAndClose(LocalStorage.Load(g.Path)) == g.ImageHash)
                {
                    newGallery.Add(g);
                }
            }
            return newGallery;
        }
        public static IList<LevelVM> LoadLevels()
        {
            using var db = new PuzzleContext(Options);
            return db.DifficultyLevels
                .Select(i => new LevelVM
                {
                    Id = i.DifficultyLevelId,
                    Name = i.Name,
                    HorizontalFragmentCount = i.HorizontalFragmentCount,
                    FragmentTypeId = i.FragmentTypeId,
                    AssemblyTypeId = i.AssemblyTypeId
                }).ToList();
        }
        public static IList<PuzzleVM> LoadPuzzles()
        {
            using var db = new PuzzleContext(Options);
            return db.Puzzles
                .Select(i => new PuzzleVM
                {
                    Id = i.PuzzleId,
                    Name = i.Name,
                    ImageId = i.ImageId,
                    DifficultyLevelId = i.DifficultyLevelId
                }).ToList();
        }
    }
}
