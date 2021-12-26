﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using puzzle.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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
                if (!LocalStorage.Exists(g.Path)) continue;

                g.Image = LocalStorage.Load(g.Path);
                if (Hasher.HashImage(g.Image) == g.ImageHash)
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
        public static IList<PuzzleVM> LoadPuzzles(IList<ImageVM> gallery)
        {
            List<PuzzleVM> puzzleFields;
            List<PuzzleVM> puzzleTapes;
            using (var db = new PuzzleContext(Options))
            {
                puzzleFields = db.PuzzleFields.Join(
                    db.Puzzles,
                    p => p.PuzzleId,
                    pf => pf.PuzzleField.PuzzleId,
                    (pf, p) => new PuzzleVM
                    {
                        Id = p.PuzzleId,
                        Name = p.Name,
                        ImageId = p.ImageId,
                        DifficultyLevelId = p.DifficultyLevelId,
                        FragmentNumbers = pf.FragmentNumbers
                    }).ToList();
                puzzleTapes = db.PuzzleTapes.Join(
                    db.Puzzles,
                    p => p.PuzzleId,
                    pf => pf.PuzzleTape.PuzzleId,
                    (pf, p) => new PuzzleVM
                    {
                        Id = p.PuzzleId,
                        Name = p.Name,
                        ImageId = p.ImageId,
                        DifficultyLevelId = p.DifficultyLevelId,
                        FragmentNumbers = pf.FragmentNumbers
                    }).ToList();
            }
            List<PuzzleVM> newGallery = new();
            foreach (var p in puzzleFields.Union(puzzleTapes).ToList())
            {
                ImageVM image;
                try
                {
                    image = gallery
                        .Where(i => i.Id == p.ImageId)
                        .Single();
                }
                catch
                {
                    continue;
                }

                if (!LocalStorage.Exists(image.Path)) continue;

                image.Image = LocalStorage.Load(image.Path);
                if (Hasher.HashImage(image.Image) == image.ImageHash)
                {
                    newGallery.Add(p);
                }
            }
            return newGallery;
        }
    }
}
