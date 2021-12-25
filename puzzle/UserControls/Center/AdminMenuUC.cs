using System;
using System.Windows.Forms;

namespace puzzle.UserControls
{
    public partial class AdminMenuUC : UserControl
    {
        public AdminMenuUC(MainForm form)
        {
            InitializeComponent();

            buttonGallery.Click += new EventHandler((s, e) =>
            {
                form.DisplayGallery();
            });
            buttonLevels.Click += new EventHandler((s, e) =>
            {
                form.DisplayLevels();
            });
            buttonPuzzles.Click += new EventHandler((s, e) =>
            {
                form.DisplayPuzzles();
            });
        }
    }
}
