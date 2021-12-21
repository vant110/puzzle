using Microsoft.EntityFrameworkCore;

namespace puzzle.Services
{
    static class Db
    {
        private static DbContextOptions<PuzzleContext> options;

        public static DbContextOptions<PuzzleContext> Options { get => options; set => options = value; }

        public static PuzzleContext Instance { get; set; }

        public static void LoadGalleries()
        {
            var gs = Instance.Galleries;
            gs.Load();
            foreach (var g in gs.Local)
            {
                if (!LocalStorage.Exists(g.Path)
                    || Hasher.HashAndCloseFile(LocalStorage.Load(g.Path)) != g.ImageHash)
                {
                    gs.Local.Remove(g);
                }
            }
        }
    }
}
