using puzzle.Dialogs;
using System;
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

            buttonHelp.Click += new EventHandler((s, e) => new HelpForm().Show());
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
