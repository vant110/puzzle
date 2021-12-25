using System;
using System.Windows.Forms;

namespace puzzle.Dialogs
{
    public partial class InsertOrUpdateLevelForm : Form
    {
        private EventHandler buttonInsertOrUpdateClick;

        public InsertOrUpdateLevelForm(MainForm form)
        {
            InitializeComponent();

            comboBoxFragmentType.DataSource = form.bindingSourceFragmentTypes.DataSource;
            comboBoxAssemblyType.DataSource = form.bindingSourceAssemblyTypes.DataSource;
            numericUpDownHorizontal.ValueChanged += new EventHandler((s, e) =>
            {
                SetLabelVertical();
            });
            SetLabelVertical();
        }

        public EventHandler ButtonInsertOrUpdateClick
        {
            set
            {
                buttonInsertOrUpdate.Click -= buttonInsertOrUpdateClick;
                buttonInsertOrUpdateClick = value;
                buttonInsertOrUpdate.Click += buttonInsertOrUpdateClick;
            }
        }

        private void SetLabelVertical()
        {
            labelVertical.Text = Math.Round((double)numericUpDownHorizontal.Value / ((double)600 / 450)).ToString();
        }
    }
}
