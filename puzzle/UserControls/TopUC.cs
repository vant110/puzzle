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
        private EventHandler buttonImageClick;
        private EventHandler buttonSoundClick;

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
        public EventHandler ButtonImageClick
        {
            set
            {
                buttonImage.Click -= buttonImageClick;
                buttonImageClick = value;
                buttonImage.Click += buttonImageClick;
            }
        }
        public EventHandler ButtonSoundClick
        {
            set
            {
                buttonSound.Click -= buttonSoundClick;
                buttonSoundClick = value;
                buttonSound.Click += buttonSoundClick;
            }
        }
    }
}
