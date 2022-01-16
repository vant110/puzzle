using System;
using System.Windows.Forms;

namespace puzzle.UserControls
{
    public partial class ImageAndMethodsUC : UserControl
    {
        private EventHandler buttonRecords1Click;
        private EventHandler buttonRecords2Click;

        public ImageAndMethodsUC()
        {
            InitializeComponent();
        }

        public EventHandler ButtonRecords1Click
        {
            set
            {
                buttonRecords1.Click -= buttonRecords1Click;
                buttonRecords1Click = value;
                buttonRecords1.Click += buttonRecords1Click;
            }
        }
        public EventHandler ButtonRecords2Click
        {
            set
            {
                buttonRecords2.Click -= buttonRecords2Click;
                buttonRecords2Click = value;
                buttonRecords2.Click += buttonRecords2Click;
            }
        }
    }
}
