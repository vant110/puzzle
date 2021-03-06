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

        public static void Info(string text)
        {
            MessageBox.Show(
                text,
                caption,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        public static DialogResult Question3(string text)
        {
            return MessageBox.Show(
                text,
                caption,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);
        }

        public static DialogResult Question2(string text)
        {
            return MessageBox.Show(
                text,
                caption,
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);
        }
    }
}
