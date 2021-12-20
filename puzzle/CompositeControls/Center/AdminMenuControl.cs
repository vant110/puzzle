using System;
using System.Windows.Forms;

namespace puzzle.CompositeControls
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
            });
            buttonPuzzles.Click += new EventHandler((s, e) =>
            {
            });
        }
    }
}
