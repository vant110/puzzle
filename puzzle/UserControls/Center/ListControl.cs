using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace puzzle.UserControls
{
    [ComplexBindingProperties("DataSource", "DataMember")]
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

        public object DataSource
        {
            get
            {
                return new object();
            }
            set
            {
            }
        }

        public string DataMember
        {
            get { return ""; }

            set {  }
        }
    }
}
