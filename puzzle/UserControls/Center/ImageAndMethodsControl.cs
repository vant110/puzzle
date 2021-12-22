using System.Windows.Forms;

namespace puzzle.UserControls
{
    public partial class ImageAndMethodsControl : UserControl
    {
        public ImageAndMethodsControl()
        {
            InitializeComponent();
        }

        public bool PanelMethodsVisible
        {
            set
            {
                panelMethods.Visible = value;
            }
        }
    }
}
