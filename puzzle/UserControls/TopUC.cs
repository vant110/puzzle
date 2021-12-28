using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace puzzle.UserControls
{
    public partial class TopUC : UserControl
    {
        private EventHandler buttonBackClick;

        public TopUC()
        {
            InitializeComponent();
        }        

        public EventHandler ButtonBackClick
        {
            set 
            {
                buttonBack.Click -= buttonBackClick;
                buttonBackClick = value;
                buttonBack.Click += buttonBackClick;
            }
        }

        public bool ButtonImageOn
        {
            set
            {
                buttonImage.ImageIndex = value ? 7 : 6;
            }
        }
        public bool ButtonPauseOn
        {
            set
            {
                buttonPause.ImageIndex = value ? 5 : 4;
            }
        }
        public bool ButtonSoundOn
        {
            set
            {
                buttonSound.ImageIndex = value ? 3 : 2;
            }
        }
    }
}
