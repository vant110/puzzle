using Microsoft.EntityFrameworkCore;

namespace puzzle.Services
{
    static class Settings
    {
        private static DbContextOptions<PuzzleContext> options;

        public static DbContextOptions<PuzzleContext> Options { get => options; set => options = value; }
    }
}
