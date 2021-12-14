using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace puzzle.Services
{
    public partial class AdminMenu : UserControl
    {
        private EventHandler buttonGalleryClick;
        private EventHandler buttonLevelsClick;
        private EventHandler buttonPuzzlesClick;

        public AdminMenu()
        {
            InitializeComponent();
        }

        public EventHandler ButtonGalleryClick
        {
            set
            {
                buttonGallery.Click -= buttonGalleryClick;
                buttonGalleryClick = value;
                buttonGallery.Click += buttonGalleryClick;
            }
        }

        public EventHandler ButtonLevelsClick
        {
            set
            {
                buttonLevels.Click -= buttonLevelsClick;
                buttonLevelsClick = value;
                buttonLevels.Click += buttonLevelsClick;
            }
        }

        public EventHandler ButtonPuzzlesClick
        {
            set
            {
                buttonPuzzles.Click -= buttonPuzzlesClick;
                buttonPuzzlesClick = value;
                buttonPuzzles.Click += buttonPuzzlesClick;
            }
        }
    }
}
