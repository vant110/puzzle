using System;
using System.Windows.Forms;

namespace puzzle.UserControls
{
    public partial class AdminMenuControl : UserControl
    {
        public AdminMenuControl(MainForm form)
        {
            InitializeComponent();

            buttonGallery.Click += new EventHandler((s, e) =>
            {
                form.ConfigureOnGallery();
            });
            buttonLevels.Click += new EventHandler((s, e) =>
            {
                form.ConfigureOnLevels();
            });
            buttonPuzzles.Click += new EventHandler((s, e) =>
            {
                form.ConfigureOnPuzzles();
            });
        }
    }
}
