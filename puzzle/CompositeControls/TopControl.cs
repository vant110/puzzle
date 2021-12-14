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
    public partial class TopControl : UserControl
    {
        private EventHandler buttonBackClick;

        public TopControl()
        {
            InitializeComponent();
        }        

        public bool ButtonBackVisible
        {
            set 
            { 
                buttonBack.Visible = value; 
            }
        }

        public bool ButtonPauseOrPlayVisible
        {
            set
            {
                buttonPauseOrPlay.Visible = value;
            }
        }

        public bool ButtonImageOrPuzzleVisible
        {
            set
            {
                buttonImageOrPuzzle.Visible = value;
            }
        }

        public bool ButtonSoundVisible
        {
            set
            {
                buttonSound.Visible = value;
            }
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

        public string LabelTitleText
        {
            set
            {
                labelTitle.Text = value;
            }
        }

        public bool ButtonPauseOn
        {
            set
            {
                buttonPauseOrPlay.ImageIndex = value ? 5 : 4;
            }
        }

        public bool ButtonImageOn
        {
            set
            {
                buttonImageOrPuzzle.ImageIndex = value ? 7 : 6;
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
