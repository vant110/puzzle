using System;
using System.Windows.Forms;

namespace puzzle.UserControls
{
    public partial class BottomUC : UserControl
    {
        private EventHandler buttonDeleteClick;
        private EventHandler buttonUpdateClick;
        private EventHandler buttonLoadClick;
        private EventHandler buttonInsertOrNewGameClick;

        public BottomUC()
        {
            InitializeComponent();
        }

        public bool ButtonDeleteVisible { set => buttonDelete.Visible = value; }
        public bool ButtonUpdateVisible { set => buttonUpdate.Visible = value; }
        public bool ButtonLoadVisible { set => buttonLoad.Visible = value; }

        public string ButtonInsertOrNewGameText { set => buttonInsertOrNewGame.Text = value; }

        public EventHandler ButtonDeleteClick
        {
            set
            {
                buttonDelete.Click -= buttonDeleteClick;
                buttonDeleteClick = value;
                buttonDelete.Click += buttonDeleteClick;
            }
        }
        public EventHandler ButtonUpdateClick
        {
            set
            {
                buttonUpdate.Click -= buttonUpdateClick;
                buttonUpdateClick = value;
                buttonUpdate.Click += buttonUpdateClick;
            }
        }
        public EventHandler ButtonLoadClick
        {
            set
            {
                buttonLoad.Click -= buttonLoadClick;
                buttonLoadClick = value;
                buttonLoad.Click += buttonLoadClick;
            }
        }
        public EventHandler ButtonInsertOrNewGameClick
        {
            set
            {
                buttonInsertOrNewGame.Click -= buttonInsertOrNewGameClick;
                buttonInsertOrNewGameClick = value;
                buttonInsertOrNewGame.Click += buttonInsertOrNewGameClick;
            }
        }
    }
}
