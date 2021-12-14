using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using puzzle.Services;
using puzzle.Model;

namespace puzzle
{
    public partial class MainForm : Form
    {
        private TopControl topControl;
        private Control fillControl;
        private BottomControl bottomControl;

        public MainForm()
        {
            InitializeComponent();
            
            CreateTopControl();
            CreateRegAndAuth();
        }

        private void CreateTopControl()
        {
            topControl = new TopControl
            {
                Dock = DockStyle.Top
            };
            Controls.Add(topControl);
        }

        private void CreateBottomControl()
        {
            bottomControl = new BottomControl
            {
                Dock = DockStyle.Bottom
            };
            Controls.Add(bottomControl);
        }

        private void ChangeFillControl(Control newControl)
        {
            Controls.Remove(fillControl);
            fillControl = newControl;
            Controls.Add(fillControl);
        }

        private void CreateRegAndAuth()
        {
            var newControl = new RegAndAuth
            {
                Dock = DockStyle.Fill
            };
            ChangeFillControl(newControl);

            topControl.ButtonBackVisible = false;
            topControl.ButtonPauseOrPlayVisible = false;
            topControl.ButtonImageOrPuzzleVisible = false;
            topControl.ButtonSoundVisible = false;

            topControl.LabelTitleText = "Регистрация/авторизация";
        }
    }
}
