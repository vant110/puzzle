using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace puzzle.Services
{
    static class MessageBoxes
    {
        private static readonly string caption = "Игра \"Puzzle\"";

        public static void Error(string text)
        {
            MessageBox.Show(
                text,
                caption,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
