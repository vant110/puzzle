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
    public partial class LevelInfoControl : UserControl
    {
        public LevelInfoControl()
        {
            InitializeComponent();
        }

        public string LabelHorizontalText
        {
            set
            {
                labelHorizontal.Text = value;
            }
        }

        public string LabelVerticalText
        {
            set
            {
                labelVertical.Text = value;
            }
        }

        public string LabelFragmentTypeText
        {
            set
            {
                labelFragmentType.Text = value;
            }
        }

        public string LabelAssemblyTypeText
        {
            set
            {
                labelAssemblyType.Text = value;
            }
        }
    }
}
