using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using puzzle.Model;
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
        public static IList<SavedGameVM> LoadGames()
        {
            List<SavedGameVM> fieldGames;
            List<SavedGameVM> tapeGames;
            using (var db = new PuzzleContext(Options))
            {
                List<SavedGameVM> games;
                List<SavedGameVM> scoreGames;
                List<SavedGameVM> timeGames;
                games = db.SavedGames
                    .Where(i => i.PlayerId == ResultDTO.PlayerId
                        && i.CountingMethodId == 0)
                    .Select(i => new SavedGameVM
                    {
                        SavedGameId = i.SavedGameId,
                        PuzzleId = i.PuzzleId,
                        FieldFragmentNumbers = i.FieldFragmentNumbers,
                        CountingMethodId = i.CountingMethodId,
                    })
                    .ToList();
                scoreGames = db.SavedGameScores.Join(
                    db.SavedGames
                    .Where(i => i.PlayerId == ResultDTO.PlayerId),
                    sgs => sgs.SavedGameId,
                    sg => sg.SavedGameId,
                    (sgs, sg) => new SavedGameVM
                    {
                        SavedGameId = sg.SavedGameId,
                        PuzzleId = sg.PuzzleId,
                        FieldFragmentNumbers = sg.FieldFragmentNumbers,
                        CountingMethodId = sg.CountingMethodId,
                        Score = sgs.Score,
                    })
                    .ToList();
                timeGames = db.SavedGameTimes.Join(
                     db.SavedGames
                     .Where(i => i.PlayerId == ResultDTO.PlayerId),
                     sgt => sgt.SavedGameId,
                     sg => sg.SavedGameId,
                     (sgt, sg) => new SavedGameVM
                     {
                         SavedGameId = sg.SavedGameId,
                         PuzzleId = sg.PuzzleId,
                         FieldFragmentNumbers = sg.FieldFragmentNumbers,
                         CountingMethodId = sg.CountingMethodId,
                         Time = sgt.Time
                     }).ToList();
                games = games.Union(scoreGames).Union(timeGames).ToList();

                fieldGames = games.Where(g => (
                    db.DifficultyLevels.Join(
                        db.Puzzles
                        .Where(p => p.PuzzleId == g.PuzzleId),
                        dl => dl.DifficultyLevelId,
                        p => p.DifficultyLevelId,
                        (dl, p) => dl.AssemblyTypeId)
                    .Single()) == 1).ToList();
                tapeGames = games
                    .Where(g => (
                        db.DifficultyLevels.Join(
                            db.Puzzles
                            .Where(p => p.PuzzleId == g.PuzzleId),
                            dl => dl.DifficultyLevelId,
                            p => p.DifficultyLevelId,
                            (dl, p) => dl.AssemblyTypeId)
                        .Single()) == 2)
                    .Join(
                        db.SavedGameTapes,
                        g => g.SavedGameId,
                        sgt => sgt.SavedGameId,
                        (g, sgt) => new SavedGameVM
                        {
                            SavedGameId = g.SavedGameId,
                            PuzzleId = g.PuzzleId,
                            FieldFragmentNumbers = g.FieldFragmentNumbers,
                            TapeFragmentNumbers = sgt.FragmentNumbers,
                            CountingMethodId = g.CountingMethodId,
                            Time = g.Time,
                            Score = g.Score                        
                        }).ToList();
            }

            return fieldGames.Union(tapeGames).ToList();
        }
        public static IList<RecordVM> LoadRecords(short puzzleId, sbyte methodId)
        {
            using var db = new PuzzleContext(Options);
            var records = db.Records
                .Where(i => i.PuzzleId == puzzleId
                    && i.CountingMethodId == methodId)
                .Join(db.Players,
                    r => r.PlayerId,
                    p => p.PlayerId,
                    (r, p) => new RecordVM
                    {
                        RecordId = r.RecordId,
                        CountingMethodId = r.CountingMethodId,
                        Login = p.Login
                    });
            if (methodId == 1)
            {
                records = records.Join(db.RecordScores,
                    r => r.RecordId,
                    rs => rs.RecordId,
                    (r, rs) => new RecordVM
                    {

                        RecordId = r.RecordId,
                        CountingMethodId = r.CountingMethodId,
                        Login = r.Login,
                        Score = rs.Score
                    });
            }
            else if (methodId == 2)
            {
                records = records.Join(db.RecordTimes,
                    r => r.RecordId,
                    rt => rt.RecordId,
                    (r, rt) => new RecordVM
                    {

                        RecordId = r.RecordId,
                        CountingMethodId = r.CountingMethodId,
                        Login = r.Login,
                        Time = rt.Time
                    });
            }
            return records.ToList();
        }
    }
}
