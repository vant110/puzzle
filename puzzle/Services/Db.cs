using Microsoft.EntityFrameworkCore;

namespace puzzle.Services
{
    static class Db
    {
        private static DbContextOptions<PuzzleContext> options;

        public static DbContextOptions<PuzzleContext> Options { get => options; set => options = value; }

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
    }
}
