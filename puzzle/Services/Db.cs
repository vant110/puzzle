using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using puzzle.Model;
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

        public static IList<ImageModel> LoadGallery()
        {
            List<ImageModel> gallery;
            using (var db = new PuzzleContext(Options))
            {
                gallery = db.Galleries
                    .Select(i => new ImageModel
                    {
                        Id = i.ImageId,
                        Name = i.Name,
                        Path = i.Path,
                        ImageHash = i.ImageHash
                    }).ToList();
            }
            List<ImageModel> newGallery = new();
            foreach (var g in gallery)
            {
                if (!LocalStorage.ImageExists(g.Path)) continue;

                g.Image = LocalStorage.LoadImage(g.Path);
                if (Hasher.Hash(g.Image) == g.ImageHash)
                {
                    newGallery.Add(g);
                }
            }
            return newGallery;
        }
        public static IList<LevelModel> LoadLevels()
        {
            using var db = new PuzzleContext(Options);
            return db.DifficultyLevels
                .Select(i => new LevelModel
                {
                    Id = i.DifficultyLevelId,
                    Name = i.Name,
                    HorizontalFragmentCount = i.HorizontalFragmentCount,
                    FragmentTypeId = i.FragmentTypeId,
                    AssemblyTypeId = i.AssemblyTypeId
                }).ToList();
        }
        public static IList<PuzzleModel> LoadPuzzles(IList<ImageModel> gallery)
        {
            List<PuzzleModel> puzzleFields;
            List<PuzzleModel> puzzleTapes;
            using (var db = new PuzzleContext(Options))
            {
                puzzleFields = db.PuzzleFields.Join(
                    db.Puzzles,
                    p => p.PuzzleId,
                    pf => pf.PuzzleField.PuzzleId,
                    (pf, p) => new PuzzleModel
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
                    (pf, p) => new PuzzleModel
                    {
                        Id = p.PuzzleId,
                        Name = p.Name,
                        ImageId = p.ImageId,
                        DifficultyLevelId = p.DifficultyLevelId,
                        FragmentNumbers = pf.FragmentNumbers
                    }).ToList();
            }
            List<PuzzleModel> newGallery = new();
            foreach (var p in puzzleFields.Union(puzzleTapes).ToList())
            {
                ImageModel image;
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

                if (!LocalStorage.ImageExists(image.Path)) continue;

                image.Image = LocalStorage.LoadImage(image.Path);
                if (Hasher.Hash(image.Image) == image.ImageHash)
                {
                    newGallery.Add(p);
                }
            }
            return newGallery;
        }
        public static IList<SavedGameModel> LoadGames()
        {
            List<SavedGameModel> fieldGames;
            List<SavedGameModel> tapeGames;
            using (var db = new PuzzleContext(Options))
            {
                List<SavedGameModel> games;
                List<SavedGameModel> scoreGames;
                List<SavedGameModel> timeGames;
                games = db.SavedGames
                    .Where(i => i.PlayerId == ResultDTO.PlayerId
                        && i.CountingMethodId == 0)
                    .Select(i => new SavedGameModel
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
                    (sgs, sg) => new SavedGameModel
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
                     (sgt, sg) => new SavedGameModel
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
                        (g, sgt) => new SavedGameModel
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
        public static IList<RecordModel> LoadRecords(short puzzleId, sbyte methodId)
        {
            using var db = new PuzzleContext(Options);
            var records = db.Records
                .Where(i => i.PuzzleId == puzzleId
                    && i.CountingMethodId == methodId)
                .Join(db.Players,
                    r => r.PlayerId,
                    p => p.PlayerId,
                    (r, p) => new RecordModel
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
                    (r, rs) => new RecordModel
                    {
                        RecordId = r.RecordId,
                        CountingMethodId = r.CountingMethodId,
                        Login = r.Login,
                        Score = rs.Score
                    }).OrderByDescending(r => r.Score);
            }
            else if (methodId == 2)
            {
                records = records.Join(db.RecordTimes,
                    r => r.RecordId,
                    rt => rt.RecordId,
                    (r, rt) => new RecordModel
                    {
                        RecordId = r.RecordId,
                        CountingMethodId = r.CountingMethodId,
                        Login = r.Login,
                        Time = rt.Time
                    }).OrderBy(r => r.Time);
            }
            return records.ToList();
        }
    }
}
