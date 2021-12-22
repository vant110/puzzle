using System.Windows.Forms;

namespace puzzle.UserControls
{
    public partial class ListControl : UserControl
    {
        public ListControl()
        {
            InitializeComponent();
        }

        public bool PanelLevelVisible
        {
            set
            {
                panelLevel.Visible = value;
            }
        }
    }
}
