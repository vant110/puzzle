using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace puzzle.CompositeControls
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
