using System;

namespace puzzle.Model
{
    static class LevelDTO
    {
        private static int horizontalFragmentCount;

        public static string Name { get; set; }
        public static int HorizontalFragmentCount {
            get
            {
                return horizontalFragmentCount;
            }
            set 
            {
                horizontalFragmentCount = value;
                VerticalFragmentCount = (int)Math.Round(horizontalFragmentCount / ((double)600 / 450));
            } 
        }
        public static int VerticalFragmentCount { get; private set; }
        public static int FragmentTypeId { get; set; }
        public static int AssemblyTypeId { get; set; }
    }
}
