using Microsoft.EntityFrameworkCore;
using puzzle.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace puzzle.Services
{
    static class Db
    {
        public static DbContextOptions<PuzzleContext> Options { get; set; }

        public static PuzzleContext Instance { get; set; }

        public static void FirstLoad()
        {
            Instance.FragmentTypes.Load();
            Instance.AssemblyTypes.Load();
            Instance.CountingMethods.Load();
        }

        public static void LoadGalleries()
        {
            Instance.Galleries.Load();
            var gs = Instance.Galleries.Local;
            foreach (var g in gs)
            {
                if (!LocalStorage.Exists(g.Path)
                    || Hasher.HashImage(LocalStorage.Load(g.Path), true) != g.ImageHash)
                {
                    gs.Remove(g);
                }
            }
        }

        public static void LoadDifficultyLevels()
        {
            Instance.DifficultyLevels.Load();
        }

        public static void LoadPuzzles()
        {
            Instance.Puzzles.Load();
        }

        public static IEnumerable<LevelViewModel> GetLevels()
        {
            using var db = new PuzzleContext(Options);
            return db.DifficultyLevels
                .Select(level => new LevelViewModel 
                {
                    Id = level.DifficultyLevelId,
                    Name = level.Name,
                    HorizontalFragmentCount = level.HorizontalFragmentCount,
                    VerticalFragmentCount = level.VerticalFragmentCount,
                    FragmentTypeId = level.FragmentTypeId,
                    AssemblyTypeId = level.AssemblyTypeId
                }).ToList();
        }
    }
}
